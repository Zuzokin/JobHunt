using JobHunt.DAL.Models;
using JobHunt.ViewModels;

namespace JobHunt.ViewMapper
{
    public class AuthMapper
    {
        public static UserModel MapRegisterViewMoveToUserModel(RegisterViewModel model) 
        {
            return new UserModel()
            {
                Email = model.Email!,
                Password = model.Password!
            };
        }
    }
}
