using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IFactory.Domain.Entities;
using IFactory.Domain.Common;
using System.Data.Common;
using IFactory.Domain.Models;
using MySql.Data.MySqlClient;
using PagedList;

namespace IFactory.DB
{
    public class ProductionDB : BaseFacade
    {
        public static bool logCell = true;
        
        public static string Operator { get; set; }

        public DataTable UserTableQuery()
        {
            Database equipDB = dataProvider.EQUIPDataBase;
            string sql = "select * from users;";
            DataSet ds = equipDB.ExecuteDataSet(CommandType.Text, sql);
            return ds.Tables[0];
        }

        
        public int GetProductionState()
        {
            Database equipDB = dataProvider.EQUIPDataBase;
            string sql = "select max(state) as state from (select 3 as state from alarm_temporary union all select max(fi.state) as state from facility_info fi) as t;";
            object obj = equipDB.ExecuteScalar(CommandType.Text, sql);
            if (obj != null && obj != DBNull.Value)
                return Convert.ToInt32(obj);
            return 2;
        }

        
        public int GetCraftState(int craftDID)
        {
            Database equipDB = dataProvider.EQUIPDataBase;
            string sql = string.Format(
                @"select max(state) as state from 
                    (select 3 as state from alarm_temporary 
                        where facility_did={0} 
                    union all select max(fi.state) as state from facility_info fi 
                    join process p on fi.process_did=p.process_did 
                        where p.craft_did={1} ) as t;"
                                        , craftDID, craftDID);

            object obj = equipDB.ExecuteScalar(CommandType.Text, sql);
            if (obj != null && obj != DBNull.Value)
                return Convert.ToInt32(obj);
            return 2;
        }
        

        public DataTable getCrafts()
        {
            Database equipDB = dataProvider.EQUIPDataBase;
            string sql = "select * from craft;";
            DataSet ds = equipDB.ExecuteDataSet(CommandType.Text, sql);
            return ds.Tables[0];
        }

        private DataTable getCraftsByID(int craftDID)
        {
            Database equipDB = dataProvider.EQUIPDataBase;
            string sql = "select * from craft where craft_did = " + craftDID.ToString();
            DataSet ds = equipDB.ExecuteDataSet(CommandType.Text, sql);
            return ds.Tables[0];
        }

        public CraftInfo GetCraftsByID(int craftDID)
        {
            DataTable tb = getCraftsByID(craftDID);
            if (tb != null && tb.Rows.Count > 0)
            {
                CraftInfo info = new CraftInfo();
                info.CraftDID = int.Parse(tb.Rows[0][0].ToString());
                info.CraftNO = tb.Rows[0][1].ToString();
                info.CraftName = tb.Rows[0][2].ToString();
                return info;
            }
            else
            {
                throw new Exception("执行 getCraftsByID 查询到空值");
            }
        }

        public IList<CraftInfo> GetCrafts()
        {
            DataTable tb = getCrafts();
            
            List<CraftInfo> lst = new List<CraftInfo>();
            if (tb != null && tb.Rows.Count > 0)
            {
                foreach(DataRow row in tb.Rows)
                {
                    CraftInfo info = new CraftInfo();
                    info.CraftDID = int.Parse(row[0].ToString());
                    info.CraftNO = row[1].ToString();
                    info.CraftName = row[2].ToString();
                    lst.Add(info);
                }
            }
            return lst;
        }

        public Dictionary<int, int> GetCraftStates()
        {
            string connectionString_ATL = @"server=127.0.0.1;database=IFactory;uid=root;pwd=root;Persist Security Info=True;Charset=utf8;";
            MySqlConnection connatl = new MySqlConnection(connectionString_ATL);
            connatl.Open();
            string sql = @"select craft_did as 'Key',max(state) as 'Value' 
                            from(
                            select facility_did as craft_did,3 as state from alarm_temporary group by facility_did
                            union all 
                            select p.craft_did,max(fi.state) as state from facility_info fi 
                            join process p on fi.process_did=p.process_did group by p.craft_did
                            ) as t group by craft_did;";
            MySqlDataAdapter b = new MySqlDataAdapter(sql, connatl);
            DataTable bt = new DataTable();
            b.Fill(bt);
            connatl.Close();

            Dictionary<int, int> dic = new Dictionary<int, int>();
            if(bt != null  && bt.Rows.Count > 0)
            {
                foreach(DataRow row in bt.Rows)
                {
                    dic.Add(int.Parse(row[0].ToString()), int.Parse(row[1].ToString()));
                }
            }
            return dic;
        }

        private DataTable getProductionLineProbably(int did)
        {
            Database equipDB = dataProvider.EQUIPDataBase;
            string sql = string.Format(
                @"select * from production_line_probably t where ( t.did = {0});"
                                        , did);
            DataSet ds = equipDB.ExecuteDataSet(CommandType.Text, sql);
            return ds.Tables[0];
        }

