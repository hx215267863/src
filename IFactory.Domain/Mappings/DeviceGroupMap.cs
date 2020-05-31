using IFactory.Domain.Entities;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace IFactory.Domain.Mappings
{
    public class DeviceGroupMap : EntityTypeConfiguration<DeviceGroupInfo>
    {
        public DeviceGroupMap()
        {
            base.ToTable("device_group");
            base.HasKey<int>((DeviceGroupInfo x) => x.DeviceGroupDID);
            base.Property<int>((DeviceGroupInfo x) => x.DeviceGroupDID).HasColumnName("device_group_did").IsRequired().HasDatabaseGeneratedOption(new DatabaseGeneratedOption?(DatabaseGeneratedOption.None));
            base.Property((DeviceGroupInfo x) => x.DeviceGroupNO).HasColumnName("device_group_no").IsOptional();
            base.Property((DeviceGroupInfo x) => x.DeviceGroupName).HasColumnName("device_group_name").IsOptional();
            base.Property<int>((DeviceGroupInfo x) => x.CraftDID).HasColumnName("craft_did").IsRequired();
            base.HasRequired<CraftInfo>((DeviceGroupInfo m) => m.Craft).WithMany().HasForeignKey<int>((DeviceGroupInfo m) => m.CraftDID);
        }
    }
}
