using ProjectManagement.Domain;

namespace ProjectManagement.Features.User.Repository
{
    public interface IUserRepository
    {
        Task<Domain.User?> GetUserByIdAsync(Guid userId);
        Task CreateUserAsync(Domain.User? user);
        Task<Task> UpdateUserAsync(Domain.User? user);
        Task UpdateUserRoleAsync(Guid userId, UserRole role);
        void AddUserToProject(UsersProjectTask usersProjectTask);
    }
}
