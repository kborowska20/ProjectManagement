using FluentAssertions;
using Moq;
using ProjectManagement.Domain;
using ProjectManagement.Features.User.Requests.UpdateUserRole;
using ProjectManagement.ServiceManager;
using Xunit;

namespace ProjectManagement.Tests.Features.User.Requests.UpdateUserRole
{
    public class UpdateUserRoleHandlerTests
    {
        private readonly Mock<IRepositoryManager> _repositoryManagerMock;
        private readonly UpdateUserRoleHandler _handler;

        public UpdateUserRoleHandlerTests()
        {
            _repositoryManagerMock = new Mock<IRepositoryManager>();
            _handler = new UpdateUserRoleHandler(_repositoryManagerMock.Object);
        }

        [Fact]
        public async Task Handle_WhenUserExists_UpdatesRoleSuccessfully()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var roleId = Guid.NewGuid();
            var user = new Domain.User { Id = userId, Name = "John Doe" };
            var role = new UserRole { Id = roleId, RoleName = "Manager" };

            _repositoryManagerMock.Setup(x => x.User.GetUserByIdAsync(userId))
                .ReturnsAsync(user);
            _repositoryManagerMock.Setup(x => x.User.UpdateUserRoleAsync(userId, roleId))
                .ReturnsAsync(role);
            _repositoryManagerMock.Setup(x => x.SaveAsync()).Returns(Task.CompletedTask);

            var command = new UpdateUserRoleCommand(userId, roleId);

            // Act
            await _handler.Handle(command, CancellationToken.None);

            // Assert
            _repositoryManagerMock.Verify(x => x.User.GetUserByIdAsync(userId), Times.Once);
            _repositoryManagerMock.Verify(x => x.User.UpdateUserRoleAsync(userId, roleId), Times.Once);
            _repositoryManagerMock.Verify(x => x.SaveAsync(), Times.Once);
        }

        [Fact]
        public async Task Handle_WhenUserDoesNotExist_ThrowsKeyNotFoundException()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var roleId = Guid.NewGuid();

            _repositoryManagerMock.Setup(x => x.User.GetUserByIdAsync(userId))
                .ReturnsAsync((Domain.User)null);

            var command = new UpdateUserRoleCommand(userId, roleId);

            // Act & Assert
            await Assert.ThrowsAsync<KeyNotFoundException>(() => _handler.Handle(command, CancellationToken.None));
            _repositoryManagerMock.Verify(x => x.SaveAsync(), Times.Never);
        }
    }
}
