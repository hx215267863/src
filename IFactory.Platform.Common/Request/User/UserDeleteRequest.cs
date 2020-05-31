using IFactory.Platform.Common.Response.User;

namespace IFactory.Platform.Common.Request.User
{
    public class UserDeleteRequest : BaseRequest<UserDeleteResponse>
    {
        public int UserId;

        public override string ApiName
        {
            get
            {
                return "user.delete";
            }
        }
    }
}
