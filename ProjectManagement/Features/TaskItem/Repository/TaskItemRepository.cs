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
            var task = await _context.Tasks.FindAsync(usersProjectTask.TaskId);
            var project = await _context.Projects.FindAsync(usersProjectTask.ProjectId);

            if (task != null && project is not null)
            {
                await _context.UserTaskItems.AddAsync(usersProjectTask);
                await _context.SaveChangesAsync();
            }
        }

        public async Task AssignTaskToUser(UsersProjectTask usersProjectTask)
        {
            var task = await _context.Tasks.FindAsync(usersProjectTask.TaskId);
            var user = await _context.Users.FindAsync(usersProjectTask.ProjectId);

            if (task != null && user is not null)
            {
                await _context.UserTaskItems.AddAsync(usersProjectTask);
                await _context.SaveChangesAsync();
            }
        }

        public async Task CreateTaskAsync(Domain.TaskItem? task)
        {
            if (task != null)
            {
                await _context.Tasks.AddAsync(task);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<Domain.TaskItem?> GetTaskByIdAsync(Guid taskId)
        {
            var t = await _context.Tasks
                .FirstOrDefaultAsync(x => x != null && x.Id == taskId);
            ArgumentNullException.ThrowIfNull(t);
            return t;
        }

    }
}
