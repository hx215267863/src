using IFactory.Domain.Entities;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace IFactory.Domain.Mappings
{
    public class PermissionMap : EntityTypeConfiguration<PermissionInfo>
    {
        public PermissionMap()
        {
            base.ToTable("Permissions");
            base.HasKey<int>((PermissionInfo x) => x.PermissionId);
            base.Property<int>((PermissionInfo x) => x.PermissionId).IsRequired().HasDatabaseGeneratedOption(new DatabaseGeneratedOption?(DatabaseGeneratedOption.Identity));
            base.Property((PermissionInfo x) => x.PermissionName).IsRequired();
            base.Property((PermissionInfo x) => x.PermissionCode).IsOptional();
            base.Property<int>((PermissionInfo x) => x.Depth).IsRequired();
            base.Property<int>((PermissionInfo x) => x.DisplayOrder).IsRequired();
            base.Property<int>((PermissionInfo x) => x.ParentId).IsOptional();
            base.Property((PermissionInfo x) => x.Remark).IsOptional();
            base.HasOptional<PermissionInfo>((PermissionInfo m) => m.Parent).WithMany((PermissionInfo m) => m.Children).HasForeignKey<int?>((PermissionInfo m) => m.ParentId);
        }
    }
}
