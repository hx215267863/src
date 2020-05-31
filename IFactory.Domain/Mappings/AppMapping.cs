using IFactory.Domain.Entities;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace IFactory.Domain.Mappings
{
    public class AppMapping : EntityTypeConfiguration<AppInfo>
    {
        public AppMapping()
        {
            base.ToTable("Apps");
            base.HasKey<int>((AppInfo x) => x.AppId);
            base.Property<int>((AppInfo x) => x.AppId).IsRequired().HasDatabaseGeneratedOption(new DatabaseGeneratedOption?(DatabaseGeneratedOption.Identity));
            base.Property((AppInfo x) => x.AppName).IsRequired();
            base.Property((AppInfo x) => x.AppKey).IsRequired();
            base.Property((AppInfo x) => x.AppSecret).IsOptional();
        }
    }
}
