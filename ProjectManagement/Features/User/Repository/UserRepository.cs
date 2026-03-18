using Microsoft.EntityFrameworkCore;
using ProjectManagement.Data;
using ProjectManagement.Domain;

namespace ProjectManagement.Features.User.Repository
{
    public class UserRepository(DataContext context) : IUserRepository
    {
        private readonly DataContext _context = context;

        public async Task<Domain.User?> GetUserByIdAsync(Guid userId)
        {
            return await _context.Users
                .FirstOrDefaultAsync(x => x.Id == userId);
        }

        public async Task CreateUserAsync(Domain.User? user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public async Task<UserRole> UpdateUserRoleAsync(Guid userId, Guid roleId)
        {
            var user = await _context.Users.FindAsync(userId);

            if (user == null)
            {
                throw new KeyNotFoundException($"User with ID {userId} not found");
            }

            var role = await _context.UserRoles.FindAsync(roleId);

            if (role == null)
            {
                throw new KeyNotFoundException($"UserRoleId with ID {roleId} not found");
            }

            user.UserRoleId = role.Id;
            // Assign the role's Id (Guid) to the user's UserRoleId property
            await _context.SaveChangesAsync();

            return role;
        }
    }
}
    