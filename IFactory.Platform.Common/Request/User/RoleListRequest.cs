using IFactory.Platform.Common.Response.User;

namespace IFactory.Platform.Common.Request.User
{
    public class RoleListRequest : BaseRequest<RoleListResponse>
    {
        public override string ApiName
        {
            get
            {
                return "role.list";
            }
        }

        public int PageNumber { get; set; }

        public int PageSize { get; set; }
    }
}
