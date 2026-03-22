using MediatR;
using Microsoft.EntityFrameworkCore;
using ProjectManagement.Data;
using ProjectManagement.Domain;
using ProjectManagement.Features.User.Requests.CreateUser;

namespace ProjectManagement.Features.User.Repository
{
    public class UserRepository(DataContext context) : IUserRepository
    {
        private readonly DataContext _context = context;

        public async Task<Domain.User?> GetUserByIdAsync(Guid userId)
        {
            return await _context.Users
                .Include(x => x.UserRole)
                .FirstOrDefaultAsync(x => x.Id == userId);
        }

        public async Task<Domain.User> CreateUserAsync(CreateUserCommand command)
        {
            if (command == null)
            {
                throw new ArgumentNullException(nameof(command));
            }

            var userRole = _context.UserRoles.FirstOrDefault(x => x.Id == command.UserRoleId);

            ArgumentNullException.ThrowIfNull(userRole);

            var user = new Domain.User
            {
                Id = Guid.NewGuid(),
                Name = command.Name,
                Email = command.Email,
                UserRole = userRole
            };
            
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return user;
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

            user.UserRole = role;
            // Assign the role's Id (Guid) to the user's UserRoleId property
            await _context.SaveChangesAsync();

            return role;
        }
    }
}
    