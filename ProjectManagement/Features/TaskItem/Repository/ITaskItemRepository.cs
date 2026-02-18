using ProjectManagement.Domain;

namespace ProjectManagement.Features.TaskItem.Repository
{
    public interface ITaskItemRepository
    {
        Task<Domain.TaskItem?> GetTaskByIdAsync(Guid taskId);
        Task CreateTaskAsync(Domain.User? user);
        Task AssignTaskToProject(UsersProjectTask usersProjectTask);
        Task AssignTaskToUser(Guid taskId, Guid userId);
    }
}
