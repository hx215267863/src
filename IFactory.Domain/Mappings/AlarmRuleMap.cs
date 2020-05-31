using IFactory.Domain.Entities;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace IFactory.Domain.Mappings
{
    public class AlarmRuleMap : EntityTypeConfiguration<AlarmRuleInfo>
    {
        public AlarmRuleMap()
        {
            base.ToTable("alarm_rule");
            base.HasKey<string>((AlarmRuleInfo x) => x.RuleDID);
            base.Property((AlarmRuleInfo x) => x.RuleDID).HasColumnName("rule_did").IsRequired().HasDatabaseGeneratedOption(new DatabaseGeneratedOption?(DatabaseGeneratedOption.None));
            base.Property((AlarmRuleInfo x) => x.AlarmContent).HasColumnName("alarm_content").IsOptional();
            base.Property<int>((AlarmRuleInfo x) => x.AlarmLocationImageDID).HasColumnName("alarm_location_image_did").IsOptional();
            base.Property((AlarmRuleInfo x) => x.AlarmReason).HasColumnName("alarm_reason").IsOptional();
            base.Property<int>((AlarmRuleInfo x) => x.AlarmTypeDID).HasColumnName("alarm_type_did").IsOptional();
            base.Property<int>((AlarmRuleInfo x) => x.CraftDID).HasColumnName("craft_did").IsOptional();
            base.Property<int>((AlarmRuleInfo x) => x.SolutionDID).HasColumnName("solution_did").IsOptional();
            base.Property<int>((AlarmRuleInfo x) => x.SolutionImageDID).HasColumnName("solution_image_did").IsOptional();
            base.Property<int>((AlarmRuleInfo x) => x.UnitDID).HasColumnName("unit_did").IsOptional();
            base.HasRequired<AlarmLocationImageInfo>((AlarmRuleInfo m) => m.AlarmLocationImage).WithMany().HasForeignKey((AlarmRuleInfo m) => m.AlarmLocationImageDID);
            base.HasRequired<CraftInfo>((AlarmRuleInfo m) => m.Craft).WithMany().HasForeignKey((AlarmRuleInfo m) => m.CraftDID);
            base.HasRequired<SolutionInfo>((AlarmRuleInfo m) => m.Solution).WithMany().HasForeignKey((AlarmRuleInfo m) => m.SolutionDID);
            base.HasRequired<SolutionImageInfo>((AlarmRuleInfo m) => m.SolutionImage).WithMany().HasForeignKey((AlarmRuleInfo m) => m.SolutionImageDID);
            base.HasRequired<UnitInfo>((AlarmRuleInfo m) => m.Unit).WithMany().HasForeignKey((AlarmRuleInfo m) => m.UnitDID);
            base.HasRequired<AlarmTypeInfo>((AlarmRuleInfo m) => m.AlarmType).WithMany().HasForeignKey((AlarmRuleInfo m) => m.AlarmTypeDID);
        }
    }
}
