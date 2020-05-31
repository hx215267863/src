using IFactory.Platform.Common.Response.User;

namespace IFactory.Platform.Common.Request.User
{
    public class PermissionSaveRequest : BaseRequest<PermissionSaveResponse>
    {
        public override string ApiName
        {
            get
            {
                return "permission.save";
            }
        }

        public int PermissionId { get; set; }

        public string PermissonName { get; set; }

        public string Remark { get; set; }
    }
}
