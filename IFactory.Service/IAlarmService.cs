using IFactory.Domain.Entities;
using IFactory.Domain.Models;
using PagedList;
using System;
using System.Collections.Generic;

namespace IFactory.Service
{
    public interface IAlarmService
    {
        void AddAlarmField(string fieldName, string fieldDescription);

        AlarmFieldInfo GetAlarmField(string fieldName);

        IList<AlarmFieldInfo> GetAllAlarmFields();

        IList<AlarmContentTopModel> GetAlarmContentTops(DateTime? alarmDateStart, DateTime? alarmDateEnd);

        IList<AlarmCraftTopModel> GetAlarmCraftTops(DateTime? alarmDateStart, DateTime? alarmDateEnd);

        IList<AlarmFacilityTopModel> GetAlarmFacilityTops(DateTime? alarmDateStart, DateTime? alarmDateEnd);

        IPagedList<AlarmRecordItem> GetPagedAlarmRecords(string keyword, DateTime? alarmDateStart, DateTime? alarmDateEnd, int pageNo, int pageSize);

        IPagedList<AlarmTemporaryItem> GetPagedAlarmTemporaries(int? processDID, int pageNo, int pageSize);

        AlarmRecordInfo GetAlarmRecord(int did);

        AlarmTemporaryInfo GetAlarmTemporary(int did);

        IList<AlarmTypeInfo> GetAlarmTypes();

        void AddAlarmRule(AlarmRuleInfo alarmRuleInfo);

        void UpdateAlarmRule(AlarmRuleInfo alarmRuleInfo);

        void SaveAlarmRuleFields(string alarmRuleDID, Dictionary<string, string> fields);

        AlarmTemporaryModel GetAlarmTemporaryModel(int did);

        AlarmRecordModel GetAlarmRecordModel(int did);

        void UpdateAlarmTemporaryHandled(int alarmTemporaryDID, int handlerId);

        AlarmRuleInfo GetAlarmRule(string ruleDID);

        string GetNextAlarmRuleDID(string craftNo);
    }
}
