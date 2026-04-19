using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using ProjectManagement.Enums;
using AttributeTargets = System.AttributeTargets;

namespace ProjectManagement.Features.Authentication
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class Authorization : Attribute, IAuthorizationFilter
    {
        private readonly IList<Role> _roles;
        public Authorization(params Role[] _roles)
        {

            this._roles = _roles ?? new Role[] { };
        }
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var isRolePermission = false;
            UserAuth user = (UserAuth)context.HttpContext.Items["User"];
            if (user == null)
            {
                context.Result = new JsonResult(
                        new { Message = "Unauthorization" }
                    )
                    { StatusCode = StatusCodes.Status401Unauthorized };
            }
            if (user != null && this._roles.Any())
                foreach (var userRole in user.Role)
                {
                    foreach (var AuthRole in this._roles)
                    {

                        if (userRole == AuthRole)
                        {
                            isRolePermission = true;
                        }
                    }
                }

            if (!isRolePermission)
                context.Result = new JsonResult(
                        new { Message = "Unauthorization" }
                    )
                    { StatusCode = StatusCodes.Status401Unauthorized };
        }
    }
}