        public ProductionLineProbablyInfo GetProductionLineProbably(int did)
        {
            DataTable tb = getProductionLineProbably(did);
            
            if (tb != null && tb.Rows.Count > 0)
            {
                int i = (tb.Rows.Count - 1); 
                ProductionLineProbablyInfo info = new ProductionLineProbablyInfo();
                info.DID = int.Parse(tb.Rows[i][0].ToString());
                info.Name = tb.Rows[i][1].ToString();
                info.ProductionType = tb.Rows[0][2].ToString();
                info.NowYield = tb.Rows[i][3].ToString();
                info.TargetYield = tb.Rows[i][4].ToString();
                info.UserName = tb.Rows[i][5].ToString();
                return info;
            }
            else
            {
                throw new Exception("执行 getProductionLineProbably 查询到空值");
            }
            
        }

        public void SaveProductionLineProbably(ProductionLineProbablyInfo productionLineProbablyInfo)
        {
            Database equipDB = dataProvider.EQUIPDataBase;
            string sql = string.Format(
                @"update production_line_probably t 
                    set
			        t.name = '{1}',
			        t.production_type = '{2}',
			        t.nowYield = {3},
                    t.targetYield = {4},
                    t.userName = '{5}'
                    where t.did = {0}"
              , productionLineProbablyInfo.DID, productionLineProbablyInfo.Name, productionLineProbablyInfo.ProductionType
              , int.Parse(productionLineProbablyInfo.NowYield), int.Parse(productionLineProbablyInfo.TargetYield), productionLineProbablyInfo.UserName);
            equipDB.ExecuteScalar(CommandType.Text, sql);
        }

        private DataTable getProcesses(int craftDID)
        {
            string connectionString_ATL = @"server=127.0.0.1;database=IFactory;uid=root;pwd=root;Persist Security Info=True;Charset=utf8;";
            MySqlConnection connatl = new MySqlConnection(connectionString_ATL);
            connatl.Open();
            string sql = string.Format(
                @"select * from process t where ( t.craft_did = {0});"
                                        , craftDID);
            MySqlDataAdapter b = new MySqlDataAdapter(sql, connatl);
            DataTable bt = new DataTable();
            b.Fill(bt);
            connatl.Close();
            return bt;
        }

        public IList<ProcessInfo> GetProcesses(int craftDID)
        {
            DataTable tbProcesses = getProcesses(craftDID);
            
            List <ProcessInfo> lst = new List<ProcessInfo>();
            
            if(tbProcesses != null && tbProcesses.Rows.Count > 0)
            {
                DataTable tbCrafts = getCraftsByID(craftDID);
                if (tbCrafts != null && tbCrafts.Rows.Count > 0)
                {
                    CraftInfo craftInfo = new CraftInfo();
                    craftInfo.CraftDID = craftDID;
                    craftInfo.CraftNO = tbCrafts.Rows[0][1].ToString();
                    craftInfo.CraftName = tbCrafts.Rows[0][2].ToString();

                    foreach (DataRow row in tbProcesses.Rows)
                    {
                        ProcessInfo processInfo = new ProcessInfo();
                        processInfo.ProcessDID = int.Parse(row[0].ToString());
                        processInfo.ProcessNO = row[1].ToString();
                        processInfo.ProcessName = row[2].ToString();
                        processInfo.CraftDID = int.Parse(row[3].ToString());
                        processInfo.Craft = craftInfo;
                        lst.Add(processInfo);
                    }
                }
                else
                {
                    throw new Exception("执行getCraftsByID查询到空值");
                }
            }
            
            return lst;
        }

        public Dictionary<int, int> GetProcessStates(int craftDID)
        {
            string connectionString_ATL = @"server=127.0.0.1;database=IFactory;uid=root;pwd=root;Persist Security Info=True;Charset=utf8;";
            MySqlConnection connatl = new MySqlConnection(connectionString_ATL);
            connatl.Open();
            Dictionary<int, int> dic = new Dictionary<int, int>();
            string sql = "";

            if (craftDID == 4)
            {
                sql = string.Format(
                @"select process_did as 'Key',max(state) as 'Value'  from (select fi.process_did,max(fi.state) as state from facility_info fi 
                group by fi.process_did) as t group by process_did;");
            }
            else
            {
                sql = string.Format(
                @"select process_did as 'Key',max(state) as 'Value'  from (select fi.process_did,max(fi.state) as state from facility_info fi 
                group by fi.process_did) as t group by process_did;");
            }

            MySqlDataAdapter b = new MySqlDataAdapter(sql, connatl);
            DataTable bt = new DataTable();
            b.Fill(bt);
            connatl.Close();

            if (bt != null && bt.Rows.Count > 0)
            {
                foreach (DataRow row in bt.Rows)
                {
                    dic.Add(int.Parse(row[0].ToString()), int.Parse(row[1].ToString()));
                }
            }
            return dic;
        }

