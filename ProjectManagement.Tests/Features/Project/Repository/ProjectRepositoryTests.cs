using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using ProjectManagement.Data;
using ProjectManagement.Domain;
using ProjectManagement.Features.Project.Repository;
using Xunit;

namespace ProjectManagement.Tests.Features.Project.Repository
{
    public class ProjectRepositoryTests : IDisposable
    {
        private readonly DataContext _context;
        private readonly ProjectRepository _repository;

        public ProjectRepositoryTests()
        {
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _context = new DataContext(options);
            _repository = new ProjectRepository(_context);
        }

        [Fact]
        public async Task GetProjectByIdAsync_WhenProjectExists_ReturnsProjectWithRelatedData()
        {
            // Arrange
            var projectId = Guid.NewGuid();
            var statusId = Guid.NewGuid();
            var userId = Guid.NewGuid();
            var taskId = Guid.NewGuid();

            var status = new ProjectStatus { Id = statusId, StatusName = "Active" };
            var user = new Domain.User { Id = userId, Name = "Test User", Email = "test@example.com" };
            var task = new Domain.TaskItem { Id = taskId, Title = "Test Task", Description = "Description" };
            var project = new Domain.Project
            {
                Id = projectId,
                ProjectName = "Test Project",
                Description = "Test Description",
                Status = status
            };

            _context.ProjectStatuses.Add(status);
            _context.Users.Add(user);
            _context.TaskItems.Add(task);
            _context.Projects.Add(project);
            _context.UsersProjectTasks.Add(new UsersProjectTask
            {
                ProjectId = projectId,
                UserId = userId,
                TaskId = taskId
            });
            await _context.SaveChangesAsync();

            // Act
            var result = await _repository.GetProjectByIdAsync(projectId);

            // Assert
            result.Should().NotBeNull();
            result.Id.Should().Be(projectId);
            result.ProjectName.Should().Be("Test Project");
            result.Status.Should().NotBeNull();
            result.Status.StatusName.Should().Be("Active");
            result.Users.Should().HaveCount(1);
            result.Tasks.Should().HaveCount(1);
        }

        [Fact]
        public async Task GetProjectByIdAsync_WhenProjectDoesNotExist_ReturnsNull()
        {
            // Arrange
            var projectId = Guid.NewGuid();

            // Act
            var result = await _repository.GetProjectByIdAsync(projectId);

            // Assert
            result.Should().BeNull();
        }

        [Fact]
        public async Task AssignUserToProject_AddsUserProjectTaskAssignment()
        {
            // Arrange
            var assignment = new UsersProjectTask
            {
                ProjectId = Guid.NewGuid(),
                UserId = Guid.NewGuid(),
                TaskId = Guid.NewGuid()
            };

            // Act
            await _repository.AssignUserToProject(assignment);
            await _context.SaveChangesAsync();

            // Assert
            var savedAssignment = await _context.UsersProjectTasks.FirstOrDefaultAsync();
            savedAssignment.Should().NotBeNull();
            savedAssignment.ProjectId.Should().Be(assignment.ProjectId);
            savedAssignment.UserId.Should().Be(assignment.UserId);
        }

        [Fact]
        public async Task UpdateProjectStatus_WhenProjectAndStatusExist_UpdatesStatus()
        {
            // Arrange
            var projectId = Guid.NewGuid();
            var oldStatusId = Guid.NewGuid();
            var newStatusId = Guid.NewGuid();

            var oldStatus = new ProjectStatus { Id = oldStatusId, StatusName = "Active" };
            var newStatus = new ProjectStatus { Id = newStatusId, StatusName = "Completed" };
            var project = new Domain.Project
            {
                Id = projectId,
                ProjectName = "Test Project",
                Description = "Test Description",
                Status = oldStatus
            };

            _context.ProjectStatuses.AddRange(oldStatus, newStatus);
            _context.Projects.Add(project);
            await _context.SaveChangesAsync();

            // Act
            var result = await _repository.UpdateProjectStatus(projectId, newStatusId);
            await _context.SaveChangesAsync();

            // Assert
            result.Should().NotBeNull();
            result.StatusName.Should().Be("Completed");

            var updatedProject = await _context.Projects.FindAsync(projectId);
            updatedProject.Status.Id.Should().Be(newStatusId);
        }

        [Fact]
        public async Task UpdateProjectStatus_WhenProjectDoesNotExist_ThrowsKeyNotFoundException()
        {
            // Arrange
            var projectId = Guid.NewGuid();
            var statusId = Guid.NewGuid();

            var status = new ProjectStatus { Id = statusId, StatusName = "Active" };
            _context.ProjectStatuses.Add(status);
            await _context.SaveChangesAsync();

            // Act & Assert
            await Assert.ThrowsAsync<KeyNotFoundException>(() => 
                _repository.UpdateProjectStatus(projectId, statusId));
        }

        [Fact]
        public async Task DeleteTaskFromProject_RemovesUserTaskAssignments()
        {
            // Arrange
            var projectId = Guid.NewGuid();
            var taskId = Guid.NewGuid();

            _context.UsersProjectTasks.AddRange(
                new UsersProjectTask { ProjectId = projectId, TaskId = taskId, UserId = Guid.NewGuid() },
                new UsersProjectTask { ProjectId = projectId, TaskId = taskId, UserId = Guid.NewGuid() }
            );
            await _context.SaveChangesAsync();

            // Act
            await _repository.DeleteTaskFromProject(projectId, taskId);
            await _context.SaveChangesAsync();

            // Assert
            var remainingAssignments = await _context.UsersProjectTasks
                .Where(upt => upt.ProjectId == projectId && upt.TaskId == taskId)
                .ToListAsync();
            remainingAssignments.Should().BeEmpty();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
