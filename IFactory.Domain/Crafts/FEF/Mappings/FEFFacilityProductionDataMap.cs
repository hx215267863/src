using IFactory.Domain.Crafts.FEF.Entities;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace IFactory.Domain.Crafts.FEF.Mappings
{
    public class FEFFacilityProductionDataMap : EntityTypeConfiguration<FEFFacilityProductionDataInfo>
    {
        public FEFFacilityProductionDataMap()
        {
            base.ToTable("fef_facility_production_data");
            base.HasKey<int>((FEFFacilityProductionDataInfo x) => x.Iden);
            base.Property<int>((FEFFacilityProductionDataInfo x) => x.Iden).IsRequired().HasDatabaseGeneratedOption(new DatabaseGeneratedOption?(DatabaseGeneratedOption.None));
            base.Property((FEFFacilityProductionDataInfo x) => x.BatteryBarCode).IsRequired();
            base.Property<int>((FEFFacilityProductionDataInfo x) => x.DeviceGroupDID).HasColumnName("device_group_did").IsRequired();
            base.Property<int>((FEFFacilityProductionDataInfo x) => x.No).IsRequired();
            base.Property((FEFFacilityProductionDataInfo x) => x.StartDate).IsOptional();
            base.Property((FEFFacilityProductionDataInfo x) => x.ProductNo).IsOptional();
            base.Property((FEFFacilityProductionDataInfo x) => x.Operator).IsOptional();
            base.Property<float>((FEFFacilityProductionDataInfo x) => x.Temprature_Top).IsOptional();
            base.Property<float>((FEFFacilityProductionDataInfo x) => x.Temprature_Bottom).IsOptional();
            base.Property<float>((FEFFacilityProductionDataInfo x) => x.UserId).IsOptional();
        }
    }
}
