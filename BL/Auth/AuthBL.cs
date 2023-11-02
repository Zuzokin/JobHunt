using JobHunt.DAL;
using JobHunt.DAL.Models;

namespace JobHunt.BL.Auth
{
    public class AuthBL : IAuthBL
    {
        private readonly IAuthDAL _authDal;
        private readonly IEncrypt _encrypt;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuthBL(
            IAuthDAL authDal,
            IEncrypt encrypt,
            IHttpContextAccessor httpContextAccessor
            )
        {
            _authDal = authDal;
            _encrypt = encrypt;
			_httpContextAccessor = httpContextAccessor;
        }

        public async Task<int> CreateUser(UserModel user)
        {
            user.Salt = Guid.NewGuid().ToString();
            user.Password = _encrypt.HashPassword(user.Password, user.Salt);
            int id = await _authDal.CreateUser(user);
            Login(id);
            return id;

		}

        public void Login(int id)
        {
            _httpContextAccessor.HttpContext?.Session.SetInt32(AuthConstants.AUTH_SESSION_PARAM_NAME, id);

		}
    }
}
