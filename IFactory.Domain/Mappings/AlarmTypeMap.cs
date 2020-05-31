using IFactory.Domain.Entities;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace IFactory.Domain.Mappings
{
    public class AlarmTypeMap : EntityTypeConfiguration<AlarmTypeInfo>
    {
        public AlarmTypeMap()
        {
            base.ToTable("alarm_type");
            base.HasKey<int>((AlarmTypeInfo x) => x.DID);
            base.Property<int>((AlarmTypeInfo x) => x.DID).IsRequired().HasDatabaseGeneratedOption(new DatabaseGeneratedOption?(DatabaseGeneratedOption.None));
            base.Property((AlarmTypeInfo x) => x.Type).IsRequired();
        }
    }
}
