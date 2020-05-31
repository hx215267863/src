using IFactory.Domain.Entities;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace IFactory.Domain.Mappings
{
    public class SolutionMap : EntityTypeConfiguration<SolutionInfo>
    {
        public SolutionMap()
        {
            base.ToTable("solution");
            base.HasKey<int>((SolutionInfo x) => x.DID);
            base.Property<int>((SolutionInfo x) => x.DID).IsRequired().HasDatabaseGeneratedOption(new DatabaseGeneratedOption?(DatabaseGeneratedOption.Identity));
            base.Property((SolutionInfo x) => x.Content).IsOptional();
        }
    }
}
