using Microsoft.EntityFrameworkCore;
using ProjectManagement.Domain;

namespace ProjectManagement.Data
{
    public class DataContext(DbContextOptions<DataContext> options) : DbContext(options)
    {
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<User?> Users { get; set; }
        public DbSet<ProjectStatus> ProjectStatuses { get; set; }
        public DbSet<Project> Projects { get; set; }
    }
}
