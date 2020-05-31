using IFactory.Domain.Models;

namespace IFactory.Platform.Common.Response.User
{
    public class RoleGetResponse : BaseResponse
    {
        public RoleModel Role { get; set; }
    }
}
