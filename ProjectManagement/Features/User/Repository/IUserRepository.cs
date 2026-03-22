using ProjectManagement.Domain;
using ProjectManagement.Features.User.Requests.CreateUser;

namespace ProjectManagement.Features.User.Repository
{
    public interface IUserRepository
    {
        Task<Domain.User?> GetUserByIdAsync(Guid userId);
        Task<Domain.User> CreateUserAsync(CreateUserCommand user);
        Task<UserRole> UpdateUserRoleAsync(Guid userId, Guid roleId);
    }
}
