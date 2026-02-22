namespace ProjectManagement.Domain
{
    public class TaskItem
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Desc { get; set; }
        public User? User { get; set; }
        public Project Project { get; set; }
    }
}
