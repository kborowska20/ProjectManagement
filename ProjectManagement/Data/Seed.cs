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
                    new UserRole { Id = new Guid(), RoleName = "Admin" },
                    new UserRole { Id = new Guid(), RoleName = "Manager" },
                    new UserRole { Id = new Guid(), RoleName = "Developer" }
                );
                context.SaveChanges();
            }

            // Create 5 users
            if (!context.Users.Any())
            {
                context.Users.AddRange(
                    new User { Id = new Guid(), Name = "Joe Doe", Email = "joe.doe@example.com"},
                    new User { Id = new Guid(), Name = "Jane Smith", Email = "jane.smith@example.com" },
                    new User { Id = new Guid(), Name = "Alice Johnson", Email = "alice.johnson@example.com" },
                    new User { Id = new Guid(), Name = "Bob Brown", Email = "bob.brown@example.com" },
                    new User { Id = new Guid(), Name = "Carol White", Email = "carol.white@example.com" }
                );
                context.SaveChanges();
            }

            if (!context.Tasks.Any())
            {
                context.Tasks.AddRange(
                    new TaskItem() { Id = new Guid("11111111-3333-3333-4444-555555555555"), Title = "Task 1", Desc = "joe.doe@example.com", UserId = 1, ProjectId = 3},
                    new TaskItem() { Id = new Guid("11111111-4444-3333-4444-555555555555"), Title = "Task 2", Desc = "jane.smith@example.com", UserId = 2,  ProjectId = 3 },
                    new TaskItem() { Id = new Guid("11111111-5555-3333-4444-555555555555"), Title = "Task 3", Desc = "alice.johnson@example.com", UserId = 3 , ProjectId = 1 }
                );
                context.SaveChanges();
            }

            // Create project statuses
            if (!context.ProjectStatuses.Any())
            {
                context.ProjectStatuses.AddRange(
                    new ProjectStatus { Id = new Guid("11111111-2222-3333-4444-555555555551"), StatusName = "Active" },
                    new ProjectStatus { Id = new Guid("11111111-2222-3333-4444-555555555552"), StatusName = "On Hold" },
                    new ProjectStatus { Id = new Guid("11111111-2222-3333-4444-555555555553"), StatusName = "Completed" }
                );
                context.SaveChanges();
            }

            // Create sample projects
            if (!context.Projects.Any())
            {
                context.Projects.AddRange(
                    new Project { Id = Guid.Parse("11111111-2222-3333-4444-555555555555"), ProjectName = "Project Delta", Description = "Internal tooling upgrade" },
                    new Project { Id = Guid.Parse("11111111-2222-3333-4444-555555555556"), ProjectName = "Project Epsilon", Description = "Customer onboarding improvements" },
                    new Project { Id = Guid.Parse("11111111-2222-3333-4444-555555555557"), ProjectName = "Project Zeta", Description = "Mobile app revamp" }
                );
                context.SaveChanges();
            }
        }
    }
}
