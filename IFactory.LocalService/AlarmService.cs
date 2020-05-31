using IFactory.Common;
using IFactory.Data;
using IFactory.Domain.Entities;
using IFactory.Domain.Models;
using MySql.Data.MySqlClient;
using PagedList;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;

namespace IFactory.LocalService
{
    public class AlarmService : BaseService, IAlarmService
    {
        public AlarmService(IDatabaseFactory databaseFactory)
          : base(databaseFactory)
        {
        }

        public void AddAlarmField(string fieldName, string fieldDescription)
        {
            using (this.DataContext.Database.BeginTransaction())
            {
                this.DataContext.Database.ExecuteSqlCommand("ALTER TABLE alarm_rule ADD " + fieldName + " varchar(255);");
                this.DataContext.AlarmFieldInfos.Add(new AlarmFieldInfo()
                {
                    FieldDescription = fieldDescription,
                    FieldName = fieldName
                });
                this.DataContext.SaveChanges();
            }
        }

        public void AddAlarmRule(AlarmRuleInfo alarmRuleInfo)
        {
            this.DataContext.AlarmRuleInfos.Add(alarmRuleInfo);
            this.DataContext.SaveChanges();
        }

        public IList<AlarmContentTopModel> GetAlarmContentTops(DateTime? alarmDateStart, DateTime? alarmDateEnd)
        {
            IQueryable<AlarmRecordInfo> queryable = base.DataContext.AlarmRecordInfos;
            if (alarmDateStart.HasValue)
            {
                queryable = from m in queryable
                            where m.AlarmTime >= alarmDateStart
                            select m;
            }
            if (alarmDateEnd.HasValue)
            {
                DateTime date = alarmDateEnd.Value.AddDays(1.0);
                queryable = from m in queryable
                            where m.AlarmTime < date
                            select m;
            }
            IQueryable<AlarmContentTopModel> source = from top in (from record in queryable
                                                                   join rule in base.DataContext.AlarmRuleInfos on record.RuleDID equals rule.RuleDID
                                                                   group rule by rule.AlarmTypeDID into g
                                                                   select new
                                                                   {
                                                                       AlarmContentDID = g.Key,
                                                                       Count = g.Count()
                                                                   }).Take(10)
                                                      join content in base.DataContext.AlarmContentInfos on top.AlarmContentDID equals content.DID
                                                      orderby top.Count descending
                                                      select new AlarmContentTopModel
                                                      {
                                                          RuleDID = top.AlarmContentDID.ToString(),
                                                          Content = content.Content,
                                                          Count = top.Count
                                                      };
            return source.ToList();
        }

        public IList<AlarmCraftTopModel> GetAlarmCraftTops(DateTime? alarmDateStart, DateTime? alarmDateEnd)
        {
            IQueryable<AlarmRecordInfo> queryable = base.DataContext.AlarmRecordInfos;
            if (alarmDateStart.HasValue)
            {
                queryable = from m in queryable
                            where m.AlarmTime >= alarmDateStart
                            select m;
            }
            if (alarmDateEnd.HasValue)
            {
                DateTime date = alarmDateEnd.Value.AddDays(1.0);
                queryable = from m in queryable
                            where m.AlarmTime < date
                            select m;
            }
            IQueryable<AlarmCraftTopModel> source = from record in queryable
                                                    join rule in base.DataContext.AlarmRuleInfos on record.RuleDID equals rule.RuleDID
                                                    group rule by rule.CraftDID into g
                                                    select new
                                                    {
                                                        CraftDID = g.Key,
                                                        Count = g.Count()
                                                    } into top
                                                    join craft in base.DataContext.CraftInfos on top.CraftDID equals craft.CraftDID
                                                    orderby top.Count descending
                                                    select new AlarmCraftTopModel
                                                    {
                                                        CraftDID = top.CraftDID,
                                                        CraftName = craft.CraftName,
                                                        Count = top.Count
                                                    };
            return source.ToList();
        }

