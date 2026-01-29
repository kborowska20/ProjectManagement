using ProjectManagement.Domain;

namespace ProjectManagement.Features.Project.Repository
{
    public class ProjectRepository : IProjectRepository
    {
        public Task<IEnumerable<Domain.Project>> GetAllConsolesAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Domain.Project> GetProjectByIdAsync(int projectId)
        {
            throw new NotImplementedException();
        }

        public Task CreateProjectAsync(Domain.Project project)
        {
            throw new NotImplementedException();
        }

        public Task UpdateProjectAsync(Domain.Project project)
        {
            throw new NotImplementedException();
        }

        public Task DeleteProjectAsync(int projectId)
        {
            throw new NotImplementedException();
        }

        public Task AssignUserToProject(int projectId, int userId)
        {
            throw new NotImplementedException();
        }

        public Task UpdateProjectStatus(int projectId, ProjectStatus status)
        {
            throw new NotImplementedException();
        }
    }
}
