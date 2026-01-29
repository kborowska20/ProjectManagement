namespace ProjectManagement.Domain
{
    public class Project
    {
        public Guid Id { get; set; }
        public string ProjectName { get; set; }
        public ICollection<User> Users { get; set; }
        public ICollection<TaskItem> Tasks { get; set; }
        public ProjectStatus Status { get; set; }
        public string Description { get; set; }
    }
}
