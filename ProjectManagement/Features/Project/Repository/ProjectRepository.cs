using Microsoft.EntityFrameworkCore;
using ProjectManagement.Data;
using ProjectManagement.Domain;
using System.Threading.Tasks;

namespace ProjectManagement.Features.Project.Repository
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly DataContext _context;
        public ProjectRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<Domain.Project> GetProjectByIdAsync(Guid projectId)
        {
            return await _context.Projects
                .FirstOrDefaultAsync(x => x.Id == projectId);
        }

        public async Task AssignUserToProject(UsersProjectTask usersProjectTask)
        {
            await _context.UserTaskItems.AddAsync(usersProjectTask);
            await _context.SaveChangesAsync(); 
        }

        public async Task UpdateProjectStatus(Guid projectId, ProjectStatus status)
        {
            var project = await _context.Projects.FindAsync(projectId);
            if (project != null)
            {
                project.Status = status;
                await _context.SaveChangesAsync();
            }
        }
    }
}
