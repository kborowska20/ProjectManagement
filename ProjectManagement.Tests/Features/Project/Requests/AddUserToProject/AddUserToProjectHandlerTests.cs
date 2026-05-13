using AutoMapper;
using FluentAssertions;
using Moq;
using ProjectManagement.Domain;
using ProjectManagement.Features.Project.Requests.AddUserToProject;
using ProjectManagement.ServiceManager;
using Xunit;

namespace ProjectManagement.Tests.Features.Project.Requests.AddUserToProject
{
    public class AddUserToProjectHandlerTests
    {
        private readonly Mock<IRepositoryManager> _repositoryManagerMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly AddUserToProjectHandler _handler;

        public AddUserToProjectHandlerTests()
        {
            _repositoryManagerMock = new Mock<IRepositoryManager>();
            _mapperMock = new Mock<IMapper>();
            _handler = new AddUserToProjectHandler(_repositoryManagerMock.Object, _mapperMock.Object);
        }

        [Fact]
        public async Task Handle_WithValidCommand_AssignsUserToProject()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var projectId = Guid.NewGuid();
            var command = new AddUserToProjectCommand(userId, projectId);

            var project = new Domain.Project { Id = projectId, ProjectName = "Test Project", Description = "Test" };
            var user = new Domain.User { Id = userId, Name = "Test User", Email = "test@example.com" };
            var expectedResult = new AddUserToProjectResult(userId, projectId);

            _repositoryManagerMock.Setup(x => x.Project.GetProjectByIdAsync(projectId))
                .ReturnsAsync(project);
            _repositoryManagerMock.Setup(x => x.User.GetUserByIdAsync(userId))
                .ReturnsAsync(user);
            _repositoryManagerMock.Setup(x => x.Project.AssignUserToProject(It.IsAny<UsersProjectTask>()))
                .Returns(Task.CompletedTask);
            _repositoryManagerMock.Setup(x => x.SaveAsync()).Returns(Task.CompletedTask);
            _mapperMock.Setup(x => x.Map<AddUserToProjectResult>(It.IsAny<UsersProjectTask>()))
                .Returns(expectedResult);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.UserId.Should().Be(userId);
            result.ProjectId.Should().Be(projectId);

            _repositoryManagerMock.Verify(x => x.Project.AssignUserToProject(
                It.Is<UsersProjectTask>(upt => upt.UserId == userId && upt.ProjectId == projectId)), Times.Once);
            _repositoryManagerMock.Verify(x => x.SaveAsync(), Times.Once);
        }

        [Fact]
        public async Task Handle_CreatesUsersProjectTaskWithNullTaskId()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var projectId = Guid.NewGuid();
            var command = new AddUserToProjectCommand(userId, projectId);

            var project = new Domain.Project { Id = projectId, ProjectName = "Test Project", Description = "Test" };
            var user = new Domain.User { Id = userId, Name = "Test User", Email = "test@example.com" };

            UsersProjectTask capturedTask = null;
            _repositoryManagerMock.Setup(x => x.Project.GetProjectByIdAsync(projectId))
                .ReturnsAsync(project);
            _repositoryManagerMock.Setup(x => x.User.GetUserByIdAsync(userId))
                .ReturnsAsync(user);
            _repositoryManagerMock.Setup(x => x.Project.AssignUserToProject(It.IsAny<UsersProjectTask>()))
                .Callback<UsersProjectTask>(upt => capturedTask = upt)
                .Returns(Task.CompletedTask);
            _repositoryManagerMock.Setup(x => x.SaveAsync()).Returns(Task.CompletedTask);

            // Act
            await _handler.Handle(command, CancellationToken.None);

            // Assert
            capturedTask.Should().NotBeNull();
            capturedTask.TaskId.Should().BeNull();
        }
    }
}
