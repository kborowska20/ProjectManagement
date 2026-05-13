using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Moq;
using ProjectManagement.Data;
using ProjectManagement.Domain;
using ProjectManagement.Features.Project.Repository;
using ProjectManagement.Features.Project.Requests.RemoveTaskItemFromProject;
using ProjectManagement.ServiceManager;
using Xunit;

namespace ProjectManagement.Tests.Features.Project.Requests.RemoveTaskItemFromProject
{
    public class RemoveTaskFromProjectHandlerTests : IDisposable
    {
        private readonly DataContext _context;
        private readonly Mock<IRepositoryManager> _repositoryManagerMock;
        private readonly Mock<IProjectRepository> _projectRepositoryMock;
        private readonly RemoveTaskFromProjectHandler _handler;

        public RemoveTaskFromProjectHandlerTests()
        {
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _context = new DataContext(options);
            _repositoryManagerMock = new Mock<IRepositoryManager>();
            _projectRepositoryMock = new Mock<IProjectRepository>();
            _repositoryManagerMock.Setup(x => x.Project).Returns(_projectRepositoryMock.Object);
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

            var command = new RemoveTaskFromProjectCommand(projectId, taskId);

            _projectRepositoryMock.Setup(x => x.DeleteTaskFromProject(projectId, taskId))
                .Returns(Task.CompletedTask);
            _repositoryManagerMock.Setup(x => x.SaveAsync()).Returns(Task.CompletedTask);

            // Act
            await _handler.Handle(command, CancellationToken.None);

            // Assert
            _projectRepositoryMock.Verify(x => x.DeleteTaskFromProject(projectId, taskId), Times.Once);
            _repositoryManagerMock.Verify(x => x.SaveAsync(), Times.Once);
        }

        [Fact]
        public async Task Handle_WithNoExistingAssignments_DoesNotThrow()
        {
            // Arrange
            var projectId = Guid.NewGuid();
            var taskId = Guid.NewGuid();
            var command = new RemoveTaskFromProjectCommand(projectId, taskId);

            _projectRepositoryMock.Setup(x => x.DeleteTaskFromProject(projectId, taskId))
                .Returns(Task.CompletedTask);
            _repositoryManagerMock.Setup(x => x.SaveAsync()).Returns(Task.CompletedTask);

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

            var command = new RemoveTaskFromProjectCommand(projectId, taskId);

            _projectRepositoryMock.Setup(x => x.DeleteTaskFromProject(projectId, taskId))
                .Returns(Task.CompletedTask);
            _repositoryManagerMock.Setup(x => x.SaveAsync()).Returns(Task.CompletedTask);

            // Act
            await _handler.Handle(command, CancellationToken.None);

            // Assert
            _projectRepositoryMock.Verify(x => x.DeleteTaskFromProject(projectId, taskId), Times.Once);
            _repositoryManagerMock.Verify(x => x.SaveAsync(), Times.Once);
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
