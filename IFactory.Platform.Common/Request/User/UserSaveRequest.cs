using IFactory.Platform.Common.Response.User;

namespace IFactory.Platform.Common.Request.User
{
    public class UserSaveRequest : BaseRequest<UserSaveResponse>
    {
        public override string ApiName
        {
            get
            {
                return "user.save";
            }
        }

        public int UserId { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public string Name { get; set; }

        public Domain.Common.Gender? Gender { get; set; }

        public Domain.Common.SizeMeas? Size { get; set; }

        public int RoleId { get; set; }

        public string CraftDIDs { get; set; }

        public string craftwork { get; set; }
        public string process { get; set; }
        public string quarters { get; set; }
        public string segments { get; set; }
        public string staffid { get; set; }

        public string barcode { get; set; }

        public string factoryID { get; set; }
        public string fano { get; set; }

        public string end_product_no { get; set; }
    }
}
