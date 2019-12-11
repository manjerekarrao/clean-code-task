using Functions.Task3.ThirdParty;

namespace Functions.Task3
{
    public abstract class UserController : IController
    {
        private readonly UserAuthenticator _userAuthenticator;

        protected UserController(UserAuthenticator userAuthenticator)
        {
            this._userAuthenticator = userAuthenticator;
        }

        public void AuthenticateUser(string userName, string password)
        {
            IUser user = _userAuthenticator.Login(userName, password);
            if (user == null)
            {
                GenerateFailLoginResponse();
            }
            else
            {
                GenerateSuccessLoginResponse(userName);
            }
        }

        public abstract void GenerateSuccessLoginResponse(string user);
        public abstract void GenerateFailLoginResponse();
    }
}