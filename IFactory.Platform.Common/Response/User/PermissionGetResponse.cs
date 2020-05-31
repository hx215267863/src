using IFactory.Domain.Models;

namespace IFactory.Platform.Common.Response.User
{
    public class PermissionGetResponse : BaseResponse
    {
        public PermissionModel Permission { get; set; }
    }
}
