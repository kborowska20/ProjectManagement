using ProjectManagement.Domain;

namespace ProjectManagement.Features.Project.Repository
{
    public interface IProjectRepository
    {
        Task<IEnumerable<Domain.Project>> GetAllConsolesAsync();
        Task<Domain.Project> GetProjectByIdAsync(Guid projectId);
        Task CreateProjectAsync(Domain.Project project);
        Task UpdateProjectAsync(Domain.Project project);
        Task DeleteProjectAsync(int projectId);
        Task AssignUserToProject(int projectId, int userId);
        Task UpdateProjectStatus(int projectId, ProjectStatus status);
    }
}
