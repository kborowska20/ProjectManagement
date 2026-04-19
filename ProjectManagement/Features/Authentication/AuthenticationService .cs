using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using ProjectManagement.Enums;

namespace ProjectManagement.Features.Authentication
{
    public class AuthenticationService : IAuthenticationService
    {
        private List<UserAuth> _users = new List<UserAuth> {
            new UserAuth {
                Id = 1, FirstName = "mytest", LastName = "User", Username = "mytestuser",Role= new List<Role>{Role.Customer} , Password = "test123"
            }
        };
        private readonly AppSettings _appSettings;
        public AuthenticationService(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
        }
        public AuthenticateResponse Authenticate(AuthenticateRequest model)
        {
            var user = _users.SingleOrDefault(x => x.Username == model.UserName && x.Password == model.Password);
            if (user == null) return null;
            var token = generateToken(user);
            return new AuthenticateResponse() { Token = token };
        }
        private string generateToken(UserAuth user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_appSettings.Key));
            var credetial = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            List<Claim> claims = new List<Claim>(){
                new Claim("Id",Convert.ToString(user.Id)),
                new Claim(JwtRegisteredClaimNames.Sub, "Test"),
                new Claim(JwtRegisteredClaimNames.Email, "test@gmail.com"),
                //new Claim("Role", Convert.ToString(user.Role)),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())

            };
            foreach (var role in user.Role)
            {

                claims.Add(new Claim("Role", Convert.ToString(role)));
            }
            var token = new JwtSecurityToken(_appSettings.Issuer, _appSettings.Issuer, claims, expires: DateTime.UtcNow.AddHours(1), signingCredentials: credetial);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
