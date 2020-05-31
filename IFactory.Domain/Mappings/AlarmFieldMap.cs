using IFactory.Domain.Entities;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace IFactory.Domain.Mappings
{
    public class AlarmFieldMap : EntityTypeConfiguration<AlarmFieldInfo>
    {
        public AlarmFieldMap()
        {
            base.ToTable("alarm_fields");
            base.HasKey<int>((AlarmFieldInfo x) => x.AlarmFieldId);
            base.Property<int>((AlarmFieldInfo x) => x.AlarmFieldId).IsRequired().HasDatabaseGeneratedOption(new DatabaseGeneratedOption?(DatabaseGeneratedOption.Identity));
            base.Property((AlarmFieldInfo x) => x.FieldName).IsRequired();
            base.Property((AlarmFieldInfo x) => x.FieldDescription).IsRequired();
        }
    }
}
