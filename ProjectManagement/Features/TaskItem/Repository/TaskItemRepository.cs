using Microsoft.EntityFrameworkCore;
using ProjectManagement.Data;
using ProjectManagement.Domain;
using System.Threading.Tasks;

namespace ProjectManagement.Features.TaskItem.Repository
{
    public class TaskItemRepository(DataContext context) : ITaskItemRepository
    {
        private readonly DataContext _context = context;

        public async Task AssignTaskToProject(UsersProjectTask usersProjectTask)
        {
            var task = await _context.TaskItems.FindAsync(usersProjectTask.TaskId);
            var project = await _context.Projects.FindAsync(usersProjectTask.ProjectId);

            if (task != null && project is not null)
            {
                try
                {
                    task.ProjectId = project.Id;
                    _context.TaskItems.Update(task);
                    await _context.SaveChangesAsync();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }

            }
        }

        public async Task AssignTaskToUser(UsersProjectTask usersProjectTask)
        {
            var task = await _context.TaskItems.FindAsync(usersProjectTask.TaskId);
            var user = await _context.Users.FindAsync(usersProjectTask.UserId);
            if (task != null && user is not null)
            {
                try
                {
                    task.AssignedUserId = user.Id;
                    _context.TaskItems.Update(task);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateException ex)
                {
                    // Handle database update exceptions
                    Console.WriteLine($"Database update error: {ex.Message}");
                }
            }
        }

        public async Task CreateTaskAsync(Domain.TaskItem? task)
        {
            if (task != null)
            {
                await _context.TaskItems.AddAsync(task);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<Domain.TaskItem?> GetTaskByIdAsync(Guid taskId)
        {
            var t = await _context.TaskItems
                .FirstOrDefaultAsync(x => x != null && x.Id == taskId);
            ArgumentNullException.ThrowIfNull(t);
            return t;
        }

    }
}
