using ProjectManagement.Data;
using ProjectManagement.Features.Project.Repository;
using ProjectManagement.Features.TaskItem.Repository;
using ProjectManagement.Features.User.Repository;
using ProjectManagement.ServiceManager;


namespace ProjectManagement
{
    public class RepositoryManager : IRepositoryManager
    {
        private readonly DataContext _context;
        private IProjectRepository _projectRepository;
        private IUserRepository _userRepository;
        private ITaskItemRepository _taskRepository;

        public RepositoryManager(DataContext context)
        {
            _context = context;
        }
        public IProjectRepository Project
        {
            get
            {
                if (_projectRepository == null)
                    _projectRepository = new ProjectRepository(_context);
                return _projectRepository;
            }
        }
        public IUserRepository User
        {
            get
            {
                if (_userRepository == null)
                    _userRepository = new UserRepository(_context);
                return _userRepository;
            }
        }


        public ITaskItemRepository TaskItem
        {
            get
            {
                if (_taskRepository == null)
                    _taskRepository = new TaskItemRepository(_context);
                return _taskRepository;

            }
        }

        public Task SaveAsync()
        {
            return _context.SaveChangesAsync();
        }
    }
}