        private DataTable getCraftProbably(int craftDID)
        {
            string connectionString_ATL = @"server=127.0.0.1;database=IFactory;uid=root;pwd=root;Persist Security Info=True;Charset=utf8;";
            MySqlConnection connatl = new MySqlConnection(connectionString_ATL);
            connatl.Open();
            string sql = "";
            DateTime today = DateTime.Now.AddMinutes(-5);
            if (craftDID == 4)
            {
                sql = string.Format(
                @"select * from(
                    select
                      c.Operator
                     ,c.nowYield
                     ,c.targetYield
                     ,c.Resource
                     ,c.PPM
                     ,c.OKCount
                     ,auto.batteryBarCode
                     ,auto.end_product_no
                 from craft_probably c 
                 join autoinspection1_facility_production_data auto
                 on auto.StartDate > '" + today + "'" + " order by c.time desc limit 1)t;");
            }
            else
            {
                sql = string.Format(
                @"select * from(
                    select
                      c.Operator
                     ,c.nowYield
                     ,c.targetYield
                     ,c.Resource
                     ,c.PPM
                     ,c.OKCount
                     ,auto.batteryBarCode
                     ,auto.end_product_no
                 from craft_probably c 
                 join autoinspection2_facility_production_data auto
                 on auto.StartDate > '" + today + "'" +" order by c.time desc limit 1)t;");
            }
            MySqlDataAdapter b = new MySqlDataAdapter(sql , connatl);
            DataTable bt = new DataTable();
            b.Fill(bt);
            connatl.Close();
            return bt;
        }

        //------------------最强分界线---
        public CraftProbablyInfo GetCraftProbably(int craftDID,string code)
        {
            DataTable tbCraftProbablyInfo = getCraftProbably(craftDID);
            
            if(tbCraftProbablyInfo != null && tbCraftProbablyInfo.Rows.Count > 0)
            {
                int i = tbCraftProbablyInfo.Rows.Count - 1;
                CraftProbablyInfo info = new CraftProbablyInfo();
                info.CraftDID = craftDID; 
                info.UseName = tbCraftProbablyInfo.Rows[i][0].ToString();
                info.NowYield = tbCraftProbablyInfo.Rows[i][1].ToString();
                info.TargetYield = tbCraftProbablyInfo.Rows[i][2].ToString();
                info.DeviceName = tbCraftProbablyInfo.Rows[i][3].ToString();
                info.PPM = double.Parse(tbCraftProbablyInfo.Rows[i][4].ToString());
                info.OKCount = int.Parse(tbCraftProbablyInfo.Rows[i][5].ToString());
                info.BatteryBarCode = tbCraftProbablyInfo.Rows[i][6].ToString();
                if(int.Parse(tbCraftProbablyInfo.Rows[i][1].ToString()) != 0)
                {
                    info.OKRate = (float.Parse(tbCraftProbablyInfo.Rows[i][5].ToString())
                    / float.Parse(tbCraftProbablyInfo.Rows[i][1].ToString()) * 100).ToString() + "%";
                }
                else
                {
                    info.OKRate = "0%";
                }
                if (tbCraftProbablyInfo.Rows[i][7].ToString() == "null")
                {
                    info.code = "null";
                }
                else
                {
                    info.code = tbCraftProbablyInfo.Rows[i][7].ToString(); ;
                }
                return info;
            }
            else
            {
                //throw new Exception("执行getCraftProbably查询到空值");
                CraftProbablyInfo info = new CraftProbablyInfo();
                info.CraftDID = craftDID;
                info.NowYield = "null";
                info.TargetYield = "null";
                info.UseName = "null";
                info.CarNO = "null";
                info.BatteryBarCode = "null";
                info.DeviceName = "null";
                info.PPM = 0;
                info.OKCount = 0;
                info.OKRate = "null";
                info.code = "null";

                return info;
            }
            
        }

        private DataTable getCraftProbablys()
        {
            string connectionString_ATL = @"server=127.0.0.1;database=IFactory;uid=root;pwd=root;Persist Security Info=True;Charset=utf8;";
            MySqlConnection connatl = new MySqlConnection(connectionString_ATL);
            connatl.Open();
            string sql = "select * from craft_probably;";
            MySqlDataAdapter b = new MySqlDataAdapter(sql, connatl);
            DataTable bt = new DataTable();
            b.Fill(bt);
            connatl.Close();
            return bt;
        }

