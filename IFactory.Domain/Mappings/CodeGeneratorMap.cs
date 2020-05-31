using IFactory.Domain.Entities;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace IFactory.Domain.Mappings
{
    public class CodeGeneratorMap : EntityTypeConfiguration<CodeGeneratorInfo>
    {
        public CodeGeneratorMap()
        {
            base.ToTable("code_generators");
            base.HasKey<string>((CodeGeneratorInfo x) => x.Prefix);
            base.Property((CodeGeneratorInfo x) => x.Prefix).IsRequired().HasDatabaseGeneratedOption(new DatabaseGeneratedOption?(DatabaseGeneratedOption.None));
            base.Property<int>((CodeGeneratorInfo x) => x.SerialNo).IsRequired();
        }
    }
}
