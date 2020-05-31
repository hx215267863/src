using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using IFactory.Domain.Entities;
using IFactory.Domain.Models;
using PagedList;
using IFactory.Common;
using MySql.Data.MySqlClient;

namespace IFactory.DB
{
    public class AlarmDB : BaseFacade
    {
        public DataTable getAlarmTemporarys()
        {
            string connectionString_ATL = @"server=127.0.0.1;database=IFactory;uid=root;pwd=root;Persist Security Info=True;Charset=utf8;";
            MySqlConnection connatl = new MySqlConnection(connectionString_ATL);
            connatl.Open();
            string sql = "select * from alarm_temporary;";
            MySqlDataAdapter b = new MySqlDataAdapter(sql, connatl);
            DataTable bt = new DataTable();
            b.Fill(bt);
            connatl.Close();
            return bt;
        }

        public IList<AlarmTemporaryInfo> GetAlarmTemporarys()
        {
            DataTable tb = getAlarmTemporarys();

            List<AlarmTemporaryInfo> lst = new List<AlarmTemporaryInfo>();
            if (tb != null && tb.Rows.Count > 0)
            {
                foreach (DataRow row in tb.Rows)
                {
                    AlarmTemporaryInfo info = new AlarmTemporaryInfo();
                    info.AlarmTemporaryDID = int.Parse(row[0].ToString());
                    info.RuleDID = row[1].ToString();
                    info.FacilityDID = int.Parse(row[2].ToString());
                    info.AlarmTime = DateTime.Parse(row[3].ToString());
                    if (row[4].ToString() != "")
                        info.DisposeState = int.Parse(row[4].ToString());
                    if (row[5].ToString() != "")
                        info.DisposeTime = DateTime.Parse(row[5].ToString());
                    info.Handler = row[6].ToString();
                    if (row[7].ToString() != "")
                        info.Duration = int.Parse(row[7].ToString());
                    info.Address = row[8].ToString();
                    info.Remark = row[9].ToString();

                    ProductionDB p = new ProductionDB();
                    FacilityInfo f = p.GetFacilityByID(info.FacilityDID);
                    info.Facility = f;

                    lst.Add(info);
                }
            }
            return lst;
        }

        private DataTable getPagedAlarmTemporaries(int? processDID,int CraftDID)
        {
            DateTime? DateE = DateTime.Now.Date;
            string connectionString_ATL = @"server=127.0.0.1;database=IFactory;uid=root;pwd=root;Persist Security Info=True;Charset=utf8;";
            //string connectionString_ATL = @"server=127.0.0.1;database=IFactory;uid=root;pwd=123456;Persist Security Info=True;Charset=utf8;";
            MySqlConnection connatl = new MySqlConnection(connectionString_ATL);
            connatl.Open();
            string sql = @"select temp.报警时间
                        , temp.报警内容
                        , temp.dispose_time
                        , temp.craft_name
                        , temp.duration
                        , temp.alarm_record_did
                        from alarm_table temp where temp.报警时间 >= '"+ DateE.Value.ToString()+
                    "' and temp.报警时间 <= '" + DateE.Value.AddDays(1.0).ToString() + "'";

            MySqlDataAdapter b = new MySqlDataAdapter(sql, connatl);
            DataTable bt = new DataTable();
            b.Fill(bt);
            connatl.Close();
            return bt;
        }

        /// <summary>
        /// 获取当前报警信息
        /// </summary>
        /// <param name="processDID">当前选择的工序</param>
        /// <param name="pageNo"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public IPagedList<AlarmTemporaryItem> GetPagedAlarmTemporaries(int? processDID, int pageNo, int pageSize, int CraftDID)
        {
            DataTable tb = getPagedAlarmTemporaries(processDID, CraftDID);

            List<AlarmTemporaryItem> lstAlarmRecord = new List<AlarmTemporaryItem>();
            if (tb != null && tb.Rows.Count > 0)
            {
                foreach(DataRow row in tb.Rows)
                {
                    AlarmTemporaryItem item = new AlarmTemporaryItem();
                    item.RuleDID = "701";
                    item.AlarmTime = DateTime.Parse(row[0].ToString());
                    item.AlarmContent = row[1].ToString();
                    item.DisposeTime = DateTime.Parse(row[2].ToString());
                    item.CraftName = row[3].ToString();
                    item.Duration = int.Parse(row[4].ToString());
                    item.AlarmDid = int.Parse(row[5].ToString());
                    lstAlarmRecord.Add(item);
                }
            }
            IQueryable<AlarmTemporaryItem> superset = lstAlarmRecord.AsQueryable();
            return new PagedList<AlarmTemporaryItem>(superset, pageNo, pageSize);
        }

