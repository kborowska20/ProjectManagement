namespace ProjectManagement.Features.Authentication
{
    public interface IUserService
    {
        UserAuth GetById(int id);
        IEnumerable<UserAuth> GetAll();
    }
}
