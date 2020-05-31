using IFactory.Domain.Crafts.Base.Entities;
using IFactory.Domain.Crafts.Base.Mappings;
using IFactory.Domain.Crafts.OCV1.Entities;
using IFactory.Domain.Crafts.OCV1.Mappings;
using Microsoft.CSharp.RuntimeBinder;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using System.Data.Entity.ModelConfiguration.Configuration;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace IFactory.Data.Crafts
{
    public class OCV1DbContext : CraftDbContext
    {
        public override IQueryable<FacilityProductionDataInfo> FacilityProductionDataInfos
        {
            get
            {
                return this.Set<OCV1FacilityProductionDataInfo>();
            }
        }

        public override IQueryable<FacilityRunArgInfo> FacilityRunArgInfos
        {
            get
            {
                return this.Set<OCV1FacilityRunArgInfo>();
            }
        }

        static OCV1DbContext()
        {
            Database.SetInitializer<OCV1DbContext>(null);
        }

        public OCV1DbContext(string nameOrConnection)
          : base(nameOrConnection)
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Type configType = typeof(OCV1FacilityRunArgMap);
            foreach (Type type in (from type in Assembly.GetAssembly(configType).GetTypes()
                                   where type.Namespace == configType.Namespace
                                   select type).Where<Type>(delegate (Type type) {
                                       if ((type.BaseType == null) || !type.BaseType.IsGenericType)
                                       {
                                           return false;
                                       }
                                       if (!(type.BaseType.GetGenericTypeDefinition() == typeof(EntityTypeConfiguration<>)))
                                       {
                                           return type.BaseType.GetGenericTypeDefinition() == typeof(FacilityRunArgMap<>);
                                       }
                                       return true;
                                   }))
            {
                object obj2 = Activator.CreateInstance(type);
                modelBuilder.Configurations.Add((dynamic)obj2);
            }

        }
    }
}
