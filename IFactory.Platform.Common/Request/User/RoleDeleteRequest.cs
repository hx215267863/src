using IFactory.Platform.Common.Response.User;

namespace IFactory.Platform.Common.Request.User
{
    public class RoleDeleteRequest : BaseRequest<RoleDeleteResponse>
    {
        public override string ApiName
        {
            get
            {
                return "role.delete";
            }
        }

        public int RoleId { get; set; }
    }
}
