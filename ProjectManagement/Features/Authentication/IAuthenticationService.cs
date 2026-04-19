namespace ProjectManagement.Features.Authentication
{
    public interface IAuthenticationService
    {
        AuthenticateResponse Authenticate(AuthenticateRequest model);
    }
}
