using IFactory.Domain.Entities;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace IFactory.Domain.Mappings
{
    public class UnitMap : EntityTypeConfiguration<UnitInfo>
    {
        public UnitMap()
        {
            base.ToTable("unit");
            base.HasKey<int>((UnitInfo x) => x.UnitDID);
            base.Property<int>((UnitInfo x) => x.UnitDID).HasColumnName("unit_did").IsRequired().HasDatabaseGeneratedOption(new DatabaseGeneratedOption?(DatabaseGeneratedOption.None));
            base.Property((UnitInfo x) => x.UnitNO).HasColumnName("unit_no").IsOptional();
            base.Property((UnitInfo x) => x.UnitName).HasColumnName("unit_name").IsOptional();
        }
    }
}
