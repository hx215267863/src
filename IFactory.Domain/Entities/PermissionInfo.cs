using System.Collections.Generic;

namespace IFactory.Domain.Entities
{
    public class PermissionInfo
    {
        private ICollection<PermissionInfo> children;

        public int PermissionId { get; set; }

        public string PermissionName { get; set; }

        public string PermissionCode { get; set; }

        public int? ParentId { get; set; }

        public int DisplayOrder { get; set; }

        public int Depth { get; set; }

        public string Remark { get; set; }

        public virtual PermissionInfo Parent { get; set; }

        public virtual ICollection<PermissionInfo> Children
        {
            get
            {
                return this.children ?? (this.children = new List<PermissionInfo>());
            }
            set
            {
                this.children = value;
            }
        }
        
    }
}
