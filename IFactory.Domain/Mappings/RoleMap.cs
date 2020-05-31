using IFactory.Domain.Entities;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace IFactory.Domain.Mappings
{
    public class RoleMap : EntityTypeConfiguration<RoleInfo>
    {
        public RoleMap()
        {
            base.ToTable("Roles");
            base.HasKey<int>((RoleInfo x) => x.RoleId);
            base.Property<int>((RoleInfo x) => x.RoleId).IsRequired().HasDatabaseGeneratedOption(new DatabaseGeneratedOption?(DatabaseGeneratedOption.Identity));
            base.Property((RoleInfo x) => x.RoleName).IsRequired();
            base.Property((RoleInfo x) => x.PermissionCodes).IsOptional();
            base.Property((RoleInfo x) => x.CreateTime).IsRequired();
            base.Property((RoleInfo x) => x.Remark).IsOptional();
        }
    }
}
