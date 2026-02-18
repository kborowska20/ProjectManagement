using ProjectManagement.Domain;

namespace ProjectManagement.Features.Project.Repository
{
    public interface IProjectRepository
    {
        Task<Domain.Project> GetProjectByIdAsync(Guid projectId);
        Task AssignUserToProject(UsersProjectTask usersProjectTask);
        Task UpdateProjectStatus(Guid projectId, ProjectStatus status);
    }
}
