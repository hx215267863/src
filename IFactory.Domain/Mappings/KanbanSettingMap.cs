using IFactory.Domain.Common;
using IFactory.Domain.Entities;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace IFactory.Domain.Mappings
{
    public class KanbanSettingMap : EntityTypeConfiguration<KanbanSettingInfo>
    {
        public KanbanSettingMap()
        {
            base.ToTable("kanban_settings");
            base.HasKey<int>((KanbanSettingInfo x) => x.KanbanSettingId);
            base.Property<int>((KanbanSettingInfo x) => x.KanbanSettingId).IsRequired().HasDatabaseGeneratedOption(new DatabaseGeneratedOption?(DatabaseGeneratedOption.None));
            base.Property((KanbanSettingInfo x) => x.DateFormat).IsRequired();
            base.Property<TimeSectionType>((KanbanSettingInfo x) => x.ExcellentRateReportTimeSection).IsRequired();
            base.Property<TimeSectionType>((KanbanSettingInfo x) => x.AlarmReportTimeSection).IsRequired();
            base.Property<TimeSectionType>((KanbanSettingInfo x) => x.ProductionReportTimeSection).IsRequired();
            base.Property<int>((KanbanSettingInfo x) => x.RefreshInterval).IsRequired();
            base.Property((KanbanSettingInfo x) => x.TimeFormat).IsRequired();
        }
    }
}
