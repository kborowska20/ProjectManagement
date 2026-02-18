namespace ProjectManagement.Domain
{
    public class UsersProjectTask
    {
        public Guid? UserId { get; set; }
        public Guid ProjectId { get; set; }
        public Guid? TaskId { get; set; }

    }
}