        public IList<AlarmFacilityTopModel> GetAlarmFacilityTops(DateTime? alarmDateStart, DateTime? alarmDateEnd)
        {
            IQueryable<AlarmRecordInfo> queryable = base.DataContext.AlarmRecordInfos;
            if (alarmDateStart.HasValue)
            {
                queryable = from m in queryable
                            where m.AlarmTime >= alarmDateStart
                            select m;
            }
            if (alarmDateEnd.HasValue)
            {
                DateTime date = alarmDateEnd.Value.AddDays(1.0);
                queryable = from m in queryable
                            where m.AlarmTime < date
                            select m;
            }
            IQueryable<AlarmFacilityTopModel> source = from top in (from record in queryable
                                                                    join rule in base.DataContext.AlarmRuleInfos on record.RuleDID equals rule.RuleDID
                                                                    group rule by rule.FacilityDID into g
                                                                    select new
                                                                    {
                                                                        FacilityDID = g.Key,
                                                                        Count = g.Count<AlarmRuleInfo>()
                                                                    }).Take(10)
                                                       join facility in base.DataContext.FacilityInfos on top.FacilityDID equals facility.FacilityDID
                                                       orderby top.Count descending
                                                       select new AlarmFacilityTopModel
                                                       {
                                                           FacilityDID = top.FacilityDID,
                                                           FacilityName = facility.MMName,
                                                           Count = top.Count
                                                       };
            return source.ToList();
        }

        public AlarmFieldInfo GetAlarmField(string fieldName)
        {
            return base.DataContext.AlarmFieldInfos.FirstOrDefault((AlarmFieldInfo m) => m.FieldName == fieldName);
        }

        public AlarmRecordInfo GetAlarmRecord(int did)
        {
            return this.DataContext.AlarmRecordInfos.Find(did);
        }

        public AlarmRecordModel GetAlarmRecordModel(int did)
        {
            IQueryable<AlarmRecordModel> source = from record in base.DataContext.AlarmRecordInfos
                                                  join rule in base.DataContext.AlarmRuleInfos on record.RuleDID equals rule.RuleDID
                                                  join content in base.DataContext.AlarmContentInfos on rule.AlarmTypeDID equals content.DID
                                                  //join craft in base.DataContext.CraftInfos on record.CraftDID equals craft.CraftDID
                                                  //join process in base.DataContext.ProcessInfos on record.ProcessDID equals process.ProcessDID
                                                  join facility in base.DataContext.FacilityInfos on record.FacilityDID equals facility.FacilityDID
                                                  //join unit in base.DataContext.UnitInfos on record.UnitDID equals unit.UnitDID
                                                  join alarmLocationImage in base.DataContext.AlarmLocationImageInfos on rule.AlarmLocationImageDID equals alarmLocationImage.DID
                                                  join solution in base.DataContext.SolutionInfos on rule.SolutionDID equals solution.DID
                                                  join solutionImage in base.DataContext.SolutionImageInfos on rule.SolutionImageDID equals solutionImage.DID
                                                  join reason in base.DataContext.AlarmReasonInfos on rule.AlarmTypeDID equals reason.DID
                                                  join alarmType in base.DataContext.AlarmTypeInfos on rule.AlarmTypeDID equals alarmType.DID
                                                  where record.AlarmRecordDID == did
                                                  select new AlarmRecordModel
                                                  {
                                                      AlarmContent = content.Content,
                                                      DID = record.AlarmRecordDID,
                                                      AlarmTime = record.AlarmTime,
                                                      DisposeTime = record.DisposeTime,
                                                      Duration = record.Duration,
                                                      FacilityName = facility.MMName,
                                                      //ProcessName = process.Name,
                                                      //UnitName = unit.Name,
                                                      //CraftName = craft.Name,
                                                      AlarmCount = (int?)record.AlarmCount,
                                                      Address = record.Address,
                                                      AlarmLocationImagePath = alarmLocationImage.Path,
                                                      AlarmReason = reason.Content,
                                                      //BatteryBarCode = record.BatteryBarCode,
                                                      CraftDID = record.FacilityDID,
                                                      DisposeState = (int?)record.DisposeState,
                                                      FacilityDID = record.FacilityDID,
                                                      Handler = record.Handler,
                                                      //ProcessDID = record.ProcessDID,
                                                      Remark = record.Remark,
                                                      RuleDID = record.RuleDID,
                                                      SolutionImagePath = solutionImage.Path,
                                                      SolutionText = solution.Content,
                                                      //UnitDID = record.UnitDID,
                                                      AlarmTypeName = alarmType.Type
                                                  };
            AlarmRecordModel alarmRecordModel = source.FirstOrDefault<AlarmRecordModel>();
            if (alarmRecordModel != null)
            {
                alarmRecordModel.FieldValues = this.GetAlarmRuleFieldValues(alarmRecordModel.RuleDID);
            }
            return alarmRecordModel;
        }

        public AlarmTemporaryInfo GetAlarmTemporary(int did)
        {
            return this.DataContext.AlarmTemporaryInfos.Find(did);
        }

