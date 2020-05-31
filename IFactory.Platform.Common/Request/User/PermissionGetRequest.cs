using IFactory.Platform.Common.Response.User;

namespace IFactory.Platform.Common.Request.User
{
    public class PermissionGetRequest : BaseRequest<PermissionGetResponse>
    {
        public override string ApiName
        {
            get
            {
                return "permission.get";
            }
        }

        public int PermissionId { get; set; }
    }
}