        public List<CraftProbablyInfo> GetCraftProbablys()
        {
            DataTable tbCraftProbablyInfo = getCraftProbablys();
            List<CraftProbablyInfo> lst = new List<CraftProbablyInfo>();
            if (tbCraftProbablyInfo != null && tbCraftProbablyInfo.Rows.Count > 0)
            {
                    CraftProbablyInfo info = new CraftProbablyInfo();
                    info.CraftDID = int.Parse(tbCraftProbablyInfo.Rows[0][0].ToString());
                    info.BatteryBarCode = tbCraftProbablyInfo.Rows[0][1].ToString();
                    info.NowYield = tbCraftProbablyInfo.Rows[0][2].ToString();
                    info.TargetYield = tbCraftProbablyInfo.Rows[0][3].ToString();
                    info.UseName = tbCraftProbablyInfo.Rows[0][4].ToString();
                    info.CarNO = tbCraftProbablyInfo.Rows[0][5].ToString();

                    lst.Add(info);
            }

            return lst;
        }

        private DataTable getAlarmCheck()
        {
            string connectionString_ATL = @"server=127.0.0.1;database=IFactory;uid=root;pwd=root;Persist Security Info=True;Charset=utf8;";
            MySqlConnection connatl = new MySqlConnection(connectionString_ATL);
            connatl.Open();
            string sql = string.Format(
                @"select
	            rule_did,
                facility_did,
                alarm_record_did
                from alarm_record
                where alarm_record_did = (select max(alarm_record_did) from alarm_record);");
            MySqlDataAdapter b = new MySqlDataAdapter(sql, connatl);
            DataTable bt = new DataTable();
            b.Fill(bt);
            connatl.Close();
            return bt;
        }

        public IList<AlarmCheckInfo> GetAlarmCheck()
        {
            DataTable tb = getAlarmCheck();

            List<AlarmCheckInfo> lst = new List<AlarmCheckInfo>();
            if (tb != null && tb.Rows.Count > 0)
            {
                foreach (DataRow row in tb.Rows)
                {
                    AlarmCheckInfo info = new AlarmCheckInfo();
                    info.AlarmCheck = row[0].ToString();
                    info.FacilityDid = row[1].ToString();
                    info.AlarmDid = int.Parse(row[2].ToString());

                    lst.Add(info);
                }
            }
            return lst;
        }

        private DataTable getPLCStates(int craftDID)
        {
            string connectionString_ATL = @"server=127.0.0.1;database=IFactory;uid=root;pwd=root;Persist Security Info=True;Charset=utf8;";
            MySqlConnection connatl = new MySqlConnection(connectionString_ATL);
            connatl.Open();
            string sql = string.Format(
                @"select * from plc_state t where ( t.craft_did = {0});"
                                        , craftDID);
            MySqlDataAdapter b = new MySqlDataAdapter(sql, connatl);
            DataTable bt = new DataTable();
            b.Fill(bt);
            connatl.Close();
            return bt;
        }

        public IList<PLCStateInfo> GetPLCStates(int craftDID)
        {
            DataTable tb = getPLCStates(craftDID);
            
            List<PLCStateInfo> lst = new List<PLCStateInfo>();
            if (tb != null && tb.Rows.Count > 0)
            {
                foreach (DataRow row in tb.Rows)
                {
                    PLCStateInfo info = new PLCStateInfo();
                    info.PLCDID = int.Parse(row[0].ToString());
                    info.PLCName = row[1].ToString();
                    info.State = int.Parse(row[2].ToString());
                    info.CraftDID = craftDID;
                    lst.Add(info);
                }
            }
            return lst;
        }

        private DataTable getProcessesByArray(int[] craftDIDs)
        {
            string array = "";
            if (craftDIDs.Length == 1)
                array = craftDIDs[0].ToString();
            else if (craftDIDs.Length > 1)
            {
                array = craftDIDs[0].ToString();
                for (int i = 1; i < craftDIDs.Length; i++)
                {
                    array += ",";
                    array += craftDIDs[i].ToString();
                }
            }

            string connectionString_ATL = @"server=127.0.0.1;database=IFactory;uid=root;pwd=root;Persist Security Info=True;Charset=utf8;";
            MySqlConnection connatl = new MySqlConnection(connectionString_ATL);
            connatl.Open();
            string sql = string.Format(
                @"select * from process t where ( t.craft_did in ( {0} ));"
                                        , array);
            MySqlDataAdapter b = new MySqlDataAdapter(sql, connatl);
            DataTable bt = new DataTable();
            b.Fill(bt);
            connatl.Close();
            return bt;
        }

        private DataTable getProcessesByID(int processDID)
        {
            string connectionString_ATL = @"server=127.0.0.1;database=IFactory;uid=root;pwd=root;Persist Security Info=True;Charset=utf8;";
            MySqlConnection connatl = new MySqlConnection(connectionString_ATL);
            connatl.Open();
            string sql = string.Format(
                @"select * from process where process_did = {0} ;"
                                        , processDID);
            MySqlDataAdapter b = new MySqlDataAdapter(sql, connatl);
            DataTable bt = new DataTable();
            b.Fill(bt);
            connatl.Close();
            return bt;
        }

        private DataTable getDeviceGroupByID(int DeviceGroupDID)
        {
            Database equipDB = dataProvider.EQUIPDataBase;
            string sql = string.Format(
                @"select * from device_group t where ( t.device_group_did = {0} );"
                                        , DeviceGroupDID);
            DataSet ds = equipDB.ExecuteDataSet(CommandType.Text, sql);
            return ds.Tables[0];
        }

        public IList<ProcessInfo> GetProcesses(int[] processDIDs)
        {
            List<ProcessInfo> lst = new List<ProcessInfo>();
            DataTable dt = getProcessesByArray(processDIDs);
            if(dt != null && dt.Rows.Count > 0)
            {
                foreach(DataRow row in dt.Rows)
                {
                    ProcessInfo info = new ProcessInfo();
                    info.ProcessDID = int.Parse(row[0].ToString());
                    info.ProcessNO = row[1].ToString();
                    info.ProcessName = row[2].ToString();
                    info.CraftDID = int.Parse(row[3].ToString());

                    DataTable tbCrafts = getCraftsByID(info.CraftDID);
                    if (tbCrafts != null && tbCrafts.Rows.Count > 0)
                    {
                        CraftInfo craftInfo = new CraftInfo();
                        craftInfo.CraftDID = info.CraftDID;
                        craftInfo.CraftNO = tbCrafts.Rows[0][1].ToString();
                        craftInfo.CraftName = tbCrafts.Rows[0][2].ToString();
                        info.Craft = craftInfo;
                    }
                    else
                    {
                        info.Craft = null;
                        throw new Exception("执行getCraftsByID查询为空");
                    }
                }
            }
            else
            {
                throw new Exception("执行getProcessesByArray查询为空");
            }
            return lst;
        }

        private DataTable getFacilities(int[] facilityDIDs)
        {
            string array = "";
            if (facilityDIDs.Length == 1)
                array = facilityDIDs[0].ToString();
            else if (facilityDIDs.Length > 1)
            {
                array = facilityDIDs[0].ToString();
                for (int i = 1; i < facilityDIDs.Length; i++)
                {
                    array += ",";
                    array += facilityDIDs[i].ToString();
                }
            }

            string connectionString_ATL = @"server=127.0.0.1;database=IFactory;uid=root;pwd=root;Persist Security Info=True;Charset=utf8;";
            MySqlConnection connatl = new MySqlConnection(connectionString_ATL);
            connatl.Open();
            string sql = string.Format(
                @"select * from facility_info t where ( t.facility_did in ( {0} ));"
                                        , array);
            MySqlDataAdapter b = new MySqlDataAdapter(sql, connatl);
            DataTable bt = new DataTable();
            b.Fill(bt);
            connatl.Close();
            return bt;
        }
        
        public FacilityInfo GetFacilityByID(int facilityDID)
        {
            if (GetFacilities(new int[] { facilityDID }).ToList().Count == 0)
                throw new Exception("执行 GetFacilityByID 查询到空值");
            else
                return GetFacilities(new int[] { facilityDID }).ToList().First();
        }

        /// <summary>
        /// GetFacilities by facility_did
        /// </summary>
        /// <param name="facilityDIDs">facility_did</param>
        /// <returns></returns>
        public IList<FacilityInfo> GetFacilities(int[] facilityDIDs)
        {
            List<FacilityInfo> lst = new List<FacilityInfo>();
            DataTable dt = getFacilities(facilityDIDs);
            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    FacilityInfo info = new FacilityInfo();
                    info.FacilityDID = int.Parse(row[0].ToString());
                    info.MMName = row[1].ToString();
                    info.ProcessDID = int.Parse(row[2].ToString());
                    info.DeviceGroupDID = int.Parse(row[3].ToString());
                    info.State = int.Parse(row[4].ToString());
                    info.MMIP = row[5].ToString();
                    //info.MMPort = row[6].ToString();
                    //info.MMIsUse = row[7].ToString() == "1" ? true: false;
                    //info.MMClearAddr = row[8].ToString();
                    //info.MMRestAddr = row[9].ToString();
                    //if (row[10].ToString() == "")
                        //info.MMSeq = null;
                    //else
                        //info.MMSeq = int.Parse(row[10].ToString());
                    //.MAAddress = row[11].ToString();
                    //info.IsUse = row[12].ToString() == "1" ? true : false;
                    //info.Remark = row[13].ToString();

                    ProcessInfo processInfo = new ProcessInfo();
                    DataTable tbProcess = getProcessesByID(info.ProcessDID);
                    if (tbProcess != null && tbProcess.Rows.Count > 0)
                    {
                        processInfo.ProcessDID = int.Parse(tbProcess.Rows[0][0].ToString());
                        processInfo.ProcessNO = tbProcess.Rows[0][1].ToString();
                        processInfo.ProcessName = tbProcess.Rows[0][2].ToString();
                        processInfo.CraftDID = int.Parse(tbProcess.Rows[0][3].ToString());
                        
                        DataTable tb = getCraftsByID(processInfo.CraftDID);
                        if (tb != null && tb.Rows.Count > 0)
                        {
                            CraftInfo craftInfo = new CraftInfo();
                            craftInfo.CraftDID = processInfo.CraftDID;
                            craftInfo.CraftNO = tb.Rows[0][1].ToString();
                            craftInfo.CraftName = tb.Rows[0][2].ToString();
                            processInfo.Craft = craftInfo;
                        }
                        else
                        {
                            processInfo.Craft = null;
                            throw new Exception("执行getCraftsByID查询为空");
                        }
                            
                        info.Process = processInfo;
                    }
                    else
                    {
                        info.Process = null;
                        throw new Exception("执行getProcessesByID查询为空");
                    }
                        
                    DataTable tbDeviceGroup = getDeviceGroupByID(info.DeviceGroupDID);
                    if (tbDeviceGroup != null && tbDeviceGroup.Rows.Count > 0)
                    {
                        DeviceGroupInfo deviceGroupInfo = new DeviceGroupInfo();
                        deviceGroupInfo.DeviceGroupDID = info.DeviceGroupDID;
                        deviceGroupInfo.DeviceGroupNO = tbDeviceGroup.Rows[0][1].ToString();
                        deviceGroupInfo.DeviceGroupName = tbDeviceGroup.Rows[0][2].ToString();
                        deviceGroupInfo.CraftDID = int.Parse(tbDeviceGroup.Rows[0][3].ToString());
                        
                        DataTable tb = getCraftsByID(deviceGroupInfo.CraftDID);
                        if (tb != null && tb.Rows.Count > 0)
                        {
                            CraftInfo craftInfo = new CraftInfo();
                            craftInfo.CraftDID = processInfo.CraftDID;
                            craftInfo.CraftNO = tb.Rows[0][1].ToString();
                            craftInfo.CraftName = tb.Rows[0][2].ToString();
                            deviceGroupInfo.Craft = craftInfo;
                        }
                        else
                        {
                            deviceGroupInfo.Craft = null;
                            throw new Exception("执行getCraftsByID查询为空");
                        }
                        info.DeviceGroup = deviceGroupInfo;
                    }
                    else
                    {
                        info.DeviceGroup = null;
                        throw new Exception("执行getDeviceGroupByID查询为空");
                    }
                    lst.Add(info);
                }
            }
            else
            {
                throw new Exception("执行getFacilities查询为空");
            }
            return lst;
        }

