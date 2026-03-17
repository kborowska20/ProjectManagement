using ProjectManagement.Domain;

namespace ProjectManagement.Features.Project.Repository
{
    public interface IProjectRepository
    {
        Task<Domain.Project> GetProjectByIdAsync(Guid projectId);
        Task AssignUserToProject(UsersProjectTask usersProjectTask);
        Task<ProjectStatus> UpdateProjectStatus(Guid projectId, Guid statusId);
        Task DeleteTaskFromProject(Guid projectId, Guid taskId);
    }
}
