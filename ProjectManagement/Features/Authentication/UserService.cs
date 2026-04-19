using ProjectManagement.Enums;

namespace ProjectManagement.Features.Authentication
{
    public class UserService : IUserService
    {
        private List<UserAuth> _users = new List<UserAuth> {
            new UserAuth {
                Id = 1, FirstName = "mytest",Role= new List<Role>{Role.Customer}, LastName = "User", Username = "mytestuser", Password = "test123"
            },
            new UserAuth {
                Id = 2, FirstName = "mytest2", LastName = "User2", Username = "test", Password = "test"
            }
        };

        public IEnumerable<UserAuth> GetAll()
        {
            return _users;
        }
        public UserAuth GetById(int id)
        {
            return _users.FirstOrDefault(x => x.Id == id);

        }
    }
}
