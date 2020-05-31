using IFactory.Domain.Entities;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace IFactory.Domain.Mappings
{
    public class AuthMapping : EntityTypeConfiguration<AuthInfo>
    {
        public AuthMapping()
        {
            base.ToTable("Auths");
            base.HasKey<int>((AuthInfo x) => x.AuthId);
            base.Property<int>((AuthInfo x) => x.AuthId).IsRequired().HasDatabaseGeneratedOption(new DatabaseGeneratedOption?(DatabaseGeneratedOption.Identity));
            base.Property<int>((AuthInfo x) => x.AppId).IsRequired();
            base.Property((AuthInfo x) => x.DeviceId).IsOptional();
            base.Property((AuthInfo x) => x.AccessToken).IsOptional();
            base.Property<int>((AuthInfo x) => x.UserId).IsRequired();
            base.HasRequired<AppInfo>((AuthInfo a) => a.App).WithMany((AppInfo b) => b.Auths).HasForeignKey<int>((AuthInfo c) => c.AppId);
            base.HasRequired<UserInfo>((AuthInfo a) => a.User).WithMany().HasForeignKey<int>((AuthInfo m) => m.UserId);
        }
    }
}
