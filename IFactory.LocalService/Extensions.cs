using AutoMapper;
using IFactory.Domain.Entities;
using IFactory.Domain.Models;
using IFactory.Domain.Models.Report;
using PagedList;

namespace IFactory.LocalService
{
    public static class Extensions
    {
        public static AlarmContentTopModel ToModel(this AlarmContentTopInfo entity)
        {
            return Mapper.Map<AlarmContentTopInfo, AlarmContentTopModel>(entity);
        }

        public static AlarmCraftTopModel ToModel(this AlarmCraftTopInfo entity)
        {
            return Mapper.Map<AlarmCraftTopInfo, AlarmCraftTopModel>(entity);
        }

        public static AlarmFacilityTopModel ToModel(this AlarmFacilityTopInfo entity)
        {
            return Mapper.Map<AlarmFacilityTopInfo, AlarmFacilityTopModel>(entity);
        }

        public static AlarmFieldModel ToModel(this AlarmFieldInfo entity)
        {
            return Mapper.Map<AlarmFieldInfo, AlarmFieldModel>(entity);
        }

        public static AlarmRuleModel ToModel(this AlarmRuleInfo entity)
        {
            return Mapper.Map<AlarmRuleInfo, AlarmRuleModel>(entity);
        }

        public static AlarmTypeModel ToModel(this AlarmTypeInfo entity)
        {
            return Mapper.Map<AlarmTypeInfo, AlarmTypeModel>(entity);
        }

        public static CraftModel ToModel(this CraftInfo entity)
        {
            return Mapper.Map<CraftInfo, CraftModel>(entity);
        }

        public static CraftProbablyModel ToModel(this CraftProbablyInfo entity)
        {
            return Mapper.Map<CraftProbablyInfo, CraftProbablyModel>(entity);
        }

        public static FacilityModel ToModel(this FacilityInfo entity)
        {
            return Mapper.Map<FacilityInfo, FacilityModel>(entity);
        }

        public static PLCStateModel ToModel(this PLCStateInfo entity)
        {
            return Mapper.Map<PLCStateInfo, PLCStateModel>(entity);
        }

        public static ProcessModel ToModel(this ProcessInfo entity)
        {
            return Mapper.Map<ProcessInfo, ProcessModel>(entity);
        }

        public static ProductionLineProbablyModel ToModel(this ProductionLineProbablyInfo entity)
        {
            return Mapper.Map<ProductionLineProbablyInfo, ProductionLineProbablyModel>(entity);
        }

        public static ProductionTypeModel ToModel(this ProductionTypeInfo entity)
        {
            return Mapper.Map<ProductionTypeInfo, ProductionTypeModel>(entity);
        }

        public static RoleModel ToModel(this RoleInfo entity)
        {
            return Mapper.Map<RoleInfo, RoleModel>(entity);
        }

        public static UnitModel ToModel(this UnitInfo entity)
        {
            return Mapper.Map<UnitInfo, UnitModel>(entity);
        }

        public static UserModel ToModel(this UserInfo entity)
        {
            return Mapper.Map<UserInfo, UserModel>(entity);
        }

        public static PermissionModel ToModel(this PermissionInfo entity)
        {
            return Mapper.Map<PermissionInfo, PermissionModel>(entity);
        }

        public static KanbanSettingModel ToModel(this KanbanSettingInfo entity)
        {
            return Mapper.Map<KanbanSettingInfo, KanbanSettingModel>(entity);
        }

        public static TextValueModel<TValue> ToModel<TValue>(this DateDataItem<TValue> entity)
        {
            return Mapper.Map<DateDataItem<TValue>, TextValueModel<TValue>>(entity);
        }

        public static TextValueModel<TValue> ToModel<TValue>(this WeekDataItem<TValue> entity)
        {
            return Mapper.Map<WeekDataItem<TValue>, TextValueModel<TValue>>(entity);
        }

        public static TextValueModel<TValue> ToModel<TValue>(this MonthDataItem<TValue> entity)
        {
            return Mapper.Map<MonthDataItem<TValue>, TextValueModel<TValue>>(entity);
        }

        public static TextValueModel<TValue> ToModel<TValue>(this QuarterDataItem<TValue> entity)
        {
            return Mapper.Map<QuarterDataItem<TValue>, TextValueModel<TValue>>(entity);
        }

        public static TextValueModel<TValue> ToModel<TValue>(this YearDataItem<TValue> entity)
        {
            return Mapper.Map<YearDataItem<TValue>, TextValueModel<TValue>>(entity);
        }

        public static PagedData<T> ToPagedData<T>(this IPagedList<T> entity)
        {
            return new PagedData<T>(entity);
        }
    }
}