        private DataTable getProductionType_Join_Facility_Join_ProcessInfos(int craftDID)
        {
            Database equipDB = dataProvider.EQUIPDataBase;
            string sql = string.Format(
                @"select * from production_type pti
                    join facility_info fi on pti.facility_did = fi.facility_did
                    join process p on fi.process_did = p.process_did;");
            DataSet ds = equipDB.ExecuteDataSet(CommandType.Text, sql);
            return ds.Tables[0];
        }

        public IList<ProductionTypeInfo> GetProductionTypeInfos(int craftDID)
        {
            List<ProductionTypeInfo> lst = new List<ProductionTypeInfo>();
            DataTable dt = getProductionType_Join_Facility_Join_ProcessInfos(craftDID);
            if(dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    ProductionTypeInfo info = new ProductionTypeInfo();
                    info.DID = int.Parse(row[0].ToString());
                    info.ProductNo = row[1].ToString();
                    if (row[2].ToString() != "")
                        info.MinWeight = decimal.Parse(row[2].ToString());
                    if (row[3].ToString() != "")
                        info.MaxWeight = decimal.Parse(row[3].ToString());
                    if (row[4].ToString() != "")
                        info.MinScope = decimal.Parse(row[4].ToString());
                    if (row[5].ToString() != "")
                        info.MaxScope = decimal.Parse(row[5].ToString());
                    if (row[6].ToString() != "")
                        info.BarCodeLen = int.Parse(row[6].ToString());
                    if (row[7].ToString() != "")
                        info.PrefixLen = int.Parse(row[7].ToString());
                    info.PrefixData = row[8].ToString();
                    info.DefaultBarCode = row[9].ToString();
                    info.FacilityDID = int.Parse(row[10].ToString());
                    info.Time = DateTime.Parse(row[11].ToString());

                    lst.Add(info);
                }
            }
            return lst;
        }

