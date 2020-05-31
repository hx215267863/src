using IFactory.Domain.Entities;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace IFactory.Domain.Mappings
{
    public class SolutionImageMap : EntityTypeConfiguration<SolutionImageInfo>
    {
        public SolutionImageMap()
        {
            base.ToTable("solution_image");
            base.HasKey<int>((SolutionImageInfo x) => x.DID);
            base.Property<int>((SolutionImageInfo x) => x.DID).IsRequired().HasDatabaseGeneratedOption(new DatabaseGeneratedOption?(DatabaseGeneratedOption.Identity));
            base.Property((SolutionImageInfo x) => x.Path).IsRequired();
        }
    }
}
