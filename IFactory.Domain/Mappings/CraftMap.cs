using IFactory.Domain.Entities;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace IFactory.Domain.Mappings
{
    public class CraftMap : EntityTypeConfiguration<CraftInfo>
    {
        public CraftMap()
        {
            base.ToTable("craft");
            base.HasKey<int>((CraftInfo x) => x.CraftDID);
            base.Property<int>((CraftInfo x) => x.CraftDID).HasColumnName("craft_did").IsRequired().HasDatabaseGeneratedOption(new DatabaseGeneratedOption?(DatabaseGeneratedOption.None));
            base.Property((CraftInfo x) => x.CraftName).HasColumnName("craft_name").IsRequired();
            base.Property((CraftInfo x) => x.CraftNO).HasColumnName("craft_no").IsOptional();
        }
    }
}
