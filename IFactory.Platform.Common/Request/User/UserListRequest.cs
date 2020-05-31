using IFactory.Platform.Common.Response.User;

namespace IFactory.Platform.Common.Request.User
{
    public class UserListRequest : BaseRequest<UserListResponse>
    {
        public override string ApiName
        {
            get
            {
                return "user.list";
            }
        }

        public int PageNumber { get; set; }

        public int PageSize { get; set; }
    }
}
