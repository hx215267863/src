using IFactory.Domain.Crafts.Baking.Entities;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace IFactory.Domain.Crafts.Baking.Mappings
{
    public class BakingFacilityProductionDataMap : EntityTypeConfiguration<BakingFacilityProductionDataInfo>
    {
        public BakingFacilityProductionDataMap()
        {
            ToTable("freebaking_facility_production_data");
            HasKey((BakingFacilityProductionDataInfo x) => x.Iden);
            base.Property<int>((BakingFacilityProductionDataInfo x) => x.Iden).IsRequired().HasDatabaseGeneratedOption(new DatabaseGeneratedOption?(DatabaseGeneratedOption.None));
            base.Property((BakingFacilityProductionDataInfo x) => x.BatteryBarCode).IsRequired();
            base.Property<int>((BakingFacilityProductionDataInfo x) => x.DeviceGroupDID).HasColumnName("device_group_did").IsRequired();
            base.Property<int>((BakingFacilityProductionDataInfo x) => x.No).IsRequired();
            base.Property((BakingFacilityProductionDataInfo x) => x.StartDate).IsOptional();
            base.Property((BakingFacilityProductionDataInfo x) => x.ProductNo).IsOptional();
            base.Property((BakingFacilityProductionDataInfo x) => x.TabBarCode).IsOptional();
            base.Property<float>((BakingFacilityProductionDataInfo x) => x.PumpPressureOut).IsOptional();
            base.Property<float>((BakingFacilityProductionDataInfo x) => x.ServoTravel1).IsOptional();
            base.Property<float>((BakingFacilityProductionDataInfo x) => x.ServoTravel2).IsOptional();
            base.Property<float>((BakingFacilityProductionDataInfo x) => x.ServoSpeed1).IsOptional();
            base.Property<float>((BakingFacilityProductionDataInfo x) => x.ServoSpeed2).IsOptional();
            base.Property<float>((BakingFacilityProductionDataInfo x) => x.PumpAddTime).IsOptional();
            base.Property<float>((BakingFacilityProductionDataInfo x) => x.PumpSaveTime).IsOptional();
            base.Property((BakingFacilityProductionDataInfo x) => x.OpenMould).IsOptional();
            base.Property<float>((BakingFacilityProductionDataInfo x) => x.ServoSortFirstDistance).IsOptional();
            base.Property((BakingFacilityProductionDataInfo x) => x.HeatNo).IsOptional();
            base.Property<float>((BakingFacilityProductionDataInfo x) => x.HeatTemp).IsOptional();
            base.Property<float>((BakingFacilityProductionDataInfo x) => x.HeatPressure).IsOptional();
            base.Property<float>((BakingFacilityProductionDataInfo x) => x.HeatTime).IsOptional();
            base.Property((BakingFacilityProductionDataInfo x) => x.TopNo).IsOptional();
            base.Property<float>((BakingFacilityProductionDataInfo x) => x.TopTemp).IsOptional();
            base.Property<float>((BakingFacilityProductionDataInfo x) => x.TopPressure).IsOptional();
            base.Property<float>((BakingFacilityProductionDataInfo x) => x.TopTime).IsOptional();
            base.Property((BakingFacilityProductionDataInfo x) => x.BottomNo).IsOptional();
            base.Property<float>((BakingFacilityProductionDataInfo x) => x.BottomTemp).IsOptional();
            base.Property<float>((BakingFacilityProductionDataInfo x) => x.BottomPressure).IsOptional();
            base.Property<float>((BakingFacilityProductionDataInfo x) => x.BottomTime).IsOptional();
            base.Property((BakingFacilityProductionDataInfo x) => x.SideNo).IsOptional();
            base.Property<float>((BakingFacilityProductionDataInfo x) => x.SideTemp).IsOptional();
            base.Property<float>((BakingFacilityProductionDataInfo x) => x.SidePressure).IsOptional();
            base.Property<float>((BakingFacilityProductionDataInfo x) => x.SideTime).IsOptional();
            base.Property((BakingFacilityProductionDataInfo x) => x.AngleNo).IsOptional();
            base.Property<float>((BakingFacilityProductionDataInfo x) => x.AngleTemp).IsOptional();
            base.Property<float>((BakingFacilityProductionDataInfo x) => x.AnglePressure).IsOptional();
            base.Property<float>((BakingFacilityProductionDataInfo x) => x.AngleTime).IsOptional();
            base.Property((BakingFacilityProductionDataInfo x) => x.InsulationTestNo).IsOptional();
            base.Property((BakingFacilityProductionDataInfo x) => x.InsulationTestResult).IsOptional();
            base.Property<float>((BakingFacilityProductionDataInfo x) => x.InsulationTabTestVoltage).IsOptional();
            base.Property<float>((BakingFacilityProductionDataInfo x) => x.InsulationTabTestTime).IsOptional();
            base.Property<float>((BakingFacilityProductionDataInfo x) => x.InsulationTabTestSize).IsOptional();
            base.Property<float>((BakingFacilityProductionDataInfo x) => x.InsulationTabBorderTestVoltage).IsOptional();
            base.Property<float>((BakingFacilityProductionDataInfo x) => x.InsulationTabBorderTestTime).IsOptional();
            base.Property<float>((BakingFacilityProductionDataInfo x) => x.InsulationTabBorderVoltage).IsOptional();
            base.Property<float>((BakingFacilityProductionDataInfo x) => x.InsulationTabBorderTime).IsOptional();
        }
    }
}
