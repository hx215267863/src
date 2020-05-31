using IFactory.Domain.Entities;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace IFactory.Domain.Mappings
{
    public class AlarmLocationImageMap : EntityTypeConfiguration<AlarmLocationImageInfo>
    {
        public AlarmLocationImageMap()
        {
            base.ToTable("alarm_location_image");
            base.HasKey<int>((AlarmLocationImageInfo x) => x.DID);
            base.Property<int>((AlarmLocationImageInfo x) => x.DID).IsRequired().HasDatabaseGeneratedOption(new DatabaseGeneratedOption?(DatabaseGeneratedOption.Identity));
            base.Property((AlarmLocationImageInfo x) => x.Path).IsRequired();
        }
    }
}
