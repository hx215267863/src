using IFactory.Domain.Entities;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace IFactory.Domain.Mappings
{
    public class ProductionTypeMap : EntityTypeConfiguration<ProductionTypeInfo>
    {
        public ProductionTypeMap()
        {
            base.ToTable("production_type");
            base.HasKey<int>((ProductionTypeInfo x) => x.DID);
            base.Property<int>((ProductionTypeInfo x) => x.DID).IsRequired().HasDatabaseGeneratedOption(new DatabaseGeneratedOption?(DatabaseGeneratedOption.None));
            base.Property((ProductionTypeInfo x) => x.ProductNo).IsOptional();
            base.Property((ProductionTypeInfo x) => x.MinWeight).IsOptional();
            base.Property((ProductionTypeInfo x) => x.MaxWeight).IsOptional();
            base.Property((ProductionTypeInfo x) => x.MinScope).IsOptional();
            base.Property((ProductionTypeInfo x) => x.MaxScope).IsOptional();
            base.Property<int>((ProductionTypeInfo x) => x.BarCodeLen).IsOptional();
            base.Property<int>((ProductionTypeInfo x) => x.PrefixLen).IsOptional();
            base.Property((ProductionTypeInfo x) => x.PrefixData).IsOptional();
            base.Property((ProductionTypeInfo x) => x.DefaultBarCode).IsOptional();
            base.Property<int>((ProductionTypeInfo x) => x.FacilityDID).HasColumnName("facility_did").IsOptional();
            base.Property((ProductionTypeInfo x) => x.Time).IsOptional();
        }
    }
}
