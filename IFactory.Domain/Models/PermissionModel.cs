namespace IFactory.Domain.Models
{
    public class PermissionModel
    {
        public int PermissionId { get; set; }

        public string PermissionName { get; set; }

        public string PermissionCode { get; set; }

        public int? ParentId { get; set; }

        public int DisplayOrder { get; set; }

        public int Depth { get; set; }

        public string Remark { get; set; }
    }
}
