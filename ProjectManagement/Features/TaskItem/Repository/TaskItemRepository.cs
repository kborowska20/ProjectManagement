using Microsoft.EntityFrameworkCore;
using ProjectManagement.Data;
using ProjectManagement.Domain;

namespace ProjectManagement.Features.TaskItem.Repository
{
    public class TaskItemRepository(DataContext context) : ITaskItemRepository
    {
        private readonly DataContext _context = context;

        public async Task AssignTaskToProject(UsersProjectTask usersProjectTask)
        {
            await _context.UserTaskItems.AddAsync(usersProjectTask);
            await _context.SaveChangesAsync();
        }

        public Task AssignTaskToUser(Guid taskId, Guid userId)
        {
            throw new NotImplementedException();
        }

        public Task CreateTaskAsync(Domain.User? user)
        {
            throw new NotImplementedException();
        }

        public Task<Domain.TaskItem?> GetTaskByIdAsync(Guid taskId)
        {
            throw new NotImplementedException();
        }

        public async Task<Domain.TaskItem?> GetUserByIdAsync(Guid taskId)
        {
            return await _context.Tasks.FirstOrDefaultAsync(x => x != null && x.Id == taskId);
        }

    }
}
