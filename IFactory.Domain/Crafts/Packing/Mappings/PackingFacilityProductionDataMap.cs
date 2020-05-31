using IFactory.Domain.Crafts.Packing.Entities;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace IFactory.Domain.Crafts.Packing.Mappings
{
    public class PackingFacilityProductionDataMap : EntityTypeConfiguration<PackingFacilityProductionDataInfo>
    {
        public PackingFacilityProductionDataMap()
        {
            base.ToTable("packing_facility_production_data");
            base.HasKey<int>((PackingFacilityProductionDataInfo x) => x.Iden);
            base.Property<int>((PackingFacilityProductionDataInfo x) => x.Iden).IsRequired().HasDatabaseGeneratedOption(new DatabaseGeneratedOption?(DatabaseGeneratedOption.None));
            base.Property((PackingFacilityProductionDataInfo x) => x.BatteryBarCode).IsRequired();
            base.Property<int>((PackingFacilityProductionDataInfo x) => x.DeviceGroupDID).HasColumnName("device_group_did").IsRequired();
            base.Property<int>((PackingFacilityProductionDataInfo x) => x.No).IsRequired();
            base.Property((PackingFacilityProductionDataInfo x) => x.StartDate).IsOptional();
            base.Property((PackingFacilityProductionDataInfo x) => x.ProductNo).IsOptional();
            base.Property((PackingFacilityProductionDataInfo x) => x.TabBarCode).IsOptional();
            base.Property<float>((PackingFacilityProductionDataInfo x) => x.PumpPressureOut).IsOptional();
            base.Property<float>((PackingFacilityProductionDataInfo x) => x.ServoTravel1).IsOptional();
            base.Property<float>((PackingFacilityProductionDataInfo x) => x.ServoTravel2).IsOptional();
            base.Property<float>((PackingFacilityProductionDataInfo x) => x.ServoSpeed1).IsOptional();
            base.Property<float>((PackingFacilityProductionDataInfo x) => x.ServoSpeed2).IsOptional();
            base.Property<float>((PackingFacilityProductionDataInfo x) => x.PumpAddTime).IsOptional();
            base.Property<float>((PackingFacilityProductionDataInfo x) => x.PumpSaveTime).IsOptional();
            base.Property((PackingFacilityProductionDataInfo x) => x.OpenMould).IsOptional();
            base.Property<float>((PackingFacilityProductionDataInfo x) => x.ServoSortFirstDistance).IsOptional();
            base.Property((PackingFacilityProductionDataInfo x) => x.HeatNo).IsOptional();
            base.Property<float>((PackingFacilityProductionDataInfo x) => x.HeatTemp).IsOptional();
            base.Property<float>((PackingFacilityProductionDataInfo x) => x.HeatPressure).IsOptional();
            base.Property<float>((PackingFacilityProductionDataInfo x) => x.HeatTime).IsOptional();
            base.Property((PackingFacilityProductionDataInfo x) => x.TopNo).IsOptional();
            base.Property<float>((PackingFacilityProductionDataInfo x) => x.TopTemp).IsOptional();
            base.Property<float>((PackingFacilityProductionDataInfo x) => x.TopPressure).IsOptional();
            base.Property<float>((PackingFacilityProductionDataInfo x) => x.TopTime).IsOptional();
            base.Property((PackingFacilityProductionDataInfo x) => x.BottomNo).IsOptional();
            base.Property<float>((PackingFacilityProductionDataInfo x) => x.BottomTemp).IsOptional();
            base.Property<float>((PackingFacilityProductionDataInfo x) => x.BottomPressure).IsOptional();
            base.Property<float>((PackingFacilityProductionDataInfo x) => x.BottomTime).IsOptional();
            base.Property((PackingFacilityProductionDataInfo x) => x.SideNo).IsOptional();
            base.Property<float>((PackingFacilityProductionDataInfo x) => x.SideTemp).IsOptional();
            base.Property<float>((PackingFacilityProductionDataInfo x) => x.SidePressure).IsOptional();
            base.Property<float>((PackingFacilityProductionDataInfo x) => x.SideTime).IsOptional();
            base.Property((PackingFacilityProductionDataInfo x) => x.AngleNo).IsOptional();
            base.Property<float>((PackingFacilityProductionDataInfo x) => x.AngleTemp).IsOptional();
            base.Property<float>((PackingFacilityProductionDataInfo x) => x.AnglePressure).IsOptional();
            base.Property<float>((PackingFacilityProductionDataInfo x) => x.AngleTime).IsOptional();
            base.Property((PackingFacilityProductionDataInfo x) => x.InsulationTestNo).IsOptional();
            base.Property((PackingFacilityProductionDataInfo x) => x.InsulationTestResult).IsOptional();
            base.Property<float>((PackingFacilityProductionDataInfo x) => x.InsulationTabTestVoltage).IsOptional();
            base.Property<float>((PackingFacilityProductionDataInfo x) => x.InsulationTabTestTime).IsOptional();
            base.Property<float>((PackingFacilityProductionDataInfo x) => x.InsulationTabTestSize).IsOptional();
            base.Property<float>((PackingFacilityProductionDataInfo x) => x.InsulationTabBorderTestVoltage).IsOptional();
            base.Property<float>((PackingFacilityProductionDataInfo x) => x.InsulationTabBorderTestTime).IsOptional();
            base.Property<float>((PackingFacilityProductionDataInfo x) => x.InsulationTabBorderVoltage).IsOptional();
            base.Property<float>((PackingFacilityProductionDataInfo x) => x.InsulationTabBorderTime).IsOptional();
        }
    }
}
