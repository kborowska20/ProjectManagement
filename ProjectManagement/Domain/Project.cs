namespace ProjectManagement.Domain
{
    public class Project
    {
        public int Id { get; set; }
        public string ProjectName { get; set; }
        public ICollection<User> Users { get; set; }
        public ProjectStatus Status { get; set; }
        public string Description { get; set; }
    }
}
