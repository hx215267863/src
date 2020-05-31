using IFactory.Platform.Common.Response.User;

namespace IFactory.Platform.Common.Request.User
{
    public class UserGetRequest : BaseRequest<UserGetResponse>
    {
        public override string ApiName
        {
            get
            {
                return "user.get";
            }
        }

        public int UserId { get; set; }
    }
}
