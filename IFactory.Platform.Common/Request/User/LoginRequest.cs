using IFactory.Platform.Common.Response.User;

namespace IFactory.Platform.Common.Request.User
{
    public class LoginRequest : BaseRequest<LoginResponse>
    {
        public override string ApiName
        {
            get
            {
                return "account.login";
            }
        }

        public string UserName { get; set; }

        public string Password { get; set; }
    }
}
