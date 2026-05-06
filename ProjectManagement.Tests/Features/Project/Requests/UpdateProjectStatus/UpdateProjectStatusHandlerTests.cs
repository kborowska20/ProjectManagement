using FluentAssertions;
using Moq;
using ProjectManagement.Domain;
using ProjectManagement.Features.Project.Requests.UpdateProjectStatus;
using ProjectManagement.ServiceManager;
using Xunit;

namespace ProjectManagement.Tests.Features.Project.Requests.UpdateProjectStatus
{
    public class UpdateProjectStatusHandlerTests
    {
        private readonly Mock<IRepositoryManager> _repositoryManagerMock;
        private readonly UpdateProjectStatusHandler _handler;

        public UpdateProjectStatusHandlerTests()
        {
            _repositoryManagerMock = new Mock<IRepositoryManager>();
            _handler = new UpdateProjectStatusHandler(_repositoryManagerMock.Object);
        }

        [Fact]
        public async Task Handle_WhenProjectExists_UpdatesStatusSuccessfully()
        {
            // Arrange
            var projectId = Guid.NewGuid();
            var statusId = Guid.NewGuid();
            var project = new Domain.Project { Id = projectId, ProjectName = "Test Project" };
            var status = new ProjectStatus { Id = statusId, StatusName = "Completed" };

            _repositoryManagerMock.Setup(x => x.Project.GetProjectByIdAsync(projectId))
                .ReturnsAsync(project);
            _repositoryManagerMock.Setup(x => x.Project.UpdateProjectStatus(projectId, statusId))
                .ReturnsAsync(status);
            _repositoryManagerMock.Setup(x => x.SaveAsync()).Returns(Task.CompletedTask);

            var command = new UpdateProjectStatusCommand(projectId, statusId);

            // Act
            await _handler.Handle(command, CancellationToken.None);

            // Assert
            _repositoryManagerMock.Verify(x => x.Project.GetProjectByIdAsync(projectId), Times.Once);
            _repositoryManagerMock.Verify(x => x.Project.UpdateProjectStatus(projectId, statusId), Times.Once);
            _repositoryManagerMock.Verify(x => x.SaveAsync(), Times.Once);
        }

        [Fact]
        public async Task Handle_WhenProjectDoesNotExist_ThrowsKeyNotFoundException()
        {
            // Arrange
            var projectId = Guid.NewGuid();
            var statusId = Guid.NewGuid();

            _repositoryManagerMock.Setup(x => x.Project.GetProjectByIdAsync(projectId))
                .ReturnsAsync((Domain.Project)null);

            var command = new UpdateProjectStatusCommand(projectId, statusId);

            // Act & Assert
            await Assert.ThrowsAsync<KeyNotFoundException>(() => _handler.Handle(command, CancellationToken.None));
            _repositoryManagerMock.Verify(x => x.SaveAsync(), Times.Never);
        }
    }
}
