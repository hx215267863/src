using IFactory.Platform.Common.Response.User;

namespace IFactory.Platform.Common.Request.User
{
    public class RoleGetRequest : BaseRequest<RoleGetResponse>
    {
        public override string ApiName
        {
            get
            {
                return "role.get";
            }
        }

        public int RoleId { get; set; }
    }
}
