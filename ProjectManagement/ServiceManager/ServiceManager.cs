using ProjectManagement.Data;
using ProjectManagement.ServiceManager;

namespace ProjectManagement.ServiceManager
{
    public class ServiceManager : IServiceManager
    {
        private readonly DataContext _context;
        private IProjectService _projectService;
        private IUserService _userService;

        public ServiceManager(DataContext context)
        {
            _context = context;
        }
        public IProjectService Project
        {
            get
            {
                if (_projectService == null)
                    _projectService = new ProjectService(_context);
                return _projectService;
            }
        }
        public IUserService User
        {
            get
            {
                if (_userService == null)
                    _userService = new UserService(_context);
                return _userService;
            }
        }
        public Task SaveAsync()
        {
            return _context.SaveChangesAsync();
        }
    }
}
