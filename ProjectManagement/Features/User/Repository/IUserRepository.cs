using ProjectManagement.Domain;

namespace ProjectManagement.Features.User.Repository
{
    public interface IUserRepository
    {
        Task<Domain.User?> GetUserByIdAsync(int userId);
        Task CreateUserAsync(Domain.User? user);
        Task UpdateUserAsync(Domain.User? user);
        Task UpdateUserRoleAsync(int userId, Domain.UserRole role);
    }
}
