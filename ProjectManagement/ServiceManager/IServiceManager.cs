namespace ProjectManagement.ServiceManager
{
    public interface IServiceManager
    {
        IUserService User { get; }
        IProjectService Project { get; }
        Task SaveAsync();
    }
}
