using AutoMapper;
using FluentAssertions;
using Moq;
using ProjectManagement.Features.Project.Repository;
using ProjectManagement.Features.Project.Requests.GetProject;
using ProjectManagement.ServiceManager;
using Xunit;

namespace ProjectManagement.Tests.Features.Project.Requests.GetProject
{
    public class GetProjectHandlerTests
    {
        private readonly Mock<IRepositoryManager> _repositoryManagerMock;
        private readonly Mock<IProjectRepository> _projectRepositoryMock;
        private readonly GetProjectHandler _handler;
        private readonly Mock<IMapper> _mapperMock;

        public GetProjectHandlerTests()
        {
            _repositoryManagerMock = new Mock<IRepositoryManager>();
            _projectRepositoryMock = new Mock<IProjectRepository>();
            _repositoryManagerMock.Setup(x => x.Project).Returns(_projectRepositoryMock.Object);
            _mapperMock = new Mock<IMapper>();
            _handler = new GetProjectHandler(_repositoryManagerMock.Object, _mapperMock.Object);
        }

        [Fact]
        public async Task Handle_WhenProjectExists_ReturnsGetProjectResult()
        {
            // Arrange
            var projectId = Guid.NewGuid();
            var project = new Domain.Project
            {
                Id = projectId,
                ProjectName = "Test Project",
                Description = "Test Description",
                Status = new Domain.ProjectStatus { Id = Guid.NewGuid(), StatusName = "Active" },
                Users = new List<Domain.User>
                {
                    new Domain.User { Id = Guid.NewGuid(), Name = "User 1", Email = "user1@test.com" }
                },
                Tasks = new List<Domain.TaskItem>
                {
                    new Domain.TaskItem { Id = Guid.NewGuid(), Title = "Task 1", Description = "Description 1" }
                }
            };

            _projectRepositoryMock.Setup(x => x.GetProjectByIdAsync(projectId))
                .ReturnsAsync(project);

            var query = new GetProjectQuery(projectId);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Id.Should().Be(projectId);
            result.ProjectName.Should().Be("Test Project");
            result.Description.Should().Be("Test Description");
            result.Status.Should().NotBeNull();
            result.Status.StatusName.Should().Be("Active");
            result.Users.Should().HaveCount(1);
            result.Tasks.Should().HaveCount(1);
        }

        [Fact]
        public async Task Handle_WhenProjectDoesNotExist_ReturnsNull()
        {
            // Arrange
            var projectId = Guid.NewGuid();
            _projectRepositoryMock.Setup(x => x.GetProjectByIdAsync(projectId))
                .ReturnsAsync((Domain.Project)null);

            var query = new GetProjectQuery(projectId);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            result.Should().BeNull();
        }

        [Fact]
        public async Task Handle_WhenProjectHasNoUsersOrTasks_ReturnsProjectWithEmptyCollections()
        {
            // Arrange
            var projectId = Guid.NewGuid();
            var project = new Domain.Project
            {
                Id = projectId,
                ProjectName = "Empty Project",
                Description = "No users or tasks",
                Status = new Domain.ProjectStatus { Id = Guid.NewGuid(), StatusName = "Active" },
                Users = new List<Domain.User>(),
                Tasks = new List<Domain.TaskItem>()
            };

            _projectRepositoryMock.Setup(x => x.GetProjectByIdAsync(projectId))
                .ReturnsAsync(project);

            var query = new GetProjectQuery(projectId);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Users.Should().BeEmpty();
            result.Tasks.Should().BeEmpty();
        }
    }
}
