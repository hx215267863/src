using IFactory.Domain.Models;

namespace IFactory.Platform.Common.Response.User
{
    public class UserGetResponse : BaseResponse
    {
        public UserModel User { get; set; }
    }
}
