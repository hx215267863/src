using System;

namespace IFactory.Domain.Entities
{
  public class UserInfo
  {
    public int UserId { get; set; }

    public string UserName { get; set; }

    public string Password { get; set; }

    public string Name { get; set; }

    public IFactory.Domain.Common.Gender? Gender { get; set; }

    public IFactory.Domain.Common.SizeMeas? Size { get; set; }

    public int RoleId { get; set; }

    public DateTime CreateTime { get; set; }

    public DateTime? LastLoginTime { get; set; }

    public string CraftDIDs { get; set; }

    public virtual RoleInfo Role { get; set; }

        public string craftwork { get; set; }
        public string process { get; set; }
        public string quarters { get; set; }
        public string segments { get; set; }
        public string staffid { get; set; }

        public string factoryID { get; set; }
        public string fano { get; set; }
        public string end_product_no { get; set; }
    }
}