        private DataTable getFacilities(int craftDID)
        {
            string connectionString_ATL = @"server=127.0.0.1;database=IFactory;uid=root;pwd=root;Persist Security Info=True;Charset=utf8;";
            MySqlConnection connatl = new MySqlConnection(connectionString_ATL);
            connatl.Open();
            string sql = string.Format(
                @"select * from facility_info fi
                    join process p on fi.process_did = p.process_did
                    where p.craft_did = {0};"
                                        , craftDID);
            MySqlDataAdapter b = new MySqlDataAdapter(sql, connatl);
            DataTable bt = new DataTable();
            b.Fill(bt);
            connatl.Close();
            return bt;
        }

        /// <summary>
        /// GetFacilities by craftDID
        /// </summary>
        /// <param name="craftDID">craftDID</param>
        /// <returns></returns>
        public IList<FacilityInfo> GetFacilities(int craftDID)
        {
            List<FacilityInfo> lst = new List<FacilityInfo>();
            DataTable dt = getFacilities(craftDID);
            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    FacilityInfo info = new FacilityInfo();
                    info.FacilityDID = int.Parse(row[0].ToString());
                    info.MMName = row[1].ToString();
                    info.ProcessDID = int.Parse(row[2].ToString());
                    info.DeviceGroupDID = int.Parse(row[3].ToString());
                    info.State = int.Parse(row[4].ToString());
                    info.MMIP = row[5].ToString();
                    info.MMPort = row[6].ToString();
                    info.MMIsUse = row[7].ToString() == "1" ? true : false;
                    info.MMClearAddr = row[8].ToString();
                    info.MMRestAddr = row[9].ToString();
                    if(row[10].ToString() != "")
                        info.MMSeq = int.Parse(row[10].ToString());
                    info.MAAddress = row[11].ToString();
                    info.IsUse = row[12].ToString() == "1" ? true : false;
                    info.Remark = row[13].ToString();

                    ProcessInfo processInfo = new ProcessInfo();
                    DataTable tbProcess = getProcessesByID(info.ProcessDID);
                    if (tbProcess != null && tbProcess.Rows.Count > 0)
                    {
                        processInfo.ProcessDID = int.Parse(tbProcess.Rows[0][0].ToString());
                        processInfo.ProcessNO = tbProcess.Rows[0][1].ToString();
                        processInfo.ProcessName = tbProcess.Rows[0][2].ToString();
                        processInfo.CraftDID = int.Parse(tbProcess.Rows[0][3].ToString());

                        
                        DataTable tb = getCraftsByID(processInfo.CraftDID);
                        if (tb != null && tb.Rows.Count > 0)
                        {
                            CraftInfo craftInfo = new CraftInfo();
                            craftInfo.CraftDID = processInfo.CraftDID;
                            craftInfo.CraftNO = tb.Rows[0][1].ToString();
                            craftInfo.CraftName = tb.Rows[0][2].ToString();
                            processInfo.Craft = craftInfo;
                        }
                        else
                        {
                            processInfo.Craft = null;
                            throw new Exception("执行getCraftsByID查询为空");
                        }
                        info.Process = processInfo;
                    }
                    else
                    {
                        info.Process = null;
                        processInfo = null;
                        throw new Exception("执行getProcessesByID查询为空");
                    }
                    

                    
                    DataTable tbDeviceGroup = getDeviceGroupByID(info.DeviceGroupDID);
                    if (tbDeviceGroup != null && tbDeviceGroup.Rows.Count > 0)
                    {
                        DeviceGroupInfo deviceGroupInfo = new DeviceGroupInfo();
                        deviceGroupInfo.DeviceGroupDID = info.DeviceGroupDID;
                        deviceGroupInfo.DeviceGroupNO = tbDeviceGroup.Rows[0][1].ToString();
                        deviceGroupInfo.DeviceGroupName = tbDeviceGroup.Rows[0][2].ToString();
                        deviceGroupInfo.CraftDID = int.Parse(tbDeviceGroup.Rows[0][3].ToString());

                        
                        DataTable tb = getCraftsByID(deviceGroupInfo.CraftDID);
                        if (tb != null && tb.Rows.Count > 0)
                        {
                            CraftInfo craftInfo = new CraftInfo();
                            craftInfo.CraftDID = processInfo.CraftDID;
                            craftInfo.CraftNO = tb.Rows[0][1].ToString();
                            craftInfo.CraftName = tb.Rows[0][2].ToString();
                            deviceGroupInfo.Craft = craftInfo;
                        }
                        else
                        {
                            deviceGroupInfo.Craft = null;
                            throw new Exception("执行getCraftsByID查询为空");
                        }
                            
                        info.DeviceGroup = deviceGroupInfo;
                    }
                    else
                    {
                        info.DeviceGroup = null;
                        throw new Exception("执行getDeviceGroupByID查询为空");
                    }
                    lst.Add(info);
                }
            }
            return lst;
        }
        
        public void AddUnit(UnitInfo unitInfo)
        {
            Database equipDB = dataProvider.EQUIPDataBase;
            string sql = string.Format(
                @"insert unit (unit_no, unit_name) values ('{0}', '{1}');"
                                        , unitInfo.UnitNO, unitInfo.UnitName);
            equipDB.ExecuteNonQuery(CommandType.Text, sql);
        }

        private DataTable getUnit(int did)
        {
            Database equipDB = dataProvider.EQUIPDataBase;
            string sql = string.Format(
                @"select * from unit u
                    where u.unit_did = {0};"
                                        , did);
            DataSet ds = equipDB.ExecuteDataSet(CommandType.Text, sql);
            return ds.Tables[0];
        }

        public UnitInfo GetUnit(int did)
        {
            DataTable dt = getUnit(did);
            if (dt != null && dt.Rows.Count > 0)
            {
                UnitInfo info = new UnitInfo();
                info.UnitDID = did;
                info.UnitNO = dt.Rows[0][1].ToString();
                info.UnitName = dt.Rows[0][2].ToString();
                return info;
            }
            return null;
        }

        public void UpdateUnit(UnitInfo unitInfo)
        {
            Database equipDB = dataProvider.EQUIPDataBase;
            string sql = string.Format(
                @"update unit
                    set unit_no = '{1}',
                        unit_name = '{2}'
                    where unit_did = {0};"
               , unitInfo.UnitDID, unitInfo.UnitNO, unitInfo.UnitName);
            equipDB.ExecuteNonQuery(CommandType.Text, sql);
        }

        public void DeleteUnit(UnitInfo unitInfo)
        {
            Database equipDB = dataProvider.EQUIPDataBase;
            string sql = string.Format(
                @"delete from unit
                    where unit_did = {0};"
               , unitInfo.UnitDID);
            equipDB.ExecuteNonQuery(CommandType.Text, sql);
        }

        public DataTable UnitTableQuery()
        {
            Database equipDB = dataProvider.EQUIPDataBase;
            string sql = "select * from unit order by unit_did";
            DataSet ds = equipDB.ExecuteDataSet(CommandType.Text, sql);
            return ds.Tables[0];
        }

        public IList<UnitInfo> GetAllUnits()
        {
            DataTable tb = UnitTableQuery();

            List<UnitInfo> lst = new List<UnitInfo>();
            if (tb != null && tb.Rows.Count > 0)
            {
                foreach (DataRow row in tb.Rows)
                {
                    UnitInfo info = new UnitInfo();
                    info.UnitDID = int.Parse(row[0].ToString());
                    info.UnitNO = row[1].ToString();
                    info.UnitName = row[2].ToString();
                    
                    lst.Add(info);
                }
            }
            return lst;
        }

        public void SaveCraftDetail(CraftDetailModel model)
        {
            DataTable tbCrafts = getCraftsByID(model.CraftDID);
            if (tbCrafts != null && tbCrafts.Rows.Count > 0)
            {
                CraftInfo craft = new CraftInfo();
                craft.CraftDID = int.Parse(tbCrafts.Rows[0][0].ToString());
                craft.CraftNO = tbCrafts.Rows[0][1].ToString();
                craft.CraftName = tbCrafts.Rows[0][2].ToString();

                CraftProbablyInfo craftProbably = this.GetCraftProbably(model.CraftDID,model.code);
                craft.CraftName = model.CraftName;

                UpdateCraft(craft);

                craftProbably.TargetYield = model.TargetYield;
                UpdateCraftProbablyInfo(craftProbably);
            }
            else
            {
                throw new Exception("执行getCraftsByID查询为空");
            }
        }

        private void UpdateCraft(CraftInfo info)
        {
            Database equipDB = dataProvider.EQUIPDataBase;
            string sql = string.Format(
                @"update craft
                    set craft_no = '{1}',
                        craft_name = '{2}'
                    where craft_did = {0};"
               , info.CraftDID, info.CraftNO, info.CraftName);
            equipDB.ExecuteNonQuery(CommandType.Text, sql);
        }

        private void UpdateCraftProbablyInfo(CraftProbablyInfo info)
        {
            Database equipDB = dataProvider.EQUIPDataBase;
            string sql = string.Format(
                @"update craft_probably
                    set BatteryBarCode = '{1}',
                        nowYield = {2},
                        targetYield = {3},
                        useName = '{4}',
                        carNO = '{5}'
                    where craft_did = {0};"
               , info.CraftDID, info.BatteryBarCode, info.NowYield, info.TargetYield
               , info.UseName, info.CarNO);
            equipDB.ExecuteNonQuery(CommandType.Text, sql);
        }

        public IList<CraftDetailModel> GetCraftDetails()
        {
            List<CraftInfo> lstCraftInfo = GetCrafts().ToList();
            List<CraftProbablyInfo> lstCraftProbablyInfo = GetCraftProbablys();

            return lstCraftInfo.Join(lstCraftProbablyInfo, c => c.CraftDID, cp => cp.CraftDID, (c, cp) => new CraftDetailModel()
            {
                CraftDID = c.CraftDID,
                CraftName = c.CraftName,
                CraftNO = c.CraftNO,
                TargetYield = cp.TargetYield
            }).ToList();
        }

        public CraftDetailModel GetCraftDetail(int craftDID)
        {
            return GetCraftDetails().ToList().Where(t => t.CraftDID == craftDID).FirstOrDefault();
        }
    }
}
