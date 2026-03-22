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

                // Get distinct user IDs
                var userIds = userTaskAssignments
                    .Where(upt => upt.UserId.HasValue)
                    .Select(upt => upt.UserId.Value)
                    .Distinct()
                    .ToList();

                // Load users and tasks
                project.Users = await _context.Users
                    .Where(u => userIds.Contains(u.Id))
                    .ToListAsync();

                project.Tasks = await _context.TaskItems
                    .Where(t => t.ProjectId == project.Id)
                    .ToListAsync();

                var userTaskIds = project.Tasks
                    .Where(t => t.AssignedUserId.HasValue)
                    .Select(t => t.AssignedUserId.Value)
                    .Distinct()
                    .ToList();

                // Find users that are in tasks but not in project.Users
                var existingUserIds = project.Users.Select(u => u.Id).ToList();
                var missingUserIds = userTaskIds.Except(existingUserIds).ToList();

                // Load and add missing users
                if (missingUserIds.Any())
                {
                    var missingUsers = await _context.Users
                        .Where(u => missingUserIds.Contains(u.Id))
                        .ToListAsync();

                    project.Users = project.Users.Concat(missingUsers).ToList();
                }
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
