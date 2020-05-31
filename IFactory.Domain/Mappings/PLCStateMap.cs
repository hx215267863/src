using IFactory.Domain.Entities;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace IFactory.Domain.Mappings
{
    public class PLCStateMap : EntityTypeConfiguration<PLCStateInfo>
    {
        public PLCStateMap()
        {
            base.ToTable("plc_state");
            base.HasKey<int>((PLCStateInfo x) => x.PLCDID);
            base.Property<int>((PLCStateInfo x) => x.PLCDID).HasColumnName("plc_did").IsRequired().HasDatabaseGeneratedOption(new DatabaseGeneratedOption?(DatabaseGeneratedOption.None));
            base.Property((PLCStateInfo x) => x.PLCName).HasColumnName("plc_name").IsOptional();
            base.Property<int>((PLCStateInfo x) => x.State).IsOptional();
            base.Property<int>((PLCStateInfo x) => x.CraftDID).HasColumnName("craft_did").IsRequired();
        }
    }
}
