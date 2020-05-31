using IFactory.Domain.Entities;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace IFactory.Domain.Mappings
{
    public class FacilityInfoMap : EntityTypeConfiguration<FacilityInfo>
    {
        public FacilityInfoMap()
        {
            base.ToTable("facility_info");
            base.HasKey<int>((FacilityInfo x) => x.FacilityDID);
            base.Property<int>((FacilityInfo x) => x.FacilityDID).HasColumnName("facility_did").IsRequired().HasDatabaseGeneratedOption(new DatabaseGeneratedOption?(DatabaseGeneratedOption.None));
            base.Property((FacilityInfo x) => x.MMName).IsOptional();
            base.Property<int>((FacilityInfo x) => x.ProcessDID).HasColumnName("process_did").IsOptional();
            base.Property((FacilityInfo x) => x.MMIP).IsOptional();
            base.Property((FacilityInfo x) => x.MMPort).IsOptional();
            base.Property<bool>((FacilityInfo x) => x.MMIsUse).IsOptional();
            base.Property((FacilityInfo x) => x.MMClearAddr).IsOptional();
            base.Property((FacilityInfo x) => x.MMRestAddr).IsOptional();
            base.Property<int>((FacilityInfo x) => x.MMSeq).IsOptional();
            base.Property((FacilityInfo x) => x.MAAddress).IsOptional();
            base.Property<bool>((FacilityInfo x) => x.IsUse).IsOptional();
            base.Property((FacilityInfo x) => x.Remark).IsOptional();
            base.Property<int>((FacilityInfo x) => x.State).IsOptional();
            base.Property<int>((FacilityInfo x) => x.DeviceGroupDID).HasColumnName("device_group_did").IsOptional();
            base.HasRequired<ProcessInfo>((FacilityInfo m) => m.Process).WithMany().HasForeignKey<int>((FacilityInfo m) => m.ProcessDID);
        }
    }
}
