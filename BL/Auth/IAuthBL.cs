using JobHunt.DAL.Models;

namespace JobHunt.BL.Auth
{
    public interface IAuthBL
    {
        Task<int> CreateUser(UserModel user);

    }
}
