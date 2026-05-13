using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using ProjectManagement.Features.Project;
using ProjectManagement.Features.Project.Requests.AddUserToProject;
using ProjectManagement.Features.Project.Requests.GetProject;
using ProjectManagement.Features.Project.Requests.RemoveTaskItemFromProject;
using ProjectManagement.Features.Project.Requests.UpdateProjectStatus;
using Xunit;

namespace ProjectManagement.Tests.Features.Project
{
    public class ProjectControllerTests
    {
        private readonly Mock<IMediator> _mediatorMock;
        private readonly ProjectController _controller;

        public ProjectControllerTests()
        {
            _mediatorMock = new Mock<IMediator>();
            _controller = new ProjectController(_mediatorMock.Object);
        }

        [Fact]
        public async Task GetProject_WhenProjectExists_ReturnsOkWithProject()
        {
            // Arrange
            var projectId = Guid.NewGuid();
            var projectResult = new GetProjectResult(
                projectId,
                "Test Project",
                "Description",
                new Domain.ProjectStatus { Id = Guid.NewGuid(), StatusName = "Active" },
                new List<Domain.User>(),
                new List<Domain.TaskItem>()
            );

            _mediatorMock.Setup(x => x.Send(It.IsAny<GetProjectQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(projectResult);

            // Act
            var result = await _controller.GetProject(projectId);

            // Assert
            var okResult = result.Result.Should().BeOfType<OkObjectResult>().Subject;
            okResult.Value.Should().Be(projectResult);
        }

        [Fact]
        public async Task GetProject_WhenProjectDoesNotExist_ReturnsNotFound()
        {
            // Arrange
            var projectId = Guid.NewGuid();
            _mediatorMock.Setup(x => x.Send(It.IsAny<GetProjectQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync((GetProjectResult)null);

            // Act
            var result = await _controller.GetProject(projectId);

            // Assert
            result.Result.Should().BeOfType<NotFoundObjectResult>();
        }

        [Fact]
        public async Task AssignUserToProject_WhenSuccessful_ReturnsOk()
        {
            // Arrange
            var command = new AddUserToProjectCommand(Guid.NewGuid(), Guid.NewGuid());
            var expectedResult = new AddUserToProjectResult(Guid.NewGuid(), Guid.NewGuid());

            _mediatorMock.Setup(x => x.Send(command, It.IsAny<CancellationToken>()))
                .ReturnsAsync(expectedResult);

            // Act
            var result = await _controller.AssignUserToProject(command);

            // Assert
            var okResult = result.Result.Should().BeOfType<OkObjectResult>().Subject;
            okResult.Value.Should().Be(expectedResult);
        }

        [Fact]
        public async Task AssignUserToProject_WhenProjectNotFound_ReturnsNotFound()
        {
            // Arrange
            var command = new AddUserToProjectCommand(Guid.NewGuid(), Guid.NewGuid());
            _mediatorMock.Setup(x => x.Send(command, It.IsAny<CancellationToken>()))
                .ReturnsAsync((AddUserToProjectResult)null);

            // Act
            var result = await _controller.AssignUserToProject(command);

            // Assert
            result.Result.Should().BeOfType<NotFoundObjectResult>();
        }

        [Fact]
        public async Task UpdateProjectStatus_WhenSuccessful_ReturnsNoContent()
        {
            // Arrange
            var command = new UpdateProjectStatusCommand(Guid.NewGuid(), Guid.NewGuid());
            _mediatorMock.Setup(x => x.Send(command, It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);

            // Act
            var result = await _controller.UpdateProjectStatus(command);

            // Assert
            result.Should().BeOfType<NoContentResult>();
        }

        [Fact]
        public async Task DeleteTaskFromProject_WhenSuccessful_ReturnsNoContent()
        {
            // Arrange
            var projectId = Guid.NewGuid();
            var taskId = Guid.NewGuid();
            _mediatorMock.Setup(x => x.Send(It.IsAny<RemoveTaskFromProjectCommand>(), It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);

            // Act
            var result = await _controller.DeleteTaskFromProject(projectId, taskId);

            // Assert
            result.Should().BeOfType<NoContentResult>();
            _mediatorMock.Verify(x => x.Send(
                It.Is<RemoveTaskFromProjectCommand>(c => c.ProjectId == projectId && c.TaskId == taskId),
                It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}
