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
            var project = await _context.Projects
                .Include(p => p.Status)  // Eagerly load Status
                .FirstOrDefaultAsync(x => x.Id == projectId);

            if (project != null)
            {
                var userTaskAssignments = await _context.UsersProjectTasks
                    .Where(upt => upt.ProjectId == projectId)
                    .ToListAsync();

                var userIds = userTaskAssignments
                    .Where(upt => upt.UserId.HasValue)
                    .Select(upt => upt.UserId.Value)
                    .Distinct()
                    .ToList();

                // Get distinct task IDs from assignments
                var taskIds = userTaskAssignments
                    .Where(upt => upt.TaskId.HasValue)
                    .Select(upt => upt.TaskId.Value)
                    .Distinct()
                    .ToList();

                project.Users = await _context.Users
                    .Where(u => userIds.Contains(u.Id))
                    .ToListAsync();

                // Load tasks
                project.Tasks = await _context.TaskItems
                    .Where(t => taskIds.Contains(t.Id))
                    .ToListAsync();

            }

            return project;
        }

        public async Task AssignUserToProject(UsersProjectTask usersProjectTask)
        {
            await _context.UsersProjectTasks.AddAsync(usersProjectTask);
            await _context.SaveChangesAsync();
        }

        public async Task<ProjectStatus> UpdateProjectStatus(Guid projectId, Guid statusId)
        {
            var project = await _context.Projects.FindAsync(projectId);
            var projectStatus = await _context.ProjectStatuses.FindAsync(statusId);
            if (projectStatus == null)
            {
                throw new ArgumentNullException("task not found");
            }

            if (project != null && projectStatus is not null)
            {
                project.Status = projectStatus;
                await _context.SaveChangesAsync();
            }

            return projectStatus;
        }

        public async Task DeleteTaskFromProject(Guid projectId, Guid taskId)
        {
            var userTaskAssignments = await _context.UsersProjectTasks
                .Where(upt => upt.ProjectId == projectId && upt.TaskId == taskId)
                .ToListAsync();

            if (userTaskAssignments.Any())
            {
                _context.UsersProjectTasks.RemoveRange(userTaskAssignments);
                await _context.SaveChangesAsync();
            }
        }
    }
}
