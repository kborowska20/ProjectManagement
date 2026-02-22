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
                    new UserRole { Id = new Guid("11111111-3333-3333-4444-555555555559"), RoleName = "Admin" },
                    new UserRole { Id = new Guid("11111111-3333-3333-4444-555555555558"), RoleName = "Manager" },
                    new UserRole { Id = new Guid("11111111-3333-3333-4444-555555555666"), RoleName = "Developer" }
                );
                context.SaveChanges();
            }

            // Create 5 users
            if (!context.Users.Any())
            {
                context.Users.AddRange(
                    new User { Id = new Guid("11111111-1111-1111-1111-111111111111"), Name = "Joe Doe", Email = "joe.doe@example.com"},
                    new User { Id = new Guid("11111111-1111-1111-1111-111111111116"), Name = "Jane Smith", Email = "jane.smith@example.com" },
                    new User { Id = new Guid("11111111-1111-1111-1111-111111111113"), Name = "Alice Johnson", Email = "alice.johnson@example.com" },
                    new User { Id = new Guid("11111111-1111-1111-1111-111111111114"), Name = "Bob Brown", Email = "bob.brown@example.com" },
                    new User { Id = new Guid("11111111-1111-1111-1111-111111111115"), Name = "Carol White", Email = "carol.white@example.com" }
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
                    new Project { Id = Guid.Parse("11111111-1111-1111-1111-111111111112"), Status = context.ProjectStatuses.Find(new Guid("11111111-2222-3333-4444-555555555551")),  ProjectName = "Project Delta", Description = "Internal tooling upgrade" },
                    new Project { Id = Guid.Parse("11111111-2222-3333-4444-555555555556"), Status = context.ProjectStatuses.Find(new Guid("11111111-2222-3333-4444-555555555551")), ProjectName = "Project Epsilon", Description = "Customer onboarding improvements" },
                    new Project { Id = Guid.Parse("11111111-2222-3333-4444-555555555557"), Status = context.ProjectStatuses.Find(new Guid("11111111-2222-3333-4444-555555555551")), ProjectName = "Project Zeta", Description = "Mobile app revamp" }
                );
                context.SaveChanges();
            }

            // Replace the TaskItem seeding block with the following code to fix CS0029 errors
            if (!context.TaskItems.Any())
            {
                context.TaskItems.AddRange(
                    new TaskItem()
                    {
                        Id = new Guid("11111111-3333-3333-4444-555555555555"),
                        Title = "Task 1",
                        Desc = "Implement user authentication",
                        User = context.Users.Find(new Guid("11111111-1111-1111-1111-111111111111")),
                        Project = context.Projects.Find(new Guid("11111111-2222-3333-4444-555555555555"))
                    },
                    new TaskItem()
                    {
                        Id = new Guid("11111111-4444-3333-4444-555555555555"),
                        Title = "Task 2",
                        Desc = "Design onboarding flow",
                        User = context.Users.Find(new Guid("11111111-1111-1111-1111-111111111112")),
                        Project = context.Projects.Find(new Guid("11111111-2222-3333-4444-555555555556"))
                    },
                    new TaskItem()
                    {
                        Id = new Guid("11111111-5555-3333-4444-555555555555"),
                        Title = "Task 3",
                        Desc = "Update mobile UI components",
                        User = context.Users.Find(new Guid("11111111-1111-1111-1111-111111111113")),
                        Project = context.Projects.Find(new Guid("11111111-2222-3333-4444-555555555557"))
                    }
                );
                context.SaveChanges();
            }

            // Assign users to projects and tasks
            if (!context.UsersProjectTasks.Any())
            {
                context.UsersProjectTasks.AddRange(
                    new UsersProjectTask { UserId = Guid.Parse("11111111-1111-1111-1111-111111111111"), ProjectId = Guid.Parse("11111111-2222-3333-4444-555555555555"), TaskId = Guid.Parse("11111111-3333-3333-4444-555555555555") },
                    new UsersProjectTask { UserId = Guid.Parse("11111111-1111-1111-1111-111111111112"), ProjectId = Guid.Parse("11111111-2222-3333-4444-555555555556"), TaskId = Guid.Parse("11111111-4444-3333-4444-555555555555") },
                    new UsersProjectTask { UserId = Guid.Parse("11111111-1111-1111-1111-111111111113"), ProjectId = Guid.Parse("11111111-2222-3333-4444-555555555557"), TaskId = Guid.Parse("11111111-5555-3333-4444-555555555555") }
                );
                context.SaveChanges();
            }
        }
    }
}
