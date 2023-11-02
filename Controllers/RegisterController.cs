using JobHunt.BL.Auth;
using JobHunt.ViewMapper;
using JobHunt.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace JobHunt.Controllers
{
    public class RegisterController: Controller
    {
        private readonly IAuthBL _authBL;
        public RegisterController(IAuthBL authBL)
        {
            _authBL = authBL;
        }

        [HttpGet]
        [Route("/register")]
        public IActionResult Index()
        {
            return View("Index", new RegisterViewModel());
        }

        [HttpPost]
        [Route("/register")]
        public IActionResult IndexSave(RegisterViewModel model)
        {
            if(ModelState.IsValid)
            {
                _authBL.CreateUser(AuthMapper.MapRegisterViewMoveToUserModel(model));
                return Redirect("/");
            }
            return View("Index", model);
        }
    }
}
