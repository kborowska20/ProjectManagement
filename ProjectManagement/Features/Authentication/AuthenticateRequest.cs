using System.ComponentModel.DataAnnotations;

namespace ProjectManagement.Features.Authentication
{
    public class AuthenticateRequest
    {
        [Required]
        public string UserName
        {
            get;
            set;
        }
        [Required]
        public string Password
        {
            get;
            set;
        }
    }
}
