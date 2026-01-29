using ProjectManagement.Domain;

namespace ProjectManagement.Features.User.Repository
{
    public interface IUserRepository
    {
        Task<Domain.User?> GetUserByIdAsync(Guid userId);
        Task CreateUserAsync(Domain.User? user);
        Task UpdateUserAsync(Domain.User? user);
        Task UpdateUserRoleAsync(Guid userId, Guid roleId);
    }
}