        public AlarmTemporaryModel GetAlarmTemporaryModel(int did)
        {
            IQueryable<AlarmTemporaryModel> source = from record in base.DataContext.AlarmTemporaryInfos
                                                     join rule in base.DataContext.AlarmRuleInfos on record.RuleDID equals rule.RuleDID
                                                     join content in base.DataContext.AlarmContentInfos on rule.AlarmTypeDID equals content.DID
                                                     //join craft in base.DataContext.CraftInfos on record.CraftDID equals craft.CraftDID
                                                     //join process in base.DataContext.ProcessInfos on record.ProcessDID equals process.ProcessDID
                                                     join facility in base.DataContext.FacilityInfos on record.FacilityDID equals facility.FacilityDID
                                                     //join unit in base.DataContext.UnitInfos on record.UnitDID equals unit.UnitDID
                                                     join alarmLocationImage in base.DataContext.AlarmLocationImageInfos on rule.AlarmLocationImageDID equals alarmLocationImage.DID
                                                     join solution in base.DataContext.SolutionInfos on rule.SolutionDID equals solution.DID
                                                     join solutionImage in base.DataContext.SolutionImageInfos on rule.SolutionImageDID equals solutionImage.DID
                                                     join reason in base.DataContext.AlarmReasonInfos on rule.AlarmTypeDID equals reason.DID
                                                     join alarmType in base.DataContext.AlarmTypeInfos on rule.AlarmTypeDID equals alarmType.DID
                                                     where record.AlarmTemporaryDID == did
                                                     select new AlarmTemporaryModel
                                                     {
                                                         AlarmContent = content.Content,
                                                         DID = record.FacilityDID,
                                                         AlarmTime = record.AlarmTime,
                                                         DisposeTime = record.DisposeTime,
                                                         Duration = record.Duration,
                                                         FacilityName = facility.MMName,
                                                         //ProcessName = process.Name,
                                                         //UnitName = unit.Name,
                                                         //CraftName = craft.Name,
                                                         //AlarmCount = (int?)record.AlarmCount,
                                                         //Address = record.Address,
                                                         //AlarmLocationImagePath = alarmLocationImage.Path,
                                                         //AlarmReasonText = reason.Content,
                                                         //BatteryBarCode = record.BatteryBarCode,
                                                         //CraftDID = record.CraftDID,
                                                         //DisposeState = (int?)record.DisposeState,
                                                         //FacilityDID = record.FacilityDID,
                                                         //Handler = record.Handler,
                                                         //ProcessDID = record.ProcessDID,
                                                         //Remark = record.Remark,
                                                         //RuleDID = record.RuleDID,
                                                         //SolutionImagePath = solutionImage.Path,
                                                         //SolutionText = solution.Content,
                                                         //UnitDID = record.UnitDID,
                                                         AlarmTypeName = alarmType.Type
                                                     };
            AlarmTemporaryModel alarmTemporaryModel = source.FirstOrDefault<AlarmTemporaryModel>();
            if (alarmTemporaryModel != null)
            {
                alarmTemporaryModel.FieldValues = this.GetAlarmRuleFieldValues(alarmTemporaryModel.RuleDID);
            }
            return alarmTemporaryModel;
        }

        public IList<AlarmTypeInfo> GetAlarmTypes()
        {
            return this.DataContext.AlarmTypeInfos.ToList();
        }

        public IList<AlarmFieldInfo> GetAllAlarmFields()
        {
            return this.DataContext.AlarmFieldInfos.ToList();
        }

