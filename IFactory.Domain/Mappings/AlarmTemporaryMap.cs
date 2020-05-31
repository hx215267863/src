using IFactory.Domain.Entities;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace IFactory.Domain.Mappings
{
    public class AlarmTemporaryMap : EntityTypeConfiguration<AlarmTemporaryInfo>
    {
        public AlarmTemporaryMap()
        {
            base.ToTable("alarm_temporary");
            base.HasKey<int>((AlarmTemporaryInfo x) => x.AlarmTemporaryDID);
            base.Property<int>((AlarmTemporaryInfo x) => x.AlarmTemporaryDID).HasColumnName("alarm_temporary_did").IsRequired().HasDatabaseGeneratedOption(new DatabaseGeneratedOption?(DatabaseGeneratedOption.None));
            base.Property((AlarmTemporaryInfo x) => x.Address).IsOptional();
            base.Property((AlarmTemporaryInfo x) => x.AlarmTime).HasColumnName("alarm_time").IsOptional();
            base.Property<int>((AlarmTemporaryInfo x) => x.DisposeState).HasColumnName("dispose_state").IsOptional();
            base.Property((AlarmTemporaryInfo x) => x.DisposeTime).HasColumnName("dispose_time").IsOptional();
            base.Property<int>((AlarmTemporaryInfo x) => x.Duration).IsOptional();
            base.Property<int>((AlarmTemporaryInfo x) => x.FacilityDID).HasColumnName("facility_did").IsOptional();
            base.Property((AlarmTemporaryInfo x) => x.Handler).IsOptional();
            base.Property((AlarmTemporaryInfo x) => x.Remark).IsOptional();
            base.Property((AlarmTemporaryInfo x) => x.RuleDID).HasColumnName("rule_did").IsOptional();
            base.HasRequired<FacilityInfo>((AlarmTemporaryInfo m) => m.Facility).WithMany().HasForeignKey<int>((AlarmTemporaryInfo m) => m.FacilityDID);
        }
    }
}
