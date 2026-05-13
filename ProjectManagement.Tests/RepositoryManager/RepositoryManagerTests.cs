using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using ProjectManagement.Data;
using ProjectManagement.ServiceManager;
using ProjectManagement.Features.User.Repository;
using ProjectManagement.Features.TaskItem.Repository;
using ProjectManagement.Features.Project.Repository;
using Xunit;

namespace ProjectManagement.Tests.RepositoryManager
{
    public class RepositoryManagerTests : IDisposable
    {
        private readonly DataContext _context;
        private readonly ProjectManagement.RepositoryManager _repositoryManager;

        public RepositoryManagerTests()
        {
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _context = new DataContext(options);
            _repositoryManager = new ProjectManagement.RepositoryManager(_context);
        }

        [Fact]
        public void User_Property_ReturnsUserRepository()
        {
            // Act
            var userRepository = _repositoryManager.User;

            // Assert
            userRepository.Should().NotBeNull();
            userRepository.Should().BeAssignableTo<IUserRepository>();
        }

        [Fact]
        public void TaskItem_Property_ReturnsTaskItemRepository()
        {
            // Act
            var taskRepository = _repositoryManager.TaskItem;

            // Assert
            taskRepository.Should().NotBeNull();
            taskRepository.Should().BeAssignableTo<ITaskItemRepository>();
        }

        [Fact]
        public void Project_Property_ReturnsProjectRepository()
        {
            // Act
            var projectRepository = _repositoryManager.Project;

            // Assert
            projectRepository.Should().NotBeNull();
            projectRepository.Should().BeAssignableTo<IProjectRepository>();
        }

        [Fact]
        public async Task SaveAsync_SavesChangesToDatabase()
        {
            // Arrange
            var user = new Domain.User
            {
                Id = Guid.NewGuid(),
                Name = "Test User",
                Email = "test@example.com"
            };

            _context.Users.Add(user);

            // Act
            await _repositoryManager.SaveAsync();

            // Assert
            var savedUser = await _context.Users.FindAsync(user.Id);
            savedUser.Should().NotBeNull();
            savedUser.Name.Should().Be("Test User");
        }

        [Fact]
        public void User_Property_ReturnsSameInstance()
        {
            // Act
            var userRepository1 = _repositoryManager.User;
            var userRepository2 = _repositoryManager.User;

            // Assert
            userRepository1.Should().BeSameAs(userRepository2);
        }

        [Fact]
        public void TaskItem_Property_ReturnsSameInstance()
        {
            // Act
            var taskRepository1 = _repositoryManager.TaskItem;
            var taskRepository2 = _repositoryManager.TaskItem;

            // Assert
            taskRepository1.Should().BeSameAs(taskRepository2);
        }

        [Fact]
        public void Project_Property_ReturnsSameInstance()
        {
            // Act
            var projectRepository1 = _repositoryManager.Project;
            var projectRepository2 = _repositoryManager.Project;

            // Assert
            projectRepository1.Should().BeSameAs(projectRepository2);
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
