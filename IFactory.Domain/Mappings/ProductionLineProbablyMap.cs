using IFactory.Domain.Entities;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace IFactory.Domain.Mappings
{
    public class ProductionLineProbablyMap : EntityTypeConfiguration<ProductionLineProbablyInfo>
    {
        public ProductionLineProbablyMap()
        {
            base.ToTable("production_line_probably");
            base.HasKey<int>((ProductionLineProbablyInfo x) => x.DID);
            base.Property<int>((ProductionLineProbablyInfo x) => x.DID).IsRequired().HasDatabaseGeneratedOption(new DatabaseGeneratedOption?(DatabaseGeneratedOption.None));
            base.Property((ProductionLineProbablyInfo x) => x.Name).IsRequired();
            base.Property((ProductionLineProbablyInfo x) => x.ProductionType).HasColumnName("production_type").IsRequired();
            base.Property((ProductionLineProbablyInfo x) => x.NowYield).IsRequired();
            base.Property((ProductionLineProbablyInfo x) => x.TargetYield).IsRequired();
            base.Property((ProductionLineProbablyInfo x) => x.UserName).IsRequired();
        }
    }
}