        public IPagedList<AlarmRecordItem> GetPagedAlarmRecords(string keyword, DateTime? alarmDateStart, DateTime? alarmDateEnd, int pageNo, int pageSize)
        {
            IQueryable<AlarmRecordInfo> queryable = base.DataContext.AlarmRecordInfos;
            IQueryable<AlarmContentInfo> queryable2 = base.DataContext.AlarmContentInfos;
            if (!string.IsNullOrEmpty(keyword))
            {
                queryable2 = from m in queryable2
                             where m.Content.Contains(keyword)
                             select m;
            }
            if (alarmDateStart.HasValue)
            {
                queryable = from m in queryable
                            where m.AlarmTime >= alarmDateStart
                            select m;
            }
            if (alarmDateEnd.HasValue)
            {
                DateTime date = alarmDateEnd.Value.AddDays(1.0);
                queryable = from m in queryable
                            where m.AlarmTime < date
                            select m;
            }
            IQueryable<AlarmRecordItem> superset = from record in queryable
                                                   join rule in base.DataContext.AlarmRuleInfos on record.RuleDID equals rule.RuleDID
                                                   join content in queryable2 on rule.AlarmTypeDID equals content.DID
                                                   //join craft in base.DataContext.CraftInfos on record.CraftDID equals craft.CraftDID
                                                   //join process in base.DataContext.ProcessInfos on record. equals process.ProcessDID
                                                   join facility in base.DataContext.FacilityInfos on record.FacilityDID equals facility.FacilityDID
                                                   //join unit in base.DataContext.UnitInfos on record.UnitDID equals unit.UnitDID
                                                   join alarmType in base.DataContext.AlarmTypeInfos on rule.AlarmTypeDID equals alarmType.DID
                                                   orderby record.RuleDID descending
                                                   select new AlarmRecordItem
                                                   {
                                                       //AlarmContentText = content.Content,
                                                       //DID = record.DID,
                                                       AlarmTime = record.AlarmTime,
                                                       DisposeTime = record.DisposeTime,
                                                       Duration = record.Duration,
                                                       FacilityName = facility.MMName,
                                                       //ProcessName = process.Name,
                                                       //UnitName = unit.Name,
                                                       //CraftName = craft.Name,
                                                       RuleDID = record.RuleDID,
                                                       AlarmTypeName = alarmType.Type
                                                   };
            return new PagedList<AlarmRecordItem>(superset, pageNo, pageSize);
        }

        public IPagedList<AlarmTemporaryItem> GetPagedAlarmTemporaries(int? processDID, int pageNo, int pageSize)
        {
            IQueryable<AlarmTemporaryInfo> queryable = base.DataContext.AlarmTemporaryInfos;
            if (processDID.HasValue)
            {
                //queryable = from m in queryable
                //            where (int?)m.ProcessDID == processDID
                //            select m;
            }
            IQueryable<AlarmTemporaryItem> superset = from record in queryable
                                                      join rule in base.DataContext.AlarmRuleInfos on record.RuleDID equals rule.RuleDID
                                                      join content in base.DataContext.AlarmContentInfos on rule.AlarmTypeDID equals content.DID
                                                      //join craft in base.DataContext.CraftInfos on record.CraftDID equals craft.CraftDID
                                                      //join process in base.DataContext.ProcessInfos on record.ProcessDID equals process.DID
                                                      join facility in base.DataContext.FacilityInfos on record.FacilityDID equals facility.FacilityDID
                                                      //join unit in base.DataContext.UnitInfos on record.UnitDID equals unit.UnitDID
                                                      join alarmType in base.DataContext.AlarmTypeInfos on rule.AlarmTypeDID equals alarmType.DID
                                                      orderby record.RuleDID descending
                                                      select new AlarmTemporaryItem
                                                      {
                                                          //AlarmContentText = content.Content,
                                                          //DID = record.DID,
                                                          AlarmTime = record.AlarmTime,
                                                          FacilityName = facility.MMName,
                                                          //ProcessName = process.Name,
                                                          //UnitName = unit.Name,
                                                          //CraftName = craft.Name,
                                                          RuleDID = record.RuleDID,
                                                          AlarmTypeName = alarmType.Type
                                                      };
            return new PagedList<AlarmTemporaryItem>(superset, pageNo, pageSize);
        }

        public void SaveAlarmRuleFields(string alarmRuleDID, Dictionary<string, string> fields)
        {
            if (fields == null || fields.Count == 0)
                return;
            using (DbContextTransaction contextTransaction = this.DataContext.Database.BeginTransaction())
            {
                List<MySqlParameter> mySqlParameterList = new List<MySqlParameter>();
                string str = "UPDATE alarm_rule SET ";
                List<string> stringList = new List<string>();
                foreach (KeyValuePair<string, string> field in fields)
                {
                    MySqlParameter mySqlParameter = new MySqlParameter("?" + field.Key, MySqlDbType.VarChar, byte.MaxValue);
                    mySqlParameter.Value = field.Value;
                    mySqlParameterList.Add(mySqlParameter);
                    stringList.Add(field.Key + "=?" + field.Key);
                }
                string sql = str + string.Join(",", stringList) + " where rule_did=?rule_did;";
                MySqlParameter mySqlParameter1 = new MySqlParameter("?rule_did", MySqlDbType.Int32);
                mySqlParameter1.Value = alarmRuleDID;
                mySqlParameterList.Add(mySqlParameter1);
                this.DataContext.Database.ExecuteSqlCommand(sql, (object[])mySqlParameterList.ToArray());
                contextTransaction.Commit();
            }
        }

        public void UpdateAlarmRule(AlarmRuleInfo alarmRuleInfo)
        {
            this.DataContext.SaveChanges();
        }

