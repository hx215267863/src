using IFactory.Domain.Common;
using IFactory.Domain.Entities;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace IFactory.Domain.Mappings
{
    public class UserMap : EntityTypeConfiguration<UserInfo>
    {
        public UserMap()
        {
            base.ToTable("Users");
            base.HasKey<int>((UserInfo x) => x.UserId);
            base.Property<int>((UserInfo x) => x.UserId).IsRequired().HasDatabaseGeneratedOption(new DatabaseGeneratedOption?(DatabaseGeneratedOption.Identity));
            base.Property((UserInfo x) => x.UserName).IsRequired();
            base.Property((UserInfo x) => x.Password).IsRequired();
            base.Property((UserInfo x) => x.Name).IsRequired();
            base.Property((UserInfo x) => x.CreateTime).IsRequired();
            base.Property<int>((UserInfo x) => x.RoleId).IsRequired();
            base.Property<Gender>((UserInfo x) => x.Gender).IsOptional();
            base.Property((UserInfo x) => x.LastLoginTime).IsOptional();
            base.Property((UserInfo x) => x.CraftDIDs).HasColumnName("craft_dids").IsOptional();
            base.HasRequired<RoleInfo>((UserInfo m) => m.Role).WithMany().HasForeignKey<int>((UserInfo m) => m.RoleId);
        }
    }
}
