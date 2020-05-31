using IFactory.Platform.Common.Response.User;

namespace IFactory.Platform.Common.Request.User
{
    public class ChangePasswordRequest : BaseRequest<ChangePasswordResponse>
    {
        public override string ApiName
        {
            get
            {
                return "change.password";
            }
        }

        public int UserId { get; set; }

        public string OldPassword { get; set; }

        public string NewPassword { get; set; }
    }
}
