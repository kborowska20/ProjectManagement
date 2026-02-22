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
            return await _context.Users.FirstOrDefaultAsync(x => x != null && x.Id == userId);
        }

        public async Task CreateUserAsync(Domain.User? user)
        { 
            await _context.Users.AddAsync(user);
        }

        public async Task<UserRole> UpdateUserRoleAsync(Guid userId, Guid roleId)
        {
            var user = await _context.Users.FindAsync(userId);

            var role = await _context.UserRoles.FindAsync(roleId);
            if (role == null)
            {
                throw new ArgumentNullException("role not found");
            }
            if (user != null && role != null)
            {
                user.Role = role;
                await _context.SaveChangesAsync();
            }
            return role;
        }
    }
}
