namespace IFactory.Platform.Common.Response.User
{
    public class LoginResponse : BaseResponse
    {
        public int UserId { get; set; }

        public string Name { get; set; }

        public string PermissionCodes { get; set; }

        public string CraftDIDs { get; set; }
    }
}
