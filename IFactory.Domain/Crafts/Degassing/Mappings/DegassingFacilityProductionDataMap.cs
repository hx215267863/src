using IFactory.Domain.Crafts.Degassing.Entities;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace IFactory.Domain.Crafts.Degassing.Mappings
{
    public class DegassingFacilityProductionDataMap : EntityTypeConfiguration<DegassingFacilityProductionDataInfo>
    {
        public DegassingFacilityProductionDataMap()
        {
            base.ToTable("degassing_facility_production_data");
            base.HasKey<int>((DegassingFacilityProductionDataInfo x) => x.Iden);
            base.Property<int>((DegassingFacilityProductionDataInfo x) => x.Iden).IsRequired().HasDatabaseGeneratedOption(new DatabaseGeneratedOption?(DatabaseGeneratedOption.None));
            base.Property((DegassingFacilityProductionDataInfo x) => x.BatteryBarCode).IsRequired();
            base.Property<int>((DegassingFacilityProductionDataInfo x) => x.DeviceGroupDID).HasColumnName("device_group_did").IsRequired();
            base.Property<int>((DegassingFacilityProductionDataInfo x) => x.No).IsRequired();
            base.Property((DegassingFacilityProductionDataInfo x) => x.StartDate).IsOptional();
            base.Property((DegassingFacilityProductionDataInfo x) => x.ProductNo).IsOptional();
            base.Property((DegassingFacilityProductionDataInfo x) => x.TabBarCode).IsOptional();
            base.Property<float>((DegassingFacilityProductionDataInfo x) => x.PumpPressureOut).IsOptional();
            base.Property<float>((DegassingFacilityProductionDataInfo x) => x.ServoTravel1).IsOptional();
            base.Property<float>((DegassingFacilityProductionDataInfo x) => x.ServoTravel2).IsOptional();
            base.Property<float>((DegassingFacilityProductionDataInfo x) => x.ServoSpeed1).IsOptional();
            base.Property<float>((DegassingFacilityProductionDataInfo x) => x.ServoSpeed2).IsOptional();
            base.Property<float>((DegassingFacilityProductionDataInfo x) => x.PumpAddTime).IsOptional();
            base.Property<float>((DegassingFacilityProductionDataInfo x) => x.PumpSaveTime).IsOptional();
            base.Property((DegassingFacilityProductionDataInfo x) => x.OpenMould).IsOptional();
            base.Property<float>((DegassingFacilityProductionDataInfo x) => x.ServoSortFirstDistance).IsOptional();
            base.Property((DegassingFacilityProductionDataInfo x) => x.HeatNo).IsOptional();
            base.Property<float>((DegassingFacilityProductionDataInfo x) => x.HeatTemp).IsOptional();
            base.Property<float>((DegassingFacilityProductionDataInfo x) => x.HeatPressure).IsOptional();
            base.Property<float>((DegassingFacilityProductionDataInfo x) => x.HeatTime).IsOptional();
            base.Property((DegassingFacilityProductionDataInfo x) => x.TopNo).IsOptional();
            base.Property<float>((DegassingFacilityProductionDataInfo x) => x.TopTemp).IsOptional();
            base.Property<float>((DegassingFacilityProductionDataInfo x) => x.TopPressure).IsOptional();
            base.Property<float>((DegassingFacilityProductionDataInfo x) => x.TopTime).IsOptional();
            base.Property((DegassingFacilityProductionDataInfo x) => x.BottomNo).IsOptional();
            base.Property<float>((DegassingFacilityProductionDataInfo x) => x.BottomTemp).IsOptional();
            base.Property<float>((DegassingFacilityProductionDataInfo x) => x.BottomPressure).IsOptional();
            base.Property<float>((DegassingFacilityProductionDataInfo x) => x.BottomTime).IsOptional();
            base.Property((DegassingFacilityProductionDataInfo x) => x.SideNo).IsOptional();
            base.Property<float>((DegassingFacilityProductionDataInfo x) => x.SideTemp).IsOptional();
            base.Property<float>((DegassingFacilityProductionDataInfo x) => x.SidePressure).IsOptional();
            base.Property<float>((DegassingFacilityProductionDataInfo x) => x.SideTime).IsOptional();
            base.Property((DegassingFacilityProductionDataInfo x) => x.AngleNo).IsOptional();
            base.Property<float>((DegassingFacilityProductionDataInfo x) => x.AngleTemp).IsOptional();
            base.Property<float>((DegassingFacilityProductionDataInfo x) => x.AnglePressure).IsOptional();
            base.Property<float>((DegassingFacilityProductionDataInfo x) => x.AngleTime).IsOptional();
            base.Property((DegassingFacilityProductionDataInfo x) => x.InsulationTestNo).IsOptional();
            base.Property((DegassingFacilityProductionDataInfo x) => x.InsulationTestResult).IsOptional();
            base.Property<float>((DegassingFacilityProductionDataInfo x) => x.InsulationTabTestVoltage).IsOptional();
            base.Property<float>((DegassingFacilityProductionDataInfo x) => x.InsulationTabTestTime).IsOptional();
            base.Property<float>((DegassingFacilityProductionDataInfo x) => x.InsulationTabTestSize).IsOptional();
            base.Property<float>((DegassingFacilityProductionDataInfo x) => x.InsulationTabBorderTestVoltage).IsOptional();
            base.Property<float>((DegassingFacilityProductionDataInfo x) => x.InsulationTabBorderTestTime).IsOptional();
            base.Property<float>((DegassingFacilityProductionDataInfo x) => x.InsulationTabBorderVoltage).IsOptional();
            base.Property<float>((DegassingFacilityProductionDataInfo x) => x.InsulationTabBorderTime).IsOptional();
        }
    }
}
