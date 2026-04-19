using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace ProjectManagement.Features.Authentication; // Replace with your project's namespace

public class JwtMiddleware
{
    private readonly RequestDelegate _next;
    private readonly AppSettings _appSettings;
    public JwtMiddleware(RequestDelegate _next, IOptions<AppSettings> _appSettings)
    {
        this._next = _next;
        this._appSettings = _appSettings.Value;
    }
    public async Task Invoke(HttpContext context, IUserService userService)
    {

        var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
        if (token != null)
            //Validate Token
            attachUserToContext(context, userService, token);
        _next(context);
    }
    private void attachUserToContext(HttpContext context, IUserService userService, string token)
    {
        try
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_appSettings.Key));
            tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateAudience = true,
                ValidateIssuer = true,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = key,
                ClockSkew = TimeSpan.Zero,
                ValidIssuer = _appSettings.Issuer,
                ValidAudience = _appSettings.Issuer
            }, out SecurityToken validateToken);
            var jwtToken = (JwtSecurityToken)validateToken;
            var userId = int.Parse(jwtToken.Claims.FirstOrDefault(_ => _.Type == "Id").Value);
            context.Items["User"] = userService.GetById(userId);
        }
        catch (Exception ex)
        {
        }
    }
}