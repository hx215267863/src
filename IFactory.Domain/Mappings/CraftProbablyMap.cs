using IFactory.Domain.Entities;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace IFactory.Domain.Mappings
{
    public class CraftProbablyMap : EntityTypeConfiguration<CraftProbablyInfo>
    {
        public CraftProbablyMap()
        {
            base.ToTable("craft_probably");
            base.HasKey<int>((CraftProbablyInfo x) => x.CraftDID);
            base.Property<int>((CraftProbablyInfo x) => x.CraftDID).HasColumnName("craft_did").IsRequired().HasDatabaseGeneratedOption(new DatabaseGeneratedOption?(DatabaseGeneratedOption.None));
            base.Property((CraftProbablyInfo x) => x.BatteryBarCode).IsRequired();
            base.Property((CraftProbablyInfo x) => x.NowYield).IsRequired();
            base.Property((CraftProbablyInfo x) => x.TargetYield).IsRequired();
            base.Property((CraftProbablyInfo x) => x.UseName).IsRequired();
            base.Property((CraftProbablyInfo x) => x.CarNO).IsOptional();
        }
    }
}
