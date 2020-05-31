using IFactory.Domain.Crafts.Mylar.Entities;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace IFactory.Domain.Crafts.Mylar.Mappings
{
    public class MylarFacilityProductionDataMap : EntityTypeConfiguration<MylarFacilityProductionDataInfo>
    {
        public MylarFacilityProductionDataMap()
        {
            base.ToTable("mylar_facility_production_data");
            base.HasKey<int>((MylarFacilityProductionDataInfo x) => x.Iden);
            base.Property<int>((MylarFacilityProductionDataInfo x) => x.Iden).IsRequired().HasDatabaseGeneratedOption(new DatabaseGeneratedOption?(DatabaseGeneratedOption.None));
            base.Property((MylarFacilityProductionDataInfo x) => x.BatteryBarCode).IsRequired();
            base.Property<int>((MylarFacilityProductionDataInfo x) => x.DeviceGroupDID).HasColumnName("device_group_did").IsRequired();
            base.Property<int>((MylarFacilityProductionDataInfo x) => x.No).IsRequired();
            base.Property((MylarFacilityProductionDataInfo x) => x.StartDate).IsOptional();
            base.Property((MylarFacilityProductionDataInfo x) => x.ProductNo).IsOptional();
            base.Property((MylarFacilityProductionDataInfo x) => x.TabBarCode).IsOptional();
            base.Property<float>((MylarFacilityProductionDataInfo x) => x.PumpPressureOut).IsOptional();
            base.Property<float>((MylarFacilityProductionDataInfo x) => x.ServoTravel1).IsOptional();
            base.Property<float>((MylarFacilityProductionDataInfo x) => x.ServoTravel2).IsOptional();
            base.Property<float>((MylarFacilityProductionDataInfo x) => x.ServoSpeed1).IsOptional();
            base.Property<float>((MylarFacilityProductionDataInfo x) => x.ServoSpeed2).IsOptional();
            base.Property<float>((MylarFacilityProductionDataInfo x) => x.PumpAddTime).IsOptional();
            base.Property<float>((MylarFacilityProductionDataInfo x) => x.PumpSaveTime).IsOptional();
            base.Property((MylarFacilityProductionDataInfo x) => x.OpenMould).IsOptional();
            base.Property<float>((MylarFacilityProductionDataInfo x) => x.ServoSortFirstDistance).IsOptional();
            base.Property((MylarFacilityProductionDataInfo x) => x.HeatNo).IsOptional();
            base.Property<float>((MylarFacilityProductionDataInfo x) => x.HeatTemp).IsOptional();
            base.Property<float>((MylarFacilityProductionDataInfo x) => x.HeatPressure).IsOptional();
            base.Property<float>((MylarFacilityProductionDataInfo x) => x.HeatTime).IsOptional();
            base.Property((MylarFacilityProductionDataInfo x) => x.TopNo).IsOptional();
            base.Property<float>((MylarFacilityProductionDataInfo x) => x.TopTemp).IsOptional();
            base.Property<float>((MylarFacilityProductionDataInfo x) => x.TopPressure).IsOptional();
            base.Property<float>((MylarFacilityProductionDataInfo x) => x.TopTime).IsOptional();
            base.Property((MylarFacilityProductionDataInfo x) => x.BottomNo).IsOptional();
            base.Property<float>((MylarFacilityProductionDataInfo x) => x.BottomTemp).IsOptional();
            base.Property<float>((MylarFacilityProductionDataInfo x) => x.BottomPressure).IsOptional();
            base.Property<float>((MylarFacilityProductionDataInfo x) => x.BottomTime).IsOptional();
            base.Property((MylarFacilityProductionDataInfo x) => x.SideNo).IsOptional();
            base.Property<float>((MylarFacilityProductionDataInfo x) => x.SideTemp).IsOptional();
            base.Property<float>((MylarFacilityProductionDataInfo x) => x.SidePressure).IsOptional();
            base.Property<float>((MylarFacilityProductionDataInfo x) => x.SideTime).IsOptional();
            base.Property((MylarFacilityProductionDataInfo x) => x.AngleNo).IsOptional();
            base.Property<float>((MylarFacilityProductionDataInfo x) => x.AngleTemp).IsOptional();
            base.Property<float>((MylarFacilityProductionDataInfo x) => x.AnglePressure).IsOptional();
            base.Property<float>((MylarFacilityProductionDataInfo x) => x.AngleTime).IsOptional();
            base.Property((MylarFacilityProductionDataInfo x) => x.InsulationTestNo).IsOptional();
            base.Property((MylarFacilityProductionDataInfo x) => x.InsulationTestResult).IsOptional();
            base.Property<float>((MylarFacilityProductionDataInfo x) => x.InsulationTabTestVoltage).IsOptional();
            base.Property<float>((MylarFacilityProductionDataInfo x) => x.InsulationTabTestTime).IsOptional();
            base.Property<float>((MylarFacilityProductionDataInfo x) => x.InsulationTabTestSize).IsOptional();
            base.Property<float>((MylarFacilityProductionDataInfo x) => x.InsulationTabBorderTestVoltage).IsOptional();
            base.Property<float>((MylarFacilityProductionDataInfo x) => x.InsulationTabBorderTestTime).IsOptional();
            base.Property<float>((MylarFacilityProductionDataInfo x) => x.InsulationTabBorderVoltage).IsOptional();
            base.Property<float>((MylarFacilityProductionDataInfo x) => x.InsulationTabBorderTime).IsOptional();
        }
    }
}
