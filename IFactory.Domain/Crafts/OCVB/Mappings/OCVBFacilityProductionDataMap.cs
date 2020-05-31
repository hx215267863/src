// Decompiled with JetBrains decompiler
using IFactory.Domain.Crafts.OCVB.Entities;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq.Expressions;

namespace IFactory.Domain.Crafts.OCVB.Mappings
{
    public class OCVBFacilityProductionDataMap : EntityTypeConfiguration<OCVBFacilityProductionDataInfo>
    {
        public OCVBFacilityProductionDataMap()
        {
            this.ToTable("ocvb_facility_production_data");
            this.HasKey<int>((x => x.Iden));
            this.Property<int>((x => x.Iden)).IsRequired().HasDatabaseGeneratedOption(new DatabaseGeneratedOption?(DatabaseGeneratedOption.None));
            this.Property((x => x.BatteryBarCode)).IsRequired();
            this.Property<int>((x => x.DeviceGroupDID)).HasColumnName("device_group_did").IsRequired();
            this.Property<int>((x => x.No)).IsRequired();
            this.Property((x => x.StartDate)).IsOptional();
            this.Property((x => x.ProductNo)).IsOptional();
            this.Property((x => x.TabBarCode)).IsOptional();
            this.Property((x => x.Operator)).IsOptional();
            this.Property((x => x.TestTime)).IsOptional();
            this.Property<float>((x => x.Voltage)).IsOptional();
            this.Property<float>((x => x.coreResistance)).IsOptional();
            this.Property<float>((x => x.Temprature_E)).IsOptional();
            this.Property<float>((x => x.Temprature_base)).IsOptional();
            Property((x => x.OCVChannel)).IsOptional();
            this.Property<int>((x => x.IVChannel)).IsOptional();
            this.Property<float>((x => x.Result)).IsOptional();
            this.Property<float>((x => x.VIValue)).IsOptional();
            this.Property<int>((x => x.UserId)).IsOptional();
        }
    }
}
