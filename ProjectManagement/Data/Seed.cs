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
                context.UserRoles.AddRange(
                    new UserRole { Id = 1, RoleName = "Admin" },
                    new UserRole { Id = 2, RoleName = "Manager" },
                    new UserRole { Id = 3, RoleName = "Developer" }
                );
                context.SaveChanges();
            }

            // Create 5 users
            if (!context.Users.Any())
            {
                context.Users.AddRange(
                    new User { Id = 1, Name = "Joe Doe", Email = "joe.doe@example.com", RoleId = 1 },
                    new User { Id = 2, Name = "Jane Smith", Email = "jane.smith@example.com", RoleId = 2 },
                    new User { Id = 3, Name = "Alice Johnson", Email = "alice.johnson@example.com", RoleId = 3 },
                    new User { Id = 4, Name = "Bob Brown", Email = "bob.brown@example.com", RoleId = 3 },
                    new User { Id = 5, Name = "Carol White", Email = "carol.white@example.com", RoleId = 2 }
                );
                context.SaveChanges();
            }

            // Create project statuses
            if (!context.ProjectStatuses.Any())
            {
                context.ProjectStatuses.AddRange(
                    new ProjectStatus { Id = 1, StatusName = "Active" },
                    new ProjectStatus { Id = 2, StatusName = "On Hold" },
                    new ProjectStatus { Id = 3, StatusName = "Completed" }
                );
                context.SaveChanges();
            }

            // Create sample projects
            if (!context.Projects.Any())
            {
                context.Projects.AddRange(
                    new Project {Id = 1, ProjectName = "Project Alpha" },
                    new Project {Id = 2, ProjectName = "Project Beta" },
                    new Project {Id = 3, ProjectName = "Project Gamma" }
                );
                context.SaveChanges();
            }
        }
    }
}
