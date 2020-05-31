using IFactory.Domain.Crafts.Base.Entities;
using IFactory.Domain.Crafts.Base.Mappings;
using IFactory.Domain.Entities;
using IFactory.Domain.Mappings;
using Microsoft.CSharp.RuntimeBinder;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using System.Data.Entity.ModelConfiguration.Configuration;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace IFactory.Data
{
    public class DataContext : DbContext
    {
        static DataContext()
        {
            Database.SetInitializer<DataContext>(null);
        }

        public DataContext() : base("DataContext")
        {
        }

        public static readonly string ConnectionString = ConfigurationManager.ConnectionStrings["DataContext"].ConnectionString;

        public virtual DbSet<AppInfo> AppInfos { get; set; }

        public virtual DbSet<AuthInfo> AuthInfos { get; set; }

        public virtual DbSet<UserInfo> UserInfos { get; set; }

        public virtual DbSet<RoleInfo> RoleInfos { get; set; }

        public virtual DbSet<CraftInfo> CraftInfos { get; set; }

        public virtual DbSet<CraftProbablyInfo> CraftProbablyInfos { get; set; }

        public virtual DbSet<ProcessInfo> ProcessInfos { get; set; }

        public virtual DbSet<ProductionLineProbablyInfo> ProductionLineProbablyInfos { get; set; }

        public virtual DbSet<PLCStateInfo> PLCStateInfos { get; set; }

        public virtual DbSet<ProductionTypeInfo> ProductionTypeInfos { get; set; }

        public virtual DbSet<FacilityInfo> FacilityInfos { get; set; }

        public virtual DbSet<AlarmContentInfo> AlarmContentInfos { get; set; }

        public virtual DbSet<AlarmContentTopInfo> AlarmContentTopInfos { get; set; }

        public virtual DbSet<AlarmCraftTopInfo> AlarmCraftTopInfos { get; set; }

        public virtual DbSet<AlarmFacilityTopInfo> AlarmFacilityTopInfos { get; set; }

        public virtual DbSet<AlarmFieldInfo> AlarmFieldInfos { get; set; }

        public virtual DbSet<AlarmReasonInfo> AlarmReasonInfos { get; set; }

        public virtual DbSet<AlarmRecordInfo> AlarmRecordInfos { get; set; }

        public virtual DbSet<AlarmRuleInfo> AlarmRuleInfos { get; set; }

        public virtual DbSet<AlarmTemporaryInfo> AlarmTemporaryInfos { get; set; }

        public virtual DbSet<AlarmTypeInfo> AlarmTypeInfos { get; set; }

        public virtual DbSet<UnitInfo> UnitInfos { get; set; }

        public virtual DbSet<SolutionInfo> SolutionInfos { get; set; }

        public virtual DbSet<SolutionImageInfo> SolutionImageInfos { get; set; }

        public virtual DbSet<AlarmLocationImageInfo> AlarmLocationImageInfos { get; set; }

        public virtual DbSet<PermissionInfo> PermissionInfos { get; set; }

        public virtual DbSet<KanbanSettingInfo> KanbanSettingInfos { get; set; }

        public virtual DbSet<CodeGeneratorInfo> CodeGeneratorInfos { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Type type1 = typeof(UserMap);
            foreach (Type type2 in Assembly.GetAssembly(type1).GetTypes().Where<Type>(delegate (Type type) {
                if (((type.BaseType == null) || type.IsAbstract) || !type.BaseType.IsGenericType)
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
                object obj2 = Activator.CreateInstance(type2);
                modelBuilder.Configurations.Add((dynamic)obj2);
            }
        }

        public IQueryable<FacilityProductionDataInfo> GetFacilityProductionDataInfos(string craftNO)
        {
            Type facilityProductionDataType = FacilityProductionDataInfo.GetFacilityProductionDataType(craftNO);
            return (IQueryable<FacilityProductionDataInfo>)this.Set(facilityProductionDataType);

        }

        public IQueryable<FacilityRunArgInfo> GetFacilityRunArgInfos(string craftNO)
        {
            Type facilityRunArgType = FacilityRunArgInfo.GetFacilityRunArgType(craftNO);
            return (IQueryable<FacilityRunArgInfo>)this.Set(facilityRunArgType);

        }
    }
}
