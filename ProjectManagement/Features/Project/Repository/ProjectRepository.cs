using Microsoft.EntityFrameworkCore;
using ProjectManagement.Data;
using ProjectManagement.Domain;

namespace ProjectManagement.Features.Project.Repository
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly DataContext _context;
        public ProjectRepository(DataContext context)
        {
            _context = context;
        }
        public Task<IEnumerable<Domain.Project>> GetAllConsolesAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<Domain.Project> GetProjectByIdAsync(Guid projectId)
        {
            return await _context.Projects
                .FirstOrDefaultAsync(x => x.Id == projectId);
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
