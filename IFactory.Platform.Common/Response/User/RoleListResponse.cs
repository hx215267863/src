using IFactory.Domain.Models;

namespace IFactory.Platform.Common.Response.User
{
    public class RoleListResponse : BaseResponse
    {
        public PagedData<RoleModel> Roles { get; set; }
    }
}
