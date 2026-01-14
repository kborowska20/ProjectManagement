using Microsoft.EntityFrameworkCore;
using ProjectManagement.Data;
using ProjectManagement.Domain;

namespace ProjectManagement.Features.User.Repository
{
    public class UserRepository(DataContext context) : IUserRepository
    {
        private readonly DataContext _context = context;

        public async Task<Domain.User?> GetUserByIdAsync(int userId)
        {
            return await _context.Users.FirstOrDefaultAsync(x => x != null && x.Id == userId);
        }

        public async Task CreateUserAsync(Domain.User? user)
        { 
            await _context.Users.AddAsync(user);
        }

        public Task UpdateUserAsync(Domain.User? user)
        {
            _context.Users.Update(user);
            return Task.CompletedTask;
        }

        public Task UpdateUserRoleAsync(int userId, UserRole role)
        {
            return _context.Users
                .Where(x => x.Id == userId)
                .ExecuteUpdateAsync(setters => setters
                    .SetProperty(u => u.RoleId, role.Id)
                );
        }
    }
}
