using IFactory.Domain.Crafts.Injection.Entities;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace IFactory.Domain.Crafts.Injection.Mappings
{
    public class InjectionFacilityProductionDataMap : EntityTypeConfiguration<InjectionFacilityProductionDataInfo>
    {
        public InjectionFacilityProductionDataMap()
        {
            base.ToTable("injection_facility_production_data");
            base.HasKey<int>((InjectionFacilityProductionDataInfo x) => x.Iden);
            base.Property<int>((InjectionFacilityProductionDataInfo x) => x.Iden).IsRequired().HasDatabaseGeneratedOption(new DatabaseGeneratedOption?(DatabaseGeneratedOption.None));
            base.Property((InjectionFacilityProductionDataInfo x) => x.BatteryBarCode).IsRequired();
            base.Property<int>((InjectionFacilityProductionDataInfo x) => x.DeviceGroupDID).HasColumnName("device_group_did").IsRequired();
            base.Property<int>((InjectionFacilityProductionDataInfo x) => x.No).IsRequired();
            base.Property((InjectionFacilityProductionDataInfo x) => x.StartDate).IsOptional();
            base.Property((InjectionFacilityProductionDataInfo x) => x.BeforeWeight).IsOptional();
            base.Property((InjectionFacilityProductionDataInfo x) => x.BeforePass).IsOptional();
            base.Property((InjectionFacilityProductionDataInfo x) => x.AfterWeight).IsOptional();
            base.Property((InjectionFacilityProductionDataInfo x) => x.InjectionWeight).IsOptional();
            base.Property((InjectionFacilityProductionDataInfo x) => x.AfterPass).IsOptional();
            base.Property<int>((InjectionFacilityProductionDataInfo x) => x.InjectionNeedle).IsOptional();
            base.Property((InjectionFacilityProductionDataInfo x) => x.PumpTime).IsOptional();
            base.Property((InjectionFacilityProductionDataInfo x) => x.PackageTime).IsOptional();
            base.Property((InjectionFacilityProductionDataInfo x) => x.PocketTime).IsOptional();
            base.Property((InjectionFacilityProductionDataInfo x) => x.SaveTime).IsOptional();
            base.Property((InjectionFacilityProductionDataInfo x) => x.PackageTemp).IsOptional();
            base.Property((InjectionFacilityProductionDataInfo x) => x.ProductType).IsOptional();
            base.Property((InjectionFacilityProductionDataInfo x) => x.ProductNo).IsOptional();
        }
    }
}
