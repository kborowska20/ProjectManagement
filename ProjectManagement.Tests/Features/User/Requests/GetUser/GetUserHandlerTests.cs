using AutoMapper;
using FluentAssertions;
using Moq;
using ProjectManagement.Domain;
using ProjectManagement.Features.User.Requests.GetUser;
using ProjectManagement.ServiceManager;
using Xunit;

namespace ProjectManagement.Tests.Features.User.Requests.GetUser
{
    public class GetUserHandlerTests
    {
        private readonly Mock<IRepositoryManager> _repositoryManagerMock;
        private readonly GetUserHandler _handler;
        private readonly Mock<IMapper> _mapperMock;

        public GetUserHandlerTests()
        {
            _repositoryManagerMock = new Mock<IRepositoryManager>();
            _mapperMock = new Mock<IMapper>();
            _handler = new GetUserHandler(_repositoryManagerMock.Object, _mapperMock.Object);
        }

        [Fact]
        public async Task Handle_WhenUserExists_ReturnsUser()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var roleId = Guid.NewGuid();
            var user = new Domain.User
            {
                Id = userId,
                Name = "John Doe",
                Email = "john@example.com",
                UserRole = new UserRole { Id = roleId, RoleName = "Admin" }
            };

            _repositoryManagerMock.Setup(x => x.User.GetUserByIdAsync(userId))
                .ReturnsAsync(user);

            var query = new GetUserQuery(userId);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Id.Should().Be(userId);
            result.Name.Should().Be("John Doe");
            result.Email.Should().Be("john@example.com");
        }

        [Fact]
        public async Task Handle_WhenUserDoesNotExist_ReturnsNull()
        {
            // Arrange
            var userId = Guid.NewGuid();
            _repositoryManagerMock.Setup(x => x.User.GetUserByIdAsync(userId))
                .ReturnsAsync((Domain.User)null);

            var query = new GetUserQuery(userId);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            result.Should().BeNull();
        }
    }
}
