using IFactory.Domain.Crafts.PIEF.Entities;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace IFactory.Domain.Crafts.PIEF.Mappings
{
    public class PIEFFacilityProductionDataMap : EntityTypeConfiguration<PIEFFacilityProductionDataInfo>
    {
        public PIEFFacilityProductionDataMap()
        {
            base.ToTable("pief_facility_production_data");
            base.HasKey<int>((PIEFFacilityProductionDataInfo x) => x.Iden);
            base.Property<int>((PIEFFacilityProductionDataInfo x) => x.Iden).IsRequired().HasDatabaseGeneratedOption(new DatabaseGeneratedOption?(DatabaseGeneratedOption.None));
            base.Property((PIEFFacilityProductionDataInfo x) => x.BatteryBarCode).IsRequired();
            base.Property<int>((PIEFFacilityProductionDataInfo x) => x.DeviceGroupDID).HasColumnName("device_group_did").IsRequired();
            base.Property<int>((PIEFFacilityProductionDataInfo x) => x.No).IsRequired();
            base.Property((PIEFFacilityProductionDataInfo x) => x.StartDate).IsOptional();
            base.Property((PIEFFacilityProductionDataInfo x) => x.ProductNo).IsOptional();
            base.Property((PIEFFacilityProductionDataInfo x) => x.TabBarCode).IsOptional();
            base.Property<float>((PIEFFacilityProductionDataInfo x) => x.PumpPressureOut).IsOptional();
            base.Property<float>((PIEFFacilityProductionDataInfo x) => x.ServoTravel1).IsOptional();
            base.Property<float>((PIEFFacilityProductionDataInfo x) => x.ServoTravel2).IsOptional();
            base.Property<float>((PIEFFacilityProductionDataInfo x) => x.ServoSpeed1).IsOptional();
            base.Property<float>((PIEFFacilityProductionDataInfo x) => x.ServoSpeed2).IsOptional();
            base.Property<float>((PIEFFacilityProductionDataInfo x) => x.PumpAddTime).IsOptional();
            base.Property<float>((PIEFFacilityProductionDataInfo x) => x.PumpSaveTime).IsOptional();
            base.Property((PIEFFacilityProductionDataInfo x) => x.OpenMould).IsOptional();
            base.Property<float>((PIEFFacilityProductionDataInfo x) => x.ServoSortFirstDistance).IsOptional();
            base.Property((PIEFFacilityProductionDataInfo x) => x.HeatNo).IsOptional();
            base.Property<float>((PIEFFacilityProductionDataInfo x) => x.HeatTemp).IsOptional();
            base.Property<float>((PIEFFacilityProductionDataInfo x) => x.HeatPressure).IsOptional();
            base.Property<float>((PIEFFacilityProductionDataInfo x) => x.HeatTime).IsOptional();
            base.Property((PIEFFacilityProductionDataInfo x) => x.TopNo).IsOptional();
            base.Property<float>((PIEFFacilityProductionDataInfo x) => x.TopTemp).IsOptional();
            base.Property<float>((PIEFFacilityProductionDataInfo x) => x.TopPressure).IsOptional();
            base.Property<float>((PIEFFacilityProductionDataInfo x) => x.TopTime).IsOptional();
            base.Property((PIEFFacilityProductionDataInfo x) => x.BottomNo).IsOptional();
            base.Property<float>((PIEFFacilityProductionDataInfo x) => x.BottomTemp).IsOptional();
            base.Property<float>((PIEFFacilityProductionDataInfo x) => x.BottomPressure).IsOptional();
            base.Property<float>((PIEFFacilityProductionDataInfo x) => x.BottomTime).IsOptional();
            base.Property((PIEFFacilityProductionDataInfo x) => x.SideNo).IsOptional();
            base.Property<float>((PIEFFacilityProductionDataInfo x) => x.SideTemp).IsOptional();
            base.Property<float>((PIEFFacilityProductionDataInfo x) => x.SidePressure).IsOptional();
            base.Property<float>((PIEFFacilityProductionDataInfo x) => x.SideTime).IsOptional();
            base.Property((PIEFFacilityProductionDataInfo x) => x.AngleNo).IsOptional();
            base.Property<float>((PIEFFacilityProductionDataInfo x) => x.AngleTemp).IsOptional();
            base.Property<float>((PIEFFacilityProductionDataInfo x) => x.AnglePressure).IsOptional();
            base.Property<float>((PIEFFacilityProductionDataInfo x) => x.AngleTime).IsOptional();
            base.Property((PIEFFacilityProductionDataInfo x) => x.InsulationTestNo).IsOptional();
            base.Property((PIEFFacilityProductionDataInfo x) => x.InsulationTestResult).IsOptional();
            base.Property<float>((PIEFFacilityProductionDataInfo x) => x.InsulationTabTestVoltage).IsOptional();
            base.Property<float>((PIEFFacilityProductionDataInfo x) => x.InsulationTabTestTime).IsOptional();
            base.Property<float>((PIEFFacilityProductionDataInfo x) => x.InsulationTabTestSize).IsOptional();
            base.Property<float>((PIEFFacilityProductionDataInfo x) => x.InsulationTabBorderTestVoltage).IsOptional();
            base.Property<float>((PIEFFacilityProductionDataInfo x) => x.InsulationTabBorderTestTime).IsOptional();
            base.Property<float>((PIEFFacilityProductionDataInfo x) => x.InsulationTabBorderVoltage).IsOptional();
            base.Property<float>((PIEFFacilityProductionDataInfo x) => x.InsulationTabBorderTime).IsOptional();
        }
    }
}
