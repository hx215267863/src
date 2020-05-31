using IFactory.Platform.Common.Response.User;

namespace IFactory.Platform.Common.Request.User
{
    public class RoleSaveRequest : BaseRequest<RoleSaveResponse>
    {
        public override string ApiName
        {
            get
            {
                return "role.save";
            }
        }

        public int RoleId { get; set; }

        public string RoleName { get; set; }

        public string Remark { get; set; }

        public string PermissionCodes { get; set; }
    }
}
