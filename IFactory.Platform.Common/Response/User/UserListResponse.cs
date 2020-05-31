using IFactory.Domain.Models;

namespace IFactory.Platform.Common.Response.User
{
    public class UserListResponse : BaseResponse
    {
        public PagedData<UserModel> Users { get; set; }
    }
}
