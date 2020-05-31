using IFactory.Domain.Crafts.Inspection2.Entities;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace IFactory.Domain.Crafts.Inspection2.Mappings
{
    public class Inspection2FacilityProductionDataMap : EntityTypeConfiguration<Inspection2FacilityProductionDataInfo>
    {
        public Inspection2FacilityProductionDataMap()
        {
            base.ToTable("autoInspection2_facility_production_data");
            base.HasKey<int>((Inspection2FacilityProductionDataInfo x) => x.Iden);
            base.Property<int>((Inspection2FacilityProductionDataInfo x) => x.Iden).IsRequired().HasDatabaseGeneratedOption(new DatabaseGeneratedOption?(DatabaseGeneratedOption.None));
            base.Property((Inspection2FacilityProductionDataInfo x) => x.BatteryBarCode).IsRequired();
            base.Property<int>((Inspection2FacilityProductionDataInfo x) => x.DeviceGroupDID).HasColumnName("device_group_did").IsRequired();
            base.Property((Inspection2FacilityProductionDataInfo x) => x.No).IsRequired();
            base.Property((Inspection2FacilityProductionDataInfo x) => x.StartDate).IsOptional();
            base.Property((Inspection2FacilityProductionDataInfo x) => x.ProductNo).IsOptional();
            base.Property((Inspection2FacilityProductionDataInfo x) => x.TabBarCode).IsOptional();
            base.Property<float>((Inspection2FacilityProductionDataInfo x) => x.PumpPressureOut).IsOptional();
            base.Property<float>((Inspection2FacilityProductionDataInfo x) => x.ServoTravel1).IsOptional();
            base.Property<float>((Inspection2FacilityProductionDataInfo x) => x.ServoTravel2).IsOptional();
            base.Property<float>((Inspection2FacilityProductionDataInfo x) => x.ServoSpeed1).IsOptional();
            base.Property<float>((Inspection2FacilityProductionDataInfo x) => x.ServoSpeed2).IsOptional();
            base.Property<float>((Inspection2FacilityProductionDataInfo x) => x.PumpAddTime).IsOptional();
            base.Property<float>((Inspection2FacilityProductionDataInfo x) => x.PumpSaveTime).IsOptional();
            base.Property((Inspection2FacilityProductionDataInfo x) => x.OpenMould).IsOptional();
            base.Property<float>((Inspection2FacilityProductionDataInfo x) => x.ServoSortFirstDistance).IsOptional();
            base.Property<float>((Inspection2FacilityProductionDataInfo x) => x.TopTemp).IsOptional();
            base.Property<float>((Inspection2FacilityProductionDataInfo x) => x.TopPressure).IsOptional();
            base.Property<float>((Inspection2FacilityProductionDataInfo x) => x.TopTime).IsOptional();
            base.Property<float>((Inspection2FacilityProductionDataInfo x) => x.BottomTemp).IsOptional();
            base.Property<float>((Inspection2FacilityProductionDataInfo x) => x.BottomPressure).IsOptional();
            base.Property<float>((Inspection2FacilityProductionDataInfo x) => x.BottomTime).IsOptional();
            base.Property<float>((Inspection2FacilityProductionDataInfo x) => x.SideTemp).IsOptional();
            base.Property<float>((Inspection2FacilityProductionDataInfo x) => x.SidePressure).IsOptional();
            base.Property<float>((Inspection2FacilityProductionDataInfo x) => x.SideTime).IsOptional();
            base.Property<float>((Inspection2FacilityProductionDataInfo x) => x.AngleTemp).IsOptional();
            base.Property<float>((Inspection2FacilityProductionDataInfo x) => x.AnglePressure).IsOptional();
            base.Property<float>((Inspection2FacilityProductionDataInfo x) => x.AngleTime).IsOptional();
            base.Property<float>((Inspection2FacilityProductionDataInfo x) => x.TabTestVoltage).IsOptional();
        }
    }
}
