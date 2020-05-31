using IFactory.Domain.Crafts.MIB.Entities;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace IFactory.Domain.Crafts.MIB.Mappings
{
    public class MIBFacilityProductionDataMap : EntityTypeConfiguration<MIBFacilityProductionDataInfo>
    {
        public MIBFacilityProductionDataMap()
        {
            base.ToTable("mib_facility_production_data");
            base.HasKey<int>((MIBFacilityProductionDataInfo x) => x.Iden);
            base.Property<int>((MIBFacilityProductionDataInfo x) => x.Iden).IsRequired().HasDatabaseGeneratedOption(new DatabaseGeneratedOption?(DatabaseGeneratedOption.None));
            base.Property((MIBFacilityProductionDataInfo x) => x.BatteryBarCode).IsRequired();
            base.Property<int>((MIBFacilityProductionDataInfo x) => x.DeviceGroupDID).HasColumnName("device_group_did").IsRequired();
            base.Property((MIBFacilityProductionDataInfo x) => x.No).IsRequired();
            base.Property((MIBFacilityProductionDataInfo x) => x.StartDate).IsOptional();
            base.Property((MIBFacilityProductionDataInfo x) => x.ProductNo).IsOptional();
            base.Property((MIBFacilityProductionDataInfo x) => x.TabBarCode).IsOptional();
            base.Property((MIBFacilityProductionDataInfo x) => x.Operator).IsOptional();
            base.Property((MIBFacilityProductionDataInfo x) => x.InTime).IsOptional();
            base.Property((MIBFacilityProductionDataInfo x) => x.OutTime).IsOptional();
            base.Property<int>((MIBFacilityProductionDataInfo x) => x.BoxNum).IsOptional();
            base.Property<int>((MIBFacilityProductionDataInfo x) => x.Floor).IsOptional();
            base.Property<int>((MIBFacilityProductionDataInfo x) => x.Location).IsOptional();
            base.Property<int>((MIBFacilityProductionDataInfo x) => x.TempratureIndex).IsOptional();
            base.Property<float>((MIBFacilityProductionDataInfo x) => x.Temprature1).IsOptional();
            base.Property<float>((MIBFacilityProductionDataInfo x) => x.Temprature2).IsOptional();
            base.Property<float>((MIBFacilityProductionDataInfo x) => x.Vacuum).IsOptional();
            base.Property<int>((MIBFacilityProductionDataInfo x) => x.UserId).IsOptional();
        }
    }
}
