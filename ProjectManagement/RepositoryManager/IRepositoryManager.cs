using ProjectManagement.Features.Project.Repository;
using ProjectManagement.Features.User.Repository;

namespace ProjectManagement.ServiceManager
{
    public interface IRepositoryManager
    {
        IUserRepository User { get; }
        IProjectRepository Project { get; }
        Task SaveAsync();
    }
}
