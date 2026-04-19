using ProjectManagement.Enums;
using System.Text.Json.Serialization;

namespace ProjectManagement.Features.Authentication
{
    public class UserAuth
    {
        public int Id
        {
            get;
            set;
        }
        public string FirstName
        {
            get;
            set;
        }
        public string LastName
        {
            get;
            set;
        }
        public string Username
        {
            get;
            set;
        }
        public List<Role> Role { get; set; }
        [JsonIgnore]
        public string Password
        {
            get;
            set;
        }
    }
}
