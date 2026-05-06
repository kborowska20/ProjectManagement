using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Moq;
using ProjectManagement.Data;
using ProjectManagement.Domain;
using ProjectManagement.Features.Project.Requests.RemoveTaskItemFromProject;
using ProjectManagement.ServiceManager;
using Xunit;

namespace ProjectManagement.Tests.Features.Project.Requests.RemoveTaskItemFromProject
{
    public class RemoveTaskFromProjectHandlerTests : IDisposable
    {
        private readonly DataContext _context;
        private readonly Mock<IRepositoryManager> _repositoryManagerMock;
        private readonly RemoveTaskFromProjectHandler _handler;

        public RemoveTaskFromProjectHandlerTests()
        {
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _context = new DataContext(options);
            _repositoryManagerMock = new Mock<IRepositoryManager>();
            _handler = new RemoveTaskFromProjectHandler(_repositoryManagerMock.Object, _context);
        }

        [Fact]
        public async Task Handle_WithExistingAssignments_RemovesAllAssignments()
        {
            // Arrange
            var projectId = Guid.NewGuid();
            var taskId = Guid.NewGuid();

            _context.UsersProjectTasks.AddRange(
                new UsersProjectTask { ProjectId = projectId, TaskId = taskId, UserId = Guid.NewGuid() },
                new UsersProjectTask { ProjectId = projectId, TaskId = taskId, UserId = Guid.NewGuid() }
            );
            await _context.SaveChangesAsync();

            var command = new RemoveTaskFromProjectCommand(taskId, projectId);

            // Act
            await _handler.Handle(command, CancellationToken.None);

            // Assert
            var remainingAssignments = await _context.UsersProjectTasks
                .Where(upt => upt.ProjectId == projectId && upt.TaskId == taskId)
                .ToListAsync();
            remainingAssignments.Should().BeEmpty();
        }

        [Fact]
        public async Task Handle_WithNoExistingAssignments_DoesNotThrow()
        {
            // Arrange
            var projectId = Guid.NewGuid();
            var taskId = Guid.NewGuid();
            var command = new RemoveTaskFromProjectCommand(taskId, projectId);

            // Act
            Func<Task> act = async () => await _handler.Handle(command, CancellationToken.None);

            // Assert
            await act.Should().NotThrowAsync();
        }

        [Fact]
        public async Task Handle_OnlyRemovesMatchingAssignments()
        {
            // Arrange
            var projectId = Guid.NewGuid();
            var taskId = Guid.NewGuid();
            var otherTaskId = Guid.NewGuid();

            _context.UsersProjectTasks.AddRange(
                new UsersProjectTask { ProjectId = projectId, TaskId = taskId, UserId = Guid.NewGuid() },
                new UsersProjectTask { ProjectId = projectId, TaskId = otherTaskId, UserId = Guid.NewGuid() }
            );
            await _context.SaveChangesAsync();

            var command = new RemoveTaskFromProjectCommand(taskId, projectId);

            // Act
            await _handler.Handle(command, CancellationToken.None);

            // Assert
            var remainingAssignments = await _context.UsersProjectTasks.ToListAsync();
            remainingAssignments.Should().HaveCount(1);
            remainingAssignments.First().TaskId.Should().Be(otherTaskId);
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
