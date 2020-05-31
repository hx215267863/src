using IFactory.Domain.Crafts.Base.Entities;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace IFactory.Domain.Crafts.Base.Mappings
{
    //配置实体映射到表
    public abstract class FacilityRunArgMap<TEntity> : EntityTypeConfiguration<TEntity> where TEntity : FacilityRunArgInfo
    {
        public abstract string TableName
        {
            get;
        }

        public FacilityRunArgMap()
        {
            base.ToTable(this.TableName);
            base.HasKey<int>((TEntity x) => x.DID);
            base.Property<int>((TEntity x) => x.DID).IsRequired().HasDatabaseGeneratedOption(new DatabaseGeneratedOption?(DatabaseGeneratedOption.None));
            base.Property<int>((TEntity x) => x.FacilityDID).HasColumnName("facility_did").IsRequired();
            base.Property((TEntity x) => x.MCCollectDDate).IsRequired();
            base.Property<long>((TEntity x) => x.MCCount).IsOptional();
            base.Property<long>((TEntity x) => x.MCBanCount).IsOptional();
            base.Property<int>((TEntity x) => x.MCType).IsOptional();
            base.Property<int>((TEntity x) => x.MCHour).IsOptional();
            base.Property<long>((TEntity x) => x.MCTotalCount).IsOptional();
            base.Property<long>((TEntity x) => x.MCTotalBadCount).IsOptional();
            base.Property<long>((TEntity x) => x.MCOpenRunTime).IsOptional();
            base.Property<long>((TEntity x) => x.MCOpenRunTotalTime).IsOptional();
            base.Property<long>((TEntity x) => x.MCWaitTime).IsOptional();
            base.Property<long>((TEntity x) => x.MCWaitTotalTime).IsOptional();
            base.Property<long>((TEntity x) => x.MCAutoRunTime).IsOptional();
            base.Property<long>((TEntity x) => x.MCAutoRunTotalTime).IsOptional();
            base.Property<long>((TEntity x) => x.MCRuningTime).IsOptional();
            base.Property<long>((TEntity x) => x.MCRuningTotalTime).IsOptional();
            base.Property<long>((TEntity x) => x.MCAutoRunWarningTime).IsOptional();
            base.Property<long>((TEntity x) => x.MCAutoRunWarningTotalTime).IsOptional();
            base.Property<long>((TEntity x) => x.MCStopTime).IsOptional();
            base.Property<long>((TEntity x) => x.MCStopTotalTime).IsOptional();
        }
    }
}
