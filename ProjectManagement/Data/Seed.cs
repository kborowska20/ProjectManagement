using System.Linq;
using ProjectManagement.Data;
using ProjectManagement.Domain;

namespace ProjectManagement.Data
{
    public class Seed
    {
        public void SeedData(DataContext context)
        {
            // Ensure roles exist first so users can reference them
            if (!context.UserRoles.Any())
            {
                var roles = new[]
                {
                    new UserRole { Id = Guid.Parse("11111111-3333-3333-4444-555555555559"), RoleName = "Admin" },
                    new UserRole { Id = Guid.Parse("11111111-3333-3333-4444-555555555558"), RoleName = "Manager" },
                    new UserRole { Id = Guid.Parse("11111111-3333-3333-4444-555555555666"), RoleName = "Developer" }
                };
                context.UserRoles.AddRange(roles);
                context.SaveChanges();
            }

            // Create users with roles
            if (!context.Users.Any())
            {
                var adminRole = context.UserRoles.Find(Guid.Parse("11111111-3333-3333-4444-555555555559"));
                var managerRole = context.UserRoles.Find(Guid.Parse("11111111-3333-3333-4444-555555555558"));
                var developerRole = context.UserRoles.Find(Guid.Parse("11111111-3333-3333-4444-555555555666"));

                var users = new[]
                {
                    new User { Id = Guid.Parse("11111111-1111-1111-1111-111111111111"), Name = "Joe Doe", Email = "joe.doe@example.com", UserRoleId = adminRole.Id },
                    new User { Id = Guid.Parse("11111111-1111-1111-1111-111111111116"), Name = "Jane Smith", Email = "jane.smith@example.com", UserRoleId = managerRole.Id },
                    new User { Id = Guid.Parse("11111111-1111-1111-1111-111111111113"), Name = "Alice Johnson", Email = "alice.johnson@example.com", UserRoleId = developerRole.Id },
                    new User { Id = Guid.Parse("11111111-1111-1111-1111-111111111114"), Name = "Bob Brown", Email = "bob.brown@example.com", UserRoleId = developerRole.Id },
                    new User { Id = Guid.Parse("11111111-1111-1111-1111-111111111115"), Name = "Carol White", Email = "carol.white@example.com", UserRoleId = developerRole.Id }
                };
                context.Users.AddRange(users);
                context.SaveChanges();
            }

            // Create project statuses
            if (!context.ProjectStatuses.Any())
            {
                var statuses = new[]
                {
                    new ProjectStatus { Id = Guid.Parse("11111111-2222-3333-4444-555555555551"), StatusName = "Active" },
                    new ProjectStatus { Id = Guid.Parse("11111111-2222-3333-4444-555555555552"), StatusName = "On Hold" },
                    new ProjectStatus { Id = Guid.Parse("11111111-2222-3333-4444-555555555553"), StatusName = "Completed" }
                };
                context.ProjectStatuses.AddRange(statuses);
                context.SaveChanges();
            }

            // Create sample projects
            if (!context.Projects.Any())
            {
                var activeStatus = context.ProjectStatuses.Find(Guid.Parse("11111111-2222-3333-4444-555555555551"));
                var onHoldStatus = context.ProjectStatuses.Find(Guid.Parse("11111111-2222-3333-4444-555555555552"));

                var projects = new[]
                {
                    new Project { Id = Guid.Parse("11111111-1111-1111-1111-111111111112"), Status = activeStatus, ProjectName = "Project Delta", Description = "Internal tooling upgrade" },
                    new Project { Id = Guid.Parse("11111111-2222-3333-4444-555555555556"), Status = activeStatus, ProjectName = "Project Epsilon", Description = "Customer onboarding improvements" },
                    new Project { Id = Guid.Parse("11111111-2222-3333-4444-555555555557"), Status = onHoldStatus, ProjectName = "Project Zeta", Description = "Mobile app revamp" }
                };
                context.Projects.AddRange(projects);
                context.SaveChanges();
            }

            // Create task items
            if (!context.TaskItems.Any())
            {
                var user1 = context.Users.Find(Guid.Parse("11111111-1111-1111-1111-111111111111"));
                var user2 = context.Users.Find(Guid.Parse("11111111-1111-1111-1111-111111111116"));
                var user3 = context.Users.Find(Guid.Parse("11111111-1111-1111-1111-111111111113"));

                var project1 = context.Projects.Find(Guid.Parse("11111111-1111-1111-1111-111111111112"));
                var project2 = context.Projects.Find(Guid.Parse("11111111-2222-3333-4444-555555555556"));
                var project3 = context.Projects.Find(Guid.Parse("11111111-2222-3333-4444-555555555557"));

                // Fix for CS0029: Use the User's Id property instead of the User object itself
                var tasks = new[]
                {
                    new TaskItem
                    {
                        Id = Guid.Parse("11111111-3333-3333-4444-555555555555"),
                        Title = "Implement User Authentication",
                        Description = "Create login and registration functionality with JWT tokens",
                        AssignedUserId = user1.Id,
                        ProjectId = project1.Id
                    },  
                    new TaskItem
                    {
                        Id = Guid.Parse("11111111-4444-3333-4444-555555555555"),
                        Title = "Design Onboarding Flow",
                        Description = "Create wireframes and mockups for new user onboarding experience",
                        AssignedUserId = user2.Id,
                        ProjectId = project2.Id
                    },
                    new TaskItem
                    {
                        Id = Guid.Parse("11111111-5555-3333-4444-555555555555"),
                        Title = "Update Mobile UI Components",
                        Description = "Modernize mobile app UI components using latest design system",
                        AssignedUserId = user3.Id,
                        ProjectId = project3.Id
                    }
                };
                context.TaskItems.AddRange(tasks);
                context.SaveChanges();
            }

            // Assign users to projects and tasks
            if (!context.UsersProjectTasks.Any())
            {
                var assignments = new[]
                {
                    new UsersProjectTask { UserId = Guid.Parse("11111111-1111-1111-1111-111111111111"), ProjectId = Guid.Parse("11111111-1111-1111-1111-111111111112"), TaskId = Guid.Parse("11111111-3333-3333-4444-555555555555") },
                    new UsersProjectTask { UserId = Guid.Parse("11111111-1111-1111-1111-111111111116"), ProjectId = Guid.Parse("11111111-2222-3333-4444-555555555556"), TaskId = Guid.Parse("11111111-4444-3333-4444-555555555555") },
                    new UsersProjectTask { UserId = Guid.Parse("11111111-1111-1111-1111-111111111113"), ProjectId = Guid.Parse("11111111-2222-3333-4444-555555555557"), TaskId = Guid.Parse("11111111-5555-3333-4444-555555555555") }
                };
                context.UsersProjectTasks.AddRange(assignments);
                context.SaveChanges();
            }
        }
    }
}
