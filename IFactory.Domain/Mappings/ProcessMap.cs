using IFactory.Domain.Entities;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace IFactory.Domain.Mappings
{
    public class ProcessMap : EntityTypeConfiguration<ProcessInfo>
    {
        public ProcessMap()
        {
            base.ToTable("process");
            base.HasKey<int>((ProcessInfo x) => x.ProcessDID);
            base.Property<int>((ProcessInfo x) => x.ProcessDID).HasColumnName("process_did").IsRequired().HasDatabaseGeneratedOption(new DatabaseGeneratedOption?(DatabaseGeneratedOption.None));
            base.Property((ProcessInfo x) => x.ProcessName).HasColumnName("process_name").IsRequired();
            base.Property((ProcessInfo x) => x.ProcessNO).HasColumnName("process_no").IsOptional();
            base.Property<int>((ProcessInfo x) => x.CraftDID).HasColumnName("craft_did").IsRequired();
            base.HasRequired<CraftInfo>((ProcessInfo m) => m.Craft).WithMany().HasForeignKey<int>((ProcessInfo m) => m.CraftDID);
        }
    }
}
