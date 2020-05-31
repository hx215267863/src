using IFactory.Platform.Common.Response.User;

namespace IFactory.Platform.Common.Request.User
{
    public class PermissionListRequest : BaseRequest<PermissionListResponse>
    {
        public override string ApiName
        {
            get
            {
                return "permission.list";
            }
        }
    }
}
