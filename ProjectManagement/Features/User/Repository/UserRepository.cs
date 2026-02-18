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

        public Task UpdateUserAsync(Domain.User? user)
        {
            _context.Users.Update(user);
            return Task.CompletedTask;
        }

        public async Task UpdateUserRoleAsync(Guid userId, UserRole role)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user != null)
            {
                user.Role = role;
                await _context.SaveChangesAsync();
            }
        }

        public void AddUserToProject(Guid userId, Guid projectId)
        {
            
        }
    }
}
