using IFactory.Domain.Crafts.Inspection1.Entities;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace IFactory.Domain.Crafts.Inspection1.Mappings
{
    public class Inspection1FacilityProductionDataMap : EntityTypeConfiguration<Inspection1FacilityProductionDataInfo>
    {
        public Inspection1FacilityProductionDataMap()
        {
            base.ToTable("autoInspection_facility_production_data");
            base.HasKey<int>((Inspection1FacilityProductionDataInfo x) => x.Iden);
            base.Property<int>((Inspection1FacilityProductionDataInfo x) => x.Iden).IsRequired().HasDatabaseGeneratedOption(new DatabaseGeneratedOption?(DatabaseGeneratedOption.None));
            base.Property((Inspection1FacilityProductionDataInfo x) => x.BatteryBarCode).IsRequired();
            base.Property<int>((Inspection1FacilityProductionDataInfo x) => x.DeviceGroupDID).HasColumnName("device_group_did").IsRequired();
            base.Property((Inspection1FacilityProductionDataInfo x) => x.No).IsRequired();
            base.Property<int>((Inspection1FacilityProductionDataInfo x) => x.ProcessDID).IsOptional();
            base.Property<int>((Inspection1FacilityProductionDataInfo x) => x.Result).IsOptional();
            base.Property<int>((Inspection1FacilityProductionDataInfo x) => x.BackReturn).IsOptional();
            base.Property<int>((Inspection1FacilityProductionDataInfo x) => x.FacilityDID).IsOptional();
            base.Property((Inspection1FacilityProductionDataInfo x) => x.StartDate).IsOptional();
            base.Property((Inspection1FacilityProductionDataInfo x) => x.ProductNo).IsOptional();
            base.Property((Inspection1FacilityProductionDataInfo x) => x.TabBarCode).IsOptional();
            base.Property<float>((Inspection1FacilityProductionDataInfo x) => x.PumpPressureOut).IsOptional();
            base.Property<float>((Inspection1FacilityProductionDataInfo x) => x.ServoTravel1).IsOptional();
            base.Property<float>((Inspection1FacilityProductionDataInfo x) => x.ServoTravel2).IsOptional();
            base.Property<float>((Inspection1FacilityProductionDataInfo x) => x.ServoSpeed1).IsOptional();
            base.Property<float>((Inspection1FacilityProductionDataInfo x) => x.ServoSpeed2).IsOptional();
            base.Property<float>((Inspection1FacilityProductionDataInfo x) => x.PumpAddTime).IsOptional();
            base.Property<float>((Inspection1FacilityProductionDataInfo x) => x.PumpSaveTime).IsOptional();
            base.Property((Inspection1FacilityProductionDataInfo x) => x.OpenMould).IsOptional();
            base.Property<float>((Inspection1FacilityProductionDataInfo x) => x.ServoSortFirstDistance).IsOptional();
            base.Property((Inspection1FacilityProductionDataInfo x) => x.HeatNo).IsOptional();
            base.Property<float>((Inspection1FacilityProductionDataInfo x) => x.HeatTemp).IsOptional();
            base.Property<float>((Inspection1FacilityProductionDataInfo x) => x.HeatPressure).IsOptional();
            base.Property<float>((Inspection1FacilityProductionDataInfo x) => x.HeatTime).IsOptional();
            base.Property((Inspection1FacilityProductionDataInfo x) => x.TopNo).IsOptional();
            base.Property<float>((Inspection1FacilityProductionDataInfo x) => x.TopTemp).IsOptional();
            base.Property<float>((Inspection1FacilityProductionDataInfo x) => x.TopPressure).IsOptional();
            base.Property<float>((Inspection1FacilityProductionDataInfo x) => x.TopTime).IsOptional();
            base.Property((Inspection1FacilityProductionDataInfo x) => x.BottomNo).IsOptional();
            base.Property<float>((Inspection1FacilityProductionDataInfo x) => x.BottomTemp).IsOptional();
            base.Property<float>((Inspection1FacilityProductionDataInfo x) => x.BottomPressure).IsOptional();
            base.Property<float>((Inspection1FacilityProductionDataInfo x) => x.BottomTime).IsOptional();
            base.Property((Inspection1FacilityProductionDataInfo x) => x.SideNo).IsOptional();
            base.Property<float>((Inspection1FacilityProductionDataInfo x) => x.SideTemp).IsOptional();
            base.Property<float>((Inspection1FacilityProductionDataInfo x) => x.SidePressure).IsOptional();
            base.Property<float>((Inspection1FacilityProductionDataInfo x) => x.SideTime).IsOptional();
            base.Property((Inspection1FacilityProductionDataInfo x) => x.AngleNo).IsOptional();
            base.Property<float>((Inspection1FacilityProductionDataInfo x) => x.AngleTemp).IsOptional();
            base.Property<float>((Inspection1FacilityProductionDataInfo x) => x.AnglePressure).IsOptional();
            base.Property<float>((Inspection1FacilityProductionDataInfo x) => x.AngleTime).IsOptional();
            base.Property((Inspection1FacilityProductionDataInfo x) => x.InsulationTestNo).IsOptional();
            base.Property((Inspection1FacilityProductionDataInfo x) => x.InsulationTestResult).IsOptional();
            base.Property<float>((Inspection1FacilityProductionDataInfo x) => x.InsulationTabTestVoltage).IsOptional();
            base.Property<float>((Inspection1FacilityProductionDataInfo x) => x.InsulationTabTestTime).IsOptional();
            base.Property<float>((Inspection1FacilityProductionDataInfo x) => x.InsulationTabTestSize).IsOptional();
            base.Property<float>((Inspection1FacilityProductionDataInfo x) => x.InsulationTabBorderTestVoltage).IsOptional();
            base.Property<float>((Inspection1FacilityProductionDataInfo x) => x.InsulationTabBorderTestTime).IsOptional();
            base.Property<float>((Inspection1FacilityProductionDataInfo x) => x.InsulationTabBorderVoltage).IsOptional();
            base.Property<float>((Inspection1FacilityProductionDataInfo x) => x.InsulationTabBorderTime).IsOptional();
        }
    }
}
