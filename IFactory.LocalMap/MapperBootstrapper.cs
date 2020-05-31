using AutoMapper;
using IFactory.Common;
using IFactory.Domain.Common;
using IFactory.Domain.Crafts.Baking.Entities;
using IFactory.Domain.Crafts.Baking.Models;
using IFactory.Domain.Crafts.Degassing.Entities;
using IFactory.Domain.Crafts.Degassing.Models;
using IFactory.Domain.Crafts.FEF.Entities;
using IFactory.Domain.Crafts.FEF.Models;
using IFactory.Domain.Crafts.Injection.Entities;
using IFactory.Domain.Crafts.Injection.Models;
using IFactory.Domain.Crafts.Inspection1.Entities;
using IFactory.Domain.Crafts.Inspection1.Models;
using IFactory.Domain.Crafts.Inspection2.Entities;
using IFactory.Domain.Crafts.Inspection2.Models;
using IFactory.Domain.Crafts.MIB.Entities;
using IFactory.Domain.Crafts.MIB.Models;
using IFactory.Domain.Crafts.Mylar.Entities;
using IFactory.Domain.Crafts.Mylar.Models;
using IFactory.Domain.Crafts.OCV1.Entities;
using IFactory.Domain.Crafts.OCV1.Models;
using IFactory.Domain.Crafts.OCVB.Entities;
using IFactory.Domain.Crafts.OCVB.Models;
using IFactory.Domain.Crafts.Packing.Entities;
using IFactory.Domain.Crafts.Packing.Models;
using IFactory.Domain.Crafts.PIEF.Entities;
using IFactory.Domain.Crafts.PIEF.Models;
using IFactory.Domain.Crafts.RF.Entities;
using IFactory.Domain.Crafts.RF.Models;
using IFactory.Domain.Entities;
using IFactory.Domain.Models;
using IFactory.Domain.Models.Report;


namespace IFactory.LocalMap
{
    public static class MapperBootstrapper
    {
        public static void Run()
        {
            Mapper.Initialize(config =>
            {
                config.CreateMap<AlarmContentTopInfo, AlarmContentTopModel>();
                config.CreateMap<AlarmCraftTopInfo, AlarmCraftTopModel>();
                config.CreateMap<AlarmFacilityTopInfo, AlarmFacilityTopModel>();
                config.CreateMap<AlarmFieldInfo, AlarmFieldModel>();
                config.CreateMap<AlarmRecordInfo, AlarmRecordModel>();
                config.CreateMap<AlarmRuleInfo, AlarmRuleModel>();
                config.CreateMap<AlarmTemporaryInfo, AlarmTemporaryModel>();
                config.CreateMap<AlarmTypeInfo, AlarmTypeModel>();
                config.CreateMap<CraftInfo, CraftModel>();
                config.CreateMap<CraftProbablyInfo, CraftProbablyModel>();
                config.CreateMap<FacilityInfo, FacilityModel>();
                config.CreateMap<PLCStateInfo, PLCStateModel>();
                config.CreateMap<ProcessInfo, ProcessModel>();
                config.CreateMap<ProductionLineProbablyInfo, ProductionLineProbablyModel>();
                config.CreateMap<ProductionTypeInfo, ProductionTypeModel>();
                config.CreateMap<RoleInfo, RoleModel>();
                config.CreateMap<UnitInfo, UnitModel>();
                config.CreateMap<PermissionInfo, PermissionModel>();
                config.CreateMap<KanbanSettingInfo, KanbanSettingModel>();
                config.CreateMap<DateDataItem<double>, TextValueModel<double>>().ForMember(m => m.Text, f => f.MapFrom(m => m.Date.ToString("yyyy-MM-dd")));
                config.CreateMap<WeekDataItem<double>, TextValueModel<double>>().ForMember(m => m.Text, f => f.MapFrom(m => m.Week.ToString()));
                config.CreateMap<MonthDataItem<double>, TextValueModel<double>>().ForMember(m => m.Text, f => f.MapFrom(m => m.Month.ToString()));
                config.CreateMap<QuarterDataItem<double>, TextValueModel<double>>().ForMember((m => m.Text), f => f.MapFrom(m => m.Quarter.ToString()));
                config.CreateMap<YearDataItem<double>, TextValueModel<double>>().ForMember((m => m.Text), f => f.MapFrom((m => m.Year.ToString())));
                config.CreateMap<DateDataItem<int>, TextValueModel<int>>().ForMember((m => m.Text), f => f.MapFrom(m => m.Date.ToString("yyyy-MM-dd")));
                config.CreateMap<WeekDataItem<int>, TextValueModel<int>>().ForMember((m => m.Text), f => f.MapFrom(m => m.Week.ToString()));
                config.CreateMap<MonthDataItem<int>, TextValueModel<int>>().ForMember((m => m.Text), f => f.MapFrom(m => m.Month.ToString()));
                config.CreateMap<QuarterDataItem<int>, TextValueModel<int>>().ForMember((m => m.Text), f => f.MapFrom(m => m.Quarter.ToString()));
                config.CreateMap<YearDataItem<int>, TextValueModel<int>>().ForMember((m => m.Text), f => f.MapFrom(m => m.Year.ToString()));
                config.CreateMap<UserInfo, UserModel>().ForMember(m => m.RoleName, m => m.MapFrom(f => f.Role.RoleName)).ForMember(m => m.GenderDesc, m => m.MapFrom(f => f.Gender == new Gender?() ? default(string) : f.Gender.Value.GetDescription()));
                config.CreateMap<BakingFacilityProductionDataInfo, BakingFacilityProductionDataModel>();
                config.CreateMap<DegassingFacilityProductionDataInfo, DegassingFacilityProductionDataModel>();
                config.CreateMap<FEFFacilityProductionDataInfo, FEFFacilityProductionDataModel>();
                config.CreateMap<InjectionFacilityProductionDataInfo, InjectionFacilityProductionDataModel>();
                config.CreateMap<Inspection1FacilityProductionDataInfo, Inspection1FacilityProductionDataModel>();
                config.CreateMap<Inspection2FacilityProductionDataInfo, Inspection2FacilityProductionDataModel>();
                config.CreateMap<MIBFacilityProductionDataInfo, MIBFacilityProductionDataModel>();
                config.CreateMap<MylarFacilityProductionDataInfo, MylarFacilityProductionDataModel>();
                config.CreateMap<OCV1FacilityProductionDataInfo, OCV1FacilityProductionDataModel>();
                config.CreateMap<OCVBFacilityProductionDataInfo, OCVBFacilityProductionDataModel>();
                config.CreateMap<PackingFacilityProductionDataInfo, PackingFacilityProductionDataModel>();
                config.CreateMap<PIEFFacilityProductionDataInfo, PIEFFacilityProductionDataModel>();
                config.CreateMap<RFFacilityProductionDataInfo, RFFacilityProductionDataModel>();
            });
        }
    }
}
