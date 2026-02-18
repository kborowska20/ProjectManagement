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

        public async Task<Task> UpdateUserAsync(Domain.User? user)
        {
            var userFind = await _context.Users.FindAsync(user);
            ArgumentNullException.ThrowIfNull(userFind);
            _context.Users.Update(userFind);
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

        public async void AddUserToProject(UsersProjectTask usersProjectTask)
        {
            var project = await _context.Projects.FindAsync(usersProjectTask.ProjectId);
            var user = await _context.Users.FindAsync(usersProjectTask.UserId);
            if (project != null&& user !=  null)
            {
                await _context.UserTaskItems.AddAsync(usersProjectTask);
                await _context.SaveChangesAsync();
            }
        }
    }
}