        public void AddAlarmField(string fieldName, string fieldDescription)
        {
            string connectionString_ATL = @"server=127.0.0.1;database=IFactory;uid=root;pwd=root;Persist Security Info=True;Charset=utf8;";

            using (MySqlConnection conn = new MySqlConnection(connectionString_ATL))
            {
                conn.Open();
                IDbTransaction Idbtran = conn.BeginTransaction();
                try
                {
                    string sql1 = "ALTER TABLE alarm_rule ADD " + fieldName + " varchar(255);";
                    MySqlCommand sqlcmd1 = new MySqlCommand(sql1, conn);
                    sqlcmd1.ExecuteNonQuery();

                    string sql2 = string.Format("insert into alarm_fields (FieldName, FieldDescription) values ('{0}', '{1}');"
                        , fieldName, fieldDescription);
                    MySqlCommand sqlcmd2 = new MySqlCommand(sql2, conn);
                    sqlcmd1.ExecuteNonQuery();
                    Idbtran.Commit();
                }
                catch
                {
                    Idbtran.Rollback();
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        public DataTable getAllAlarmFields()
        {
            string connectionString_ATL = @"server=127.0.0.1;database=IFactory;uid=root;pwd=root;Persist Security Info=True;Charset=utf8;";
            MySqlConnection connatl = new MySqlConnection(connectionString_ATL);
            connatl.Open();
            string sql = "select * from alarm_fields;";
            MySqlDataAdapter b = new MySqlDataAdapter(sql, connatl);
            DataTable bt = new DataTable();
            b.Fill(bt);
            connatl.Close();
            return bt;
        }

        public IList<AlarmFieldInfo> GetAllAlarmFields()
        {
            DataTable tb = getAllAlarmFields();

            List<AlarmFieldInfo> lst = new List<AlarmFieldInfo>();
            if (tb != null && tb.Rows.Count > 0)
            {
                foreach (DataRow row in tb.Rows)
                {
                    AlarmFieldInfo info = new AlarmFieldInfo();
                    info.AlarmFieldId = int.Parse(row[0].ToString());
                    info.FieldName = row[1].ToString();
                    info.FieldDescription = row[2].ToString();
                    
                    lst.Add(info);
                }
            }
            return lst;
        }

        public DataTable getAlarmTypes()
        {
            string connectionString_ATL = @"server=127.0.0.1;database=IFactory;uid=root;pwd=root;Persist Security Info=True;Charset=utf8;";
            MySqlConnection connatl = new MySqlConnection(connectionString_ATL);
            connatl.Open();
            string sql = "select * from alarm_type;";
            MySqlDataAdapter b = new MySqlDataAdapter(sql, connatl);
            DataTable bt = new DataTable();
            b.Fill(bt);
            connatl.Close();
            return bt;
        }

        public IList<AlarmTypeInfo> GetAlarmTypes()
        {
            DataTable tb = getAlarmTypes();

            List<AlarmTypeInfo> lst = new List<AlarmTypeInfo>();
            if (tb != null && tb.Rows.Count > 0)
            {
                foreach (DataRow row in tb.Rows)
                {
                    AlarmTypeInfo info = new AlarmTypeInfo();
                    info.DID = int.Parse(row[0].ToString());
                    info.Type = row[1].ToString();

                    lst.Add(info);
                }
            }
            return lst;
        }

        public DataTable getCodeGenerator(string Prefix)
        {
            Database equipDB = dataProvider.EQUIPDataBase;
            string sql = string.Format("select * from code_generators where Prefix = '{0}';"
                , Prefix);
            DataSet ds = equipDB.ExecuteDataSet(CommandType.Text, sql);
            return ds.Tables[0];
        }

        public CodeGeneratorInfo GetCodeGenerator(string Prefix)
        {
            DataTable tb = getCodeGenerator(Prefix);
            if (tb != null && tb.Rows.Count > 0)
            {
                CodeGeneratorInfo info = new CodeGeneratorInfo();
                info.Prefix = tb.Rows[0][0].ToString();
                info.SerialNo = int.Parse(tb.Rows[0][1].ToString());
                return info;
            }
            else
                return null;
        }

        /// <summary>
        /// 新增、生成 rule_did
        /// </summary>
        /// <param name="craftNo"></param>
        /// <returns></returns>
        public string GetNextAlarmRuleDID(string craftNo)
        {
            string str;
            Database equipDB = dataProvider.EQUIPDataBase;

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
            CodeGeneratorInfo entity = GetCodeGenerator(str);
            if (entity == null)
            {
                entity = new CodeGeneratorInfo();
                entity.Prefix = str;
                entity.SerialNo = 0;  
                
                
                string sql = string.Format("insert code_generators (Prefix, SerialNo) values ('{0}', {1});"
                , entity.Prefix, entity.SerialNo);
                equipDB.ExecuteNonQuery(CommandType.Text, sql);
            }
            int serialNo = entity.SerialNo;
            entity.SerialNo = serialNo + 1;

            string sql2 = string.Format("update code_generators set SerialNo = {1} where Prefix = '{0}';"
                , entity.Prefix, entity.SerialNo);
            equipDB.ExecuteNonQuery(CommandType.Text, sql2);

            return str + entity.SerialNo.ToString().PadLeft(4, '0');
        }

        public void AddAlarmRule(AlarmRuleInfo alarmRuleInfo)
        {
            string connectionString_ATL = @"server=127.0.0.1;database=IFactory;uid=root;pwd=root;Persist Security Info=True;Charset=utf8;";

            using (MySqlConnection connatl = new MySqlConnection(connectionString_ATL))
            {
                connatl.Open();
                IDbTransaction Idbtran = connatl.BeginTransaction();
                try
                {
                    string sql = string.Format(
                 @"insert alarm_rule (rule_did, alarm_type_did, solution_did, solution_image_did, alarm_location_image_did
                    , craft_did, unit_did, alarm_content, alarm_reason)
                    values('{0}', '{1}', {2}, {3}, {4}, {5}, {6}, '{7}', '{8}');"
                    , alarmRuleInfo.RuleDID, alarmRuleInfo.AlarmTypeDID, alarmRuleInfo.SolutionDID, alarmRuleInfo.SolutionImageDID, alarmRuleInfo.AlarmLocationImageDID
                    , alarmRuleInfo.CraftDID, alarmRuleInfo.UnitDID, alarmRuleInfo.AlarmContent, alarmRuleInfo.AlarmReason);
                    MySqlCommand cmd = new MySqlCommand(sql, connatl);
                    cmd.ExecuteNonQuery();

                    Idbtran.Commit();
                }
                catch
                {
                    Idbtran.Rollback();
                }
                finally
                {
                    connatl.Close();
                }
            }
        }

        public void AddImagePath(AlarmRuleInfo alarmRuleInfo)
        {
            Database equipDB = dataProvider.EQUIPDataBase;
            string sql = string.Format(
                @"insert solution_image (path)
                    values('{0}');"
                , alarmRuleInfo.SolutionImage.Path);
            string sql2 = string.Format(
                @"insert alarm_location_image (path)
                    values('{0}');"
                , alarmRuleInfo.AlarmLocationImage.Path);
            equipDB.ExecuteNonQuery(CommandType.Text, sql + sql2);
        }

        public void SaveAlarmRuleFields(string alarmRuleDID, Dictionary<string, string> fields)
        {
            if (fields == null || fields.Count == 0)
                return;
            string connectionString_ATL = @"server=127.0.0.1;database=IFactory;uid=root;pwd=root;Persist Security Info=True;Charset=utf8;";
            using (MySqlConnection connatl = new MySqlConnection(connectionString_ATL))
            {
                connatl.Open();
                IDbTransaction Idbtran = connatl.BeginTransaction();
                try
                {
                    string str = "UPDATE alarm_rule SET ";
                    List<string> stringList = new List<string>();
                    foreach (KeyValuePair<string, string> field in fields)
                    {
                        stringList.Add(field.Key + "= " + field.Key);
                    }
                    string sql = str + string.Join(",", stringList) + " where rule_did = " + alarmRuleDID;
                    MySqlCommand cmd = new MySqlCommand(sql, connatl);
                    cmd.ExecuteNonQuery();
                    Idbtran.Commit();
                }
                catch
                {
                    Idbtran.Rollback();
                }
                finally
                {
                    connatl.Close();
                }
            }
        }

        public AlarmRecordModel GetAlarmRecordModel(int did)
        {
            Database equipDB = dataProvider.EQUIPDataBase;
            string sql = sql = string.Format(
                    @"select temp.alarm_record_did
						    , temp.rule_did
						    , cr.craft_did
						    , p.process_did
						    , f.facility_did
						    , u.unit_did
						    , temp.alarm_time
						    , temp.dispose_state
						    , temp.dispose_state
						    , temp.dispose_time
						    , temp.handler
						    , temp.duration
						    , temp.address
						    , temp.remark
	                        , rule.alarm_content
	                        , rule.alarm_reason
	                        , sl.content
	                        , slimage.path
	                        , image.path
	                        , cr.craft_name
	                        , u.unit_name
	                        , p.process_name
	                        , f.MMName
	                        , almtp.type
                    from alarm_record temp 
                    join facility_info f on f.facility_did = temp.facility_did
                    join alarm_rule rule on rule.rule_did = temp.rule_did
                    join craft cr on cr.craft_did = rule.craft_did
                    join unit u on u.unit_did = rule.unit_did
                    join alarm_type almtp on almtp.did = rule.alarm_type_did
                    join process p on p.process_did = f.process_did
					left join alarm_location_image image on image.did = rule.alarm_location_image_did
					left join solution sl on sl.did = rule.solution_did
					left join solution_image slimage on slimage.did = rule.solution_image_did
                   where temp.alarm_record_did = {0};"
                                        , did);

            DataSet ds = equipDB.ExecuteDataSet(CommandType.Text, sql);
            DataTable tb = ds.Tables[0];
            if (tb != null && tb.Rows.Count > 0)
            {
                AlarmRecordModel model = new AlarmRecordModel();

                model.DID = int.Parse(tb.Rows[0][0].ToString());
                model.RuleDID = tb.Rows[0][1].ToString();
                model.CraftDID = int.Parse(tb.Rows[0][2].ToString());
                model.ProcessDID = int.Parse(tb.Rows[0][3].ToString());
                model.FacilityDID = int.Parse(tb.Rows[0][4].ToString());
                model.UnitDID = int.Parse(tb.Rows[0][5].ToString());
                model.AlarmTime = DateTime.Parse(tb.Rows[0][6].ToString());
                if (tb.Rows[0][7].ToString() != "")
                    model.AlarmCount = int.Parse(tb.Rows[0][7].ToString());
                if (tb.Rows[0][8].ToString() != "")
                    model.DisposeState = int.Parse(tb.Rows[0][8].ToString());
                if (tb.Rows[0][9].ToString() != "")
                    model.DisposeTime = DateTime.Parse(tb.Rows[0][9].ToString());
                model.Handler = tb.Rows[0][10].ToString();
                if (tb.Rows[0][11].ToString() != "")
                    model.Duration = int.Parse(tb.Rows[0][11].ToString());
                model.Address = tb.Rows[0][12].ToString();
                model.Remark = tb.Rows[0][13].ToString();
                model.AlarmContent = tb.Rows[0][14].ToString();
                model.AlarmReason = tb.Rows[0][15].ToString();
                model.SolutionText = tb.Rows[0][16].ToString();
                model.SolutionImagePath = tb.Rows[0][17].ToString();
                model.AlarmLocationImagePath = tb.Rows[0][18].ToString();
                model.CraftName = tb.Rows[0][19].ToString();
                model.UnitName = tb.Rows[0][20].ToString();
                model.ProcessName = tb.Rows[0][21].ToString();
                model.FacilityName = tb.Rows[0][22].ToString();
                model.AlarmTypeName = tb.Rows[0][23].ToString();

                model.FieldValues = this.GetAlarmRuleFieldValues(model.RuleDID);
                return model;
            }
            else
            {
                throw new Exception("");
            }
        }

        public IList<AlarmFieldValue> GetAlarmRuleFieldValues(string alarmRuleDID)
        {
            List<AlarmFieldValue> alarmFieldValueList = new List<AlarmFieldValue>();
            IList<AlarmFieldInfo> allAlarmFields = this.GetAllAlarmFields();
            if (allAlarmFields.Count > 0)
            {
                string sql = string.Format("select " + string.Join(",", allAlarmFields.Select(m => m.FieldName)) + " from alarm_rule where 'rule_did = {0}';", alarmRuleDID);
                Database equipDB = dataProvider.EQUIPDataBase;
                DataSet ds = equipDB.ExecuteDataSet(CommandType.Text, sql);
                DataTable tb = ds.Tables[0];
                if (tb != null && tb.Rows.Count > 0)
                {
                    DataRow row = tb.Rows[0];
                    /*
                    foreach (AlarmFieldInfo alarmFieldInfo in allAlarmFields)
                        alarmFieldValueList.Add(new AlarmFieldValue()
                        {
                            AlarmFieldId = alarmFieldInfo.AlarmFieldId,
                            FieldName = alarmFieldInfo.FieldName,
                            FieldDescription = alarmFieldInfo.FieldDescription,
                            FieldValue = row.Field<string>(alarmFieldInfo.FieldName)
                        });
                    */
                }
            }
            return alarmFieldValueList;
        }

        /// <summary>
        /// 统计前10大报警总次数的工序
        /// </summary>
        /// <param name="alarmDateStart"></param>
        /// <param name="alarmDateEnd"></param>
        /// <returns></returns>
        public IList<AlarmFacilityTopModel> GetAlarmFacilityTops(DateTime? alarmDateStart, DateTime? alarmDateEnd)
        {
            string connectionString_ATL = @"server=127.0.0.1;database=IFactory;uid=root;pwd=root;Persist Security Info=True;Charset=utf8;";
            MySqlConnection connatl = new MySqlConnection(connectionString_ATL);
            connatl.Open();
            string sql = @"select f.facility_did, f.MMName, count(*) 
                                  
                            from alarm_record t
                            join facility_info f on f.facility_did = t.facility_did
                            join alarm_rule r on r.rule_did = t.rule_did
                            where r.craft_did = 4 ";
            string w1 = alarmDateStart.HasValue ? "and t.alarm_time >= '" + alarmDateStart.ToString() + "'" : "";
            string w2 = alarmDateEnd.HasValue ? " and t.alarm_time < '" + alarmDateEnd.Value.AddDays(1.0).ToString() + "'" : "";
            string w3 = "group by f.facility_did order by count(*) desc limit 10";
            MySqlDataAdapter b = new MySqlDataAdapter(sql +w1 +w2 +w3, connatl);
            DataTable bt = new DataTable();
            b.Fill(bt);
            connatl.Close();

            List<AlarmFacilityTopModel> lstAlarmFacilityTopModel = new List<AlarmFacilityTopModel>();

            if (bt != null && bt.Rows.Count > 0)
            {
                foreach (DataRow row in bt.Rows)
                {
                    AlarmFacilityTopModel info = new AlarmFacilityTopModel();
                    info.FacilityDID = int.Parse(row[0].ToString());
                    info.FacilityName = row[1].ToString();
                    info.Count = int.Parse(row[2].ToString());

                    lstAlarmFacilityTopModel.Add(info);
                }
            }
            return lstAlarmFacilityTopModel;
        }

        /// <summary>
        /// 统计前10大报警总次数的工序2
        /// </summary>
        /// <param name="alarmDateStart"></param>
        /// <param name="alarmDateEnd"></param>
        /// <returns></returns>
        public IList<AlarmFacilityTopModel> GetAlarmFacilityTops2(DateTime? alarmDateStart, DateTime? alarmDateEnd)
        {
            string connectionString_ATL = @"server=127.0.0.1;database=IFactory;uid=root;pwd=root;Persist Security Info=True;Charset=utf8;";
            MySqlConnection connatl = new MySqlConnection(connectionString_ATL);
            connatl.Open();
            string sql = @"select f.facility_did, f.MMName, count(*) 
                                  
                            from alarm_record t
                            join facility_info f on f.facility_did = t.facility_did
                            join alarm_rule r on r.rule_did = t.rule_did
                            where r.craft_did = 12 ";
            string w1 = alarmDateStart.HasValue ? "and t.alarm_time >= '" + alarmDateStart.ToString() + "'" : "";
            string w2 = alarmDateEnd.HasValue ? " and t.alarm_time < '" + alarmDateEnd.Value.AddDays(1.0).ToString() + "'" : "";
            string w3 = "group by f.facility_did order by count(*) desc limit 10";
            MySqlDataAdapter b = new MySqlDataAdapter(sql + w1 + w2 + w3, connatl);
            DataTable bt = new DataTable();
            b.Fill(bt);
            connatl.Close();

            List<AlarmFacilityTopModel> lstAlarmFacilityTopModel = new List<AlarmFacilityTopModel>();

            if (bt != null && bt.Rows.Count > 0)
            {
                foreach (DataRow row in bt.Rows)
                {
                    AlarmFacilityTopModel info = new AlarmFacilityTopModel();
                    info.FacilityDID = int.Parse(row[0].ToString());
                    info.FacilityName = row[1].ToString();
                    info.Count = int.Parse(row[2].ToString());

                    lstAlarmFacilityTopModel.Add(info);
                }
            }
            return lstAlarmFacilityTopModel;
        }

        /// <summary>
        /// 统计前10大报警总次数的报警规则
        /// </summary>
        /// <param name="alarmDateStart"></param>
        /// <param name="alarmDateEnd"></param>
        /// <returns></returns>
        public IList<AlarmContentTopModel> GetAlarmContentTops(DateTime? alarmDateStart, DateTime? alarmDateEnd)
        {
            string connectionString_ATL = @"server=127.0.0.1;database=IFactory;uid=root;pwd=root;Persist Security Info=True;Charset=utf8;";
            MySqlConnection connatl = new MySqlConnection(connectionString_ATL);
            connatl.Open();
            string sql = @"select r.rule_did, r.alarm_content, count(*) 
                            from alarm_record t
                            join alarm_rule r on r.rule_did = t.rule_did
                            where r.craft_did = 4 ";
            string w1 = alarmDateStart.HasValue ? "and t.alarm_time >= '" + alarmDateStart.ToString() + "'" : "";
            string w2 = alarmDateEnd.HasValue ? " and t.alarm_time < '" + alarmDateEnd.Value.AddDays(1.0).ToString() + "'" : "";
            string w3 = "group by r.rule_did order by count(*) desc limit 10";
            MySqlDataAdapter b = new MySqlDataAdapter(sql + w1 + w2 + w3, connatl);
            DataTable bt = new DataTable();
            b.Fill(bt);
            connatl.Close();

            List<AlarmContentTopModel> lstAlarmContentTopModel = new List<AlarmContentTopModel>();

            if (bt != null && bt.Rows.Count > 0)
            {
                foreach (DataRow row in bt.Rows)
                {
                    AlarmContentTopModel info = new AlarmContentTopModel();
                    info.RuleDID = row[0].ToString();
                    info.Content = row[1].ToString();
                    info.Count = int.Parse(row[2].ToString());

                    lstAlarmContentTopModel.Add(info);
                }
            }
            return lstAlarmContentTopModel;
        }

        /// <summary>
        /// 统计前10大报警总次数的报警规则2
        /// </summary>
        /// <param name="alarmDateStart"></param>
        /// <param name="alarmDateEnd"></param>
        /// <returns></returns>
        public IList<AlarmContentTopModel> GetAlarmContentTops2(DateTime? alarmDateStart, DateTime? alarmDateEnd)
        {
            string connectionString_ATL = @"server=127.0.0.1;database=IFactory;uid=root;pwd=root;Persist Security Info=True;Charset=utf8;";
            MySqlConnection connatl = new MySqlConnection(connectionString_ATL);
            connatl.Open();
            string sql = @"select r.rule_did, r.alarm_content, count(*) 
                            from alarm_record t
                            join alarm_rule r on r.rule_did = t.rule_did
                            where r.craft_did = 12 ";
            string w1 = alarmDateStart.HasValue ? "and t.alarm_time >= '" + alarmDateStart.ToString() + "'" : "";
            string w2 = alarmDateEnd.HasValue ? " and t.alarm_time < '" + alarmDateEnd.Value.AddDays(1.0).ToString() + "'" : "";
            string w3 = "group by r.rule_did order by count(*) desc limit 10";
            MySqlDataAdapter b = new MySqlDataAdapter(sql + w1 + w2 + w3, connatl);
            DataTable bt = new DataTable();
            b.Fill(bt);
            connatl.Close();

            List<AlarmContentTopModel> lstAlarmContentTopModel = new List<AlarmContentTopModel>();

            if (bt != null && bt.Rows.Count > 0)
            {
                foreach (DataRow row in bt.Rows)
                {
                    AlarmContentTopModel info = new AlarmContentTopModel();
                    info.RuleDID = row[0].ToString();
                    info.Content = row[1].ToString();
                    info.Count = int.Parse(row[2].ToString());

                    lstAlarmContentTopModel.Add(info);
                }
            }
            return lstAlarmContentTopModel;
        }

        /// <summary>
        /// 统计前10大报警总次数的工艺
        /// </summary>
        /// <param name="alarmDateStart"></param>
        /// <param name="alarmDateEnd"></param>
        /// <returns></returns>
        public IList<AlarmCraftTopModel> GetAlarmCraftTops(DateTime? alarmDateStart, DateTime? alarmDateEnd)
        {
            string connectionString_ATL = @"server=127.0.0.1;database=IFactory;uid=root;pwd=root;Persist Security Info=True;Charset=utf8;";
            MySqlConnection connatl = new MySqlConnection(connectionString_ATL);
            connatl.Open();
            string sql = @"select c.craft_did, c.Resource, t.alarm_time, count(*) 
                            from alarm_record t
                            join alarm_rule r on r.rule_did = t.rule_did
                            join craft_probably c on c.craft_did = r.craft_did
                            where c.craft_did = 4 ";
            string w1 = alarmDateStart.HasValue ? "and t.alarm_time >= '" + alarmDateStart.ToString() + "'" : "";
            string w2 = alarmDateEnd.HasValue ? " and t.alarm_time < '" + alarmDateEnd.Value.AddDays(1.0).ToString() + "'" : "";
            string w3 = "group by c.craft_did order by count(*) desc limit 10";
            MySqlDataAdapter b = new MySqlDataAdapter(sql + w1 + w2 + w3, connatl);
            DataTable bt = new DataTable();
            b.Fill(bt);
            connatl.Close();

            List<AlarmCraftTopModel> lstAlarmCraftTopModel = new List<AlarmCraftTopModel>();
            
            if (bt != null && bt.Rows.Count > 0)
            {
                foreach (DataRow row in bt.Rows)
                {
                    AlarmCraftTopModel info = new AlarmCraftTopModel();
                    info.CraftDID = int.Parse(row[0].ToString());
                    info.CraftName = row[1].ToString();
                    info.AlarmTime = DateTime.Parse(row[2].ToString());
                    info.Count = int.Parse(row[3].ToString());

                    lstAlarmCraftTopModel.Add(info);
                }
            }
            
            return lstAlarmCraftTopModel;
        }

        /// <summary>
        /// 统计前10大报警总次数的工艺2
        /// </summary>
        /// <param name="alarmDateStart"></param>
        /// <param name="alarmDateEnd"></param>
        /// <returns></returns>
        public IList<AlarmCraftTopModel> GetAlarmCraftTops2(DateTime? alarmDateStart, DateTime? alarmDateEnd)
        {
            string connectionString_ATL = @"server=127.0.0.1;database=IFactory;uid=root;pwd=root;Persist Security Info=True;Charset=utf8;";
            MySqlConnection connatl = new MySqlConnection(connectionString_ATL);
            connatl.Open();
            string sql = @"select c.craft_did, c.Resource, t.alarm_time, count(*) 
                            from alarm_record t
                            join alarm_rule r on r.rule_did = t.rule_did
                            join craft_probably c on c.craft_did = r.craft_did
                            where c.craft_did = 12 ";
            string w1 = alarmDateStart.HasValue ? "and t.alarm_time >= '" + alarmDateStart.ToString() + "'" : "";
            string w2 = alarmDateEnd.HasValue ? " and t.alarm_time < '" + alarmDateEnd.Value.AddDays(1.0).ToString() + "'" : "";
            string w3 = "group by c.craft_did order by count(*) desc limit 10";
            MySqlDataAdapter b = new MySqlDataAdapter(sql + w1 + w2 + w3, connatl);
            DataTable bt = new DataTable();
            b.Fill(bt);
            connatl.Close();

            List<AlarmCraftTopModel> lstAlarmCraftTopModel = new List<AlarmCraftTopModel>();

            if (bt != null && bt.Rows.Count > 0)
            {
                foreach (DataRow row in bt.Rows)
                {
                    AlarmCraftTopModel info = new AlarmCraftTopModel();
                    info.CraftDID = int.Parse(row[0].ToString());
                    info.CraftName = row[1].ToString();
                    info.AlarmTime = DateTime.Parse(row[2].ToString());
                    info.Count = int.Parse(row[3].ToString());

                    lstAlarmCraftTopModel.Add(info);
                }
            }

            return lstAlarmCraftTopModel;
        }

        public string w2 { get; set; }
        public string w3 { get; set; }
        /// <summary>
        /// 获取历史报警记录
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="alarmDateStart"></param>
        /// <param name="alarmDateEnd"></param>
        /// <param name="pageNo"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public IPagedList<AlarmRecordItem> GetPagedAlarmRecords(string keyword, DateTime? alarmDateStart, DateTime? alarmDateEnd, int pageNo, int pageSize, int CraftDID)
        {
            string connectionString_ATL = @"server=127.0.0.1;database=IFactory;uid=root;pwd=root;Persist Security Info=True;Charset=utf8;";
            MySqlConnection connatl = new MySqlConnection(connectionString_ATL);
            connatl.Open();

            string sql = string.Format(@"select
                              temp.报警时间
                            , temp.报警内容
                            , temp.dispose_time
                            , temp.craft_name
                            , temp.duration
                            , temp.alarm_record_did
                            from alarm_table temp");
            string w1 = " where temp.报警内容 like '%" + keyword +"%'";
            string w2 = alarmDateStart.HasValue ? " and temp.报警时间 >= '" + alarmDateStart.Value.ToString() + "' " : "";
            string w3 = alarmDateStart.HasValue ? " and temp.报警时间 < '" + alarmDateStart.Value.AddDays(1.0).ToString() + "' " : "";
            string w4 = " order by temp.报警时间 DESC";
            MySqlDataAdapter b = new MySqlDataAdapter(sql+w1+w2+w3+w4, connatl);
            DataTable bt = new DataTable();
            connatl.Close();
            b.Fill(bt);

            List<AlarmRecordItem> lstAlarmRecord = new List<AlarmRecordItem>();
            if (bt != null && bt.Rows.Count > 0)
            {
                foreach(DataRow row in bt.Rows)
                {
                    AlarmRecordItem item = new AlarmRecordItem();
                    item.RuleDID = "701";
                    item.AlarmTime = DateTime.Parse(row[0].ToString());
                    item.AlarmContent = row[1].ToString();
                    item.DisposeTime = DateTime.Parse(row[2].ToString());
                    item.CraftName = row[3].ToString();
                    item.Duration = int.Parse(row[4].ToString());
                    item.AlarmDid = int.Parse(row[5].ToString());
                    lstAlarmRecord.Add(item);
                }
            }

            IQueryable<AlarmRecordItem> superset = lstAlarmRecord.AsQueryable();
            return new PagedList<AlarmRecordItem>(superset, pageNo, pageSize);
        }

        public AlarmTemporaryModel GetAlarmTemporaryModel(int did)
        {
            Database equipDB = dataProvider.EQUIPDataBase;
            string sql  = string.Format(
                    @"select temp.alarm_temporary_did
						    , temp.rule_did
						    , cr.craft_did
						    , p.process_did
						    , f.facility_did
						    , u.unit_did
                            , temp.alarm_time
						    , temp.dispose_state
						    , temp.dispose_time
						    , temp.handler
						    , temp.duration
						    , temp.address
						    , temp.remark
	                        , rule.alarm_content
	                        , rule.alarm_reason
	                        , sl.content
	                        , slimage.path
	                        , image.path
	                        , cr.craft_name
	                        , u.unit_name
	                        , p.process_name
	                        , f.MMName
	                        , almtp.type
                    from alarm_temporary temp 
                    join facility_info f on f.facility_did = temp.facility_did
                    join alarm_rule rule on rule.rule_did = temp.rule_did
                    join craft cr on cr.craft_did = rule.craft_did
                    join unit u on u.unit_did = rule.unit_did
                    join alarm_type almtp on almtp.did = rule.alarm_type_did
                    join process p on p.process_did = f.process_did
					left join alarm_location_image image on image.did = rule.alarm_location_image_did
					left join solution sl on sl.did = rule.solution_did
					left join solution_image slimage on slimage.did = rule.solution_image_did
					where temp.alarm_temporary_did = {0};"
                                        , did);

            DataSet ds = equipDB.ExecuteDataSet(CommandType.Text, sql);
            DataTable tb = ds.Tables[0];
            if (tb != null && tb.Rows.Count > 0)
            {
                AlarmTemporaryModel model = new AlarmTemporaryModel();

            model.DID = int.Parse(tb.Rows[0][0].ToString());
            model.RuleDID =  tb.Rows[0][1].ToString();
            model.CraftDID =  int.Parse(tb.Rows[0][2].ToString());
            model.ProcessDID =  int.Parse(tb.Rows[0][3].ToString());
            model.FacilityDID =  int.Parse(tb.Rows[0][4].ToString());
            model.UnitDID =  int.Parse(tb.Rows[0][5].ToString());
            model.AlarmTime = DateTime.Parse(tb.Rows[0][6].ToString());
            if (tb.Rows[0][7].ToString() != "")
            model.DisposeState = 0; int.Parse(tb.Rows[0][7].ToString());
                                        if (tb.Rows[0][8].ToString() != "")
            model.DisposeTime = DateTime.Parse(tb.Rows[0][8].ToString());
            model.Handler =  tb.Rows[0][9].ToString();
                                if (tb.Rows[0][10].ToString() != "")
            model.Duration =  int.Parse(tb.Rows[0][10].ToString());
            model.Address =  tb.Rows[0][11].ToString();
            model.Remark = tb.Rows[0][12].ToString();
            model.AlarmContent =tb.Rows[0][13].ToString();
            model.AlarmReason = tb.Rows[0][14].ToString();
            model.SolutionText = tb.Rows[0][15].ToString();
            model.SolutionImagePath = tb.Rows[0][16].ToString();
            model.AlarmLocationImagePath = tb.Rows[0][17].ToString();
            model.CraftName = tb.Rows[0][18].ToString();
            model.UnitName = tb.Rows[0][19].ToString();
            model.ProcessName = tb.Rows[0][20].ToString();
            model.FacilityName = tb.Rows[0][21].ToString();
            model.AlarmTypeName = tb.Rows[0][22].ToString();

            model.FieldValues = this.GetAlarmRuleFieldValues(model.RuleDID);
                return model;
            }
            else
            {
                throw new Exception("执行GetAlarmTemporaryModel未查询到数据");
            }
        }

        public DataTable getAlarmTemporaryByID(int alarmTemporaryDID)
        {
            string connectionString_ATL = @"server=127.0.0.1;database=IFactory;uid=root;pwd=root;Persist Security Info=True;Charset=utf8;";
            //string connectionString_ATL = @"server=127.0.0.1;database=IFactory;uid=root;pwd=123456;Persist Security Info=True;Charset=utf8;";
            MySqlConnection connatl = new MySqlConnection(connectionString_ATL);
            connatl.Open();
            string sql = "select * from alarm_temporary where alarm_temporary_did = " + alarmTemporaryDID.ToString() + ";";
            MySqlDataAdapter b = new MySqlDataAdapter(sql, connatl);
            DataTable bt = new DataTable();
            b.Fill(bt);
            connatl.Close();
            return bt;
        }

        public AlarmTemporaryInfo GetAlarmTemporaryByID(int alarmTemporaryDID)
        {
            DataTable tb = getAlarmTemporaryByID(alarmTemporaryDID);
            
            if (tb != null && tb.Rows.Count > 0)
            {
                AlarmTemporaryInfo info = new AlarmTemporaryInfo();
                info.AlarmTemporaryDID = int.Parse(tb.Rows[0][0].ToString());
                info.RuleDID = tb.Rows[0][1].ToString();
                info.FacilityDID = int.Parse(tb.Rows[0][2].ToString());
                info.AlarmTime = DateTime.Parse(tb.Rows[0][3].ToString());
                if (tb.Rows[0][4].ToString() != "")
                    info.DisposeState = int.Parse(tb.Rows[0][4].ToString());
                if (tb.Rows[0][5].ToString() != "")
                    info.DisposeTime = DateTime.Parse(tb.Rows[0][5].ToString());
                info.Handler = tb.Rows[0][6].ToString();
                if (tb.Rows[0][7].ToString() != "")
                    info.Duration = int.Parse(tb.Rows[0][7].ToString());
                info.Address = tb.Rows[0][8].ToString();
                info.Remark = tb.Rows[0][9].ToString();

                ProductionDB p = new ProductionDB();
                FacilityInfo f = p.GetFacilityByID(info.FacilityDID);
                info.Facility = f;

                return info;
            }
            else
            {
                throw new Exception("执行 getAlarmTemporaryByID 查询到空值");
            }
        }


        /// <summary>
        /// 用户操作完后，将当前报警信息清除，并将当前用户操作的报警信息增加到历史报警记录里面
        /// </summary>
        /// <param name="alarmTemporaryDID"></param>
        /// <param name="handlerId"></param>
        public void UpdateAlarmTemporaryHandled(int alarmTemporaryDID, int handlerId)
        {

            AlarmTemporaryInfo entity = GetAlarmTemporaryByID(alarmTemporaryDID);

            UserDB u = new UserDB();
            UserInfo user = u.GetUserByUserID(handlerId);

            AlarmRecordInfo info = new AlarmRecordInfo();
            info.Address = entity.Address;
            info.AlarmCount = 0;
            info.AlarmTime = entity.AlarmTime;
            info.DisposeState = 2;
            info.DisposeTime = DateTime.Now;
            info.Duration = (int)DateTime.Now.Subtract(entity.AlarmTime).TotalMinutes;
            info.FacilityDID = entity.FacilityDID;
            info.Handler = user.Name;
            info.Remark = entity.Remark;
            info.RuleDID = entity.RuleDID;

            insertAlarmRecordInfo(info);
            removeFromAlarmTemporaryByID(alarmTemporaryDID);
        }

        private void insertAlarmRecordInfo(AlarmRecordInfo info)
        {
            string sql = string.Format(@"insert alarm_record (
	                                        rule_did,
	                                        facility_did,
	                                        alarm_time,
	                                        alarm_count,
	                                        dispose_state,
	                                        dispose_time,
	                                        handler,
	                                        duration,
	                                        address,
	                                        remark)
                                        values(
                                        '{0}', {1}, '{2}', {3}, {4}, '{5}', '{6}', {7}, '{8}', '{9}');"
           , info.RuleDID, info.FacilityDID, info.AlarmTime, info.AlarmCount, info.DisposeState, info.DisposeTime
           , info.Handler, info.Duration, info.Address , info.Remark);

            Database equipDB = dataProvider.EQUIPDataBase;
            equipDB.ExecuteNonQuery(CommandType.Text, sql);
        }

        private void removeFromAlarmTemporaryByID(int alarmTemporaryDID)
        {
            string sql = string.Format("delete from alarm_temporary where alarm_temporary_did = {0}", alarmTemporaryDID);
            Database equipDB = dataProvider.EQUIPDataBase;
            equipDB.ExecuteNonQuery(CommandType.Text, sql);
        }
    }


    
}
