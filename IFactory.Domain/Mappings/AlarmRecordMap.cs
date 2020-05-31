using IFactory.Domain.Entities;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace IFactory.Domain.Mappings
{
    public class AlarmRecordMap : EntityTypeConfiguration<AlarmRecordInfo>
    {
        public AlarmRecordMap()
        {
            base.ToTable("alarm_record");
            base.HasKey<int>((AlarmRecordInfo x) => x.AlarmRecordDID);
            base.Property<int>((AlarmRecordInfo x) => x.AlarmRecordDID).HasColumnName("alarm_record_did").IsRequired().HasDatabaseGeneratedOption(new DatabaseGeneratedOption?(DatabaseGeneratedOption.None));
            base.Property((AlarmRecordInfo x) => x.Address).IsOptional();
            base.Property<int>((AlarmRecordInfo x) => x.AlarmCount).HasColumnName("alarm_count").IsOptional();
            base.Property((AlarmRecordInfo x) => x.AlarmTime).HasColumnName("alarm_time").IsOptional();
            base.Property<int>((AlarmRecordInfo x) => x.DisposeState).HasColumnName("dispose_state").IsOptional();
            base.Property((AlarmRecordInfo x) => x.DisposeTime).HasColumnName("dispose_time").IsOptional();
            base.Property<int>((AlarmRecordInfo x) => x.Duration).IsOptional();
            base.Property<int>((AlarmRecordInfo x) => x.FacilityDID).HasColumnName("facility_did").IsOptional();
            base.Property((AlarmRecordInfo x) => x.Handler).IsOptional();
            base.Property((AlarmRecordInfo x) => x.Remark).IsOptional();
            base.Property((AlarmRecordInfo x) => x.RuleDID).HasColumnName("rule_did").IsOptional();
            base.HasRequired<FacilityInfo>((AlarmRecordInfo m) => m.Facility).WithMany().HasForeignKey<int>((AlarmRecordInfo m) => m.FacilityDID);
        }
    }
}
