namespace IFactory.Platform.Common.Response.User
{
    public class UserSaveResponse : BaseResponse
    {
        public int UserId { get; set; }

        public IFactory.Domain.Common.SizeMeas? SizeMeas { get; set; }
    }
}
