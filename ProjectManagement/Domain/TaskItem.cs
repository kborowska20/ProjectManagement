namespace ProjectManagement.Domain
{
    public class TaskItem
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Desc { get; set; }
        public int UserId { get; set; }
        public int ProjectId { get; set; }
    }
}
