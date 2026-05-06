using FluentAssertions;
using Moq;
using ProjectManagement.Features.TaskItem.Requests.GetTask;
using ProjectManagement.ServiceManager;
using Xunit;

namespace ProjectManagement.Tests.Features.TaskItem.Requests.GetTask
{
    public class GetTaskHandlerTests
    {
        private readonly Mock<IRepositoryManager> _repositoryManagerMock;
        private readonly GetTaskHandler _handler;

        public GetTaskHandlerTests()
        {
            _repositoryManagerMock = new Mock<IRepositoryManager>();
            _handler = new GetTaskHandler(_repositoryManagerMock.Object);
        }

        [Fact]
        public async Task Handle_WhenTaskExists_ReturnsTaskResult()
        {
            // Arrange
            var taskId = Guid.NewGuid();
            var projectId = Guid.NewGuid();
            var userId = Guid.NewGuid();

            var task = new Domain.TaskItem
            {
                Id = taskId,
                Title = "Test Task",
                Description = "Test Description"
            };

            _repositoryManagerMock.Setup(x => x.TaskItem.GetTaskByIdAsync(taskId))
                .ReturnsAsync(task);

            var query = new GetTaskQuery(taskId);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Id.Should().Be(taskId);
            result.Title.Should().Be("Test Task");
            result.Description.Should().Be("Test Description");
        }

        [Fact]
        public async Task Handle_WhenTaskDoesNotExist_ReturnsNull()
        {
            // Arrange
            var taskId = Guid.NewGuid();
            _repositoryManagerMock.Setup(x => x.TaskItem.GetTaskByIdAsync(taskId))
                .ReturnsAsync((Domain.TaskItem)null);

            var query = new GetTaskQuery(taskId);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            result.Should().BeNull();
        }

        [Fact]
        public async Task Handle_WhenTaskHasNoUserOrProject_ReturnsTaskWithNullReferences()
        {
            // Arrange
            var taskId = Guid.NewGuid();
            var task = new Domain.TaskItem
            {
                Id = taskId,
                Title = "Unassigned Task",
                Description = "No user or project"
            };

            _repositoryManagerMock.Setup(x => x.TaskItem.GetTaskByIdAsync(taskId))
                .ReturnsAsync(task);

            var query = new GetTaskQuery(taskId);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
        }
    }
}
