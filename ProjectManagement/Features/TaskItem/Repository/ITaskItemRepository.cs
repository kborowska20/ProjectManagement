using ProjectManagement.Domain;

namespace ProjectManagement.Features.TaskItem.Repository
{
    public interface ITaskItemRepository
    {
        Task<Domain.TaskItem?> GetTaskByIdAsync(Guid taskId);
        Task CreateTaskAsync(Domain.TaskItem? task);
        Task AssignTaskToProject(UsersProjectTask usersProjectTask);
        Task AssignTaskToUser(UsersProjectTask usersProjectTask);
    }
}
