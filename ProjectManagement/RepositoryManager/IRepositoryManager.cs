using ProjectManagement.Features.Project.Repository;
using ProjectManagement.Features.TaskItem.Repository;
using ProjectManagement.Features.User.Repository;

namespace ProjectManagement.ServiceManager
{
    public interface IRepositoryManager
    {
        IUserRepository User { get; }
        ITaskItemRepository TaskItem { get; }
        IProjectRepository Project { get; }
        Task SaveAsync();
    }
}