        public IList<AlarmFieldValue> GetAlarmRuleFieldValues(string alarmRuleDID)
        {
            List<AlarmFieldValue> alarmFieldValueList = new List<AlarmFieldValue>();
            IList<AlarmFieldInfo> allAlarmFields = this.GetAllAlarmFields();
            if (allAlarmFields.Count > 0)
            {
                string commandText = "select " + string.Join(",", allAlarmFields.Select<AlarmFieldInfo, string>(m => m.FieldName)) + " from alarm_rule where rule_did=?rule_did;";
                DataRow row = MySqlHelper.ExecuteDataRow(DataContext.ConnectionString, commandText, new MySqlParameter("?rule_did", alarmRuleDID));
                if (row != null)
                {
                    foreach (AlarmFieldInfo alarmFieldInfo in allAlarmFields)
                        alarmFieldValueList.Add(new AlarmFieldValue()
                        {
                            AlarmFieldId = alarmFieldInfo.AlarmFieldId,
                            FieldName = alarmFieldInfo.FieldName,
                            FieldDescription = alarmFieldInfo.FieldDescription,
                            FieldValue = row.Field<string>(alarmFieldInfo.FieldName)
                        });
                }
            }
            return alarmFieldValueList;
        }

        public void UpdateAlarmTemporaryHandled(int alarmTemporaryDID, int handlerId)
        {
            //AlarmTemporaryInfo entity = this.DataContext.AlarmTemporaryInfos.Find(alarmTemporaryDID);
            //UserInfo userInfo = this.DataContext.UserInfos.Find(handlerId);
            //this.DataContext.AlarmRecordInfos.Add(new AlarmRecordInfo()
            //{
            //    Address = entity.Address,
            //    AlarmCount = entity..AlarmCount,
            //    AlarmTime = entity.AlarmTime,
            //    BatteryBarCode = entity.BatteryBarCode,
            //    CraftDID = entity.CraftDID,
            //    DID = entity.DID,
            //    DisposeState = 2,
            //    DisposeTime = new DateTime?(DateTime.Now),
            //    Duration = new int?((int)DateTime.Now.Subtract(entity.AlarmTime).TotalMinutes),
            //    FacilityDID = entity.FacilityDID,
            //    Handler = userInfo.Name,
            //    ProcessDID = entity.ProcessDID,
            //    Remark = entity.Remark,
            //    RuleDID = entity.RuleDID,
            //    UnitDID = entity.UnitDID
            //});
            //this.DataContext.AlarmTemporaryInfos.Remove(entity);
            //this.DataContext.SaveChanges();
        }

        public AlarmRuleInfo GetAlarmRule(string ruleDID)
        {
            return this.DataContext.AlarmRuleInfos.Find(ruleDID);
        }

        public string GetNextAlarmRuleDID(string craftNo)
        {
            string str;
            switch (CommonHelper.GetCraftShortNO(craftNo))
            {
                case "RFP":
                    str = "0X0A";
                    break;
                case "TAP":
                    str = "0X0B";
                    break;
                case "PAK":
                    str = "0X0C";
                    break;
                case "IN1":
                    str = "0X0D";
                    break;
                case "MLA":
                    str = "0X0E";
                    break;
                case "MIB":
                    str = "0X0F";
                    break;
                case "INJ":
                    str = "0X0G";
                    break;
                case "BAK":
                    str = "0X0H";
                    break;
                case "PIE":
                    str = "0X0I";
                    break;
                case "DGA":
                    str = "0X0J";
                    break;
                case "FEF":
                    str = "0X0K";
                    break;
                case "IN2":
                    str = "0X0L";
                    break;
                case "OC1":
                    str = "0X0M";
                    break;
                case "OCB":
                    str = "0X0N";
                    break;
                case "XRA":
                    str = "0X0O";
                    break;
                case "FQI":
                    str = "0X0P";
                    break;
                case "AVI":
                    str = "0X0Q";
                    break;
                default:
                    throw new Exception("CraftNo无效");
            }
            CodeGeneratorInfo entity = this.DataContext.CodeGeneratorInfos.Find(str);
            if (entity == null)
            {
                entity = new CodeGeneratorInfo();
                entity.Prefix = str;
                entity.SerialNo = 1;
                this.DataContext.CodeGeneratorInfos.Add(entity);
            }
            int serialNo = entity.SerialNo;
            entity.SerialNo = serialNo + 1;
            this.DataContext.SaveChanges();
            return str + serialNo.ToString().PadLeft(4, '0');
        }
    }
}
