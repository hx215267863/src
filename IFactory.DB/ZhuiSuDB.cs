using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using IFactory.Domain.Models;
using PagedList;
using MySql.Data.MySqlClient;

namespace IFactory.DB
{
    public class ZhuiSuDB : BaseFacade
    {
        private DataTable getPagedZhuiSuData(int? processDID)
        {
            DateTime? DateE = DateTime.Now.Date;
            string connectionString_ATL = @"server=127.0.0.1;database=IFactory;uid=root;pwd=root;Persist Security Info=True;Charset=utf8;";
            MySqlConnection connatl = new MySqlConnection(connectionString_ATL);
            connatl.Open();
            string sql = "";
            sql = string.Format(
                @"select * from autoinspection1_facility_production_data auto 
                    join craft c on c.craft_did=auto.process_did 
                    join device_group de on de.device_group_did = auto.device_group_did
                    where StartDate >= '" + DateE.Value.ToString() +
                    "' and StartDate <= '" + DateE.Value.AddDays(1.0).ToString() + "'" + "order by auto.StartDate DESC");
            MySqlDataAdapter b = new MySqlDataAdapter(sql, connatl);
            DataTable bt = new DataTable();
            b.Fill(bt);
            connatl.Close();
            return bt;
        }

        /// <summary>
        /// 获取实时追溯信息
        /// </summary>
        /// <param name="processDID">当前选择的工序</param>
        /// <param name="pageNo"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public IPagedList<ZhuiSuItem> GetPagedZhuiSuData(int? processDID, int pageNo, int pageSize)
        {
            DataTable tb = getPagedZhuiSuData(processDID);

            List<ZhuiSuItem> lst = new List<ZhuiSuItem>();
            if (tb != null && tb.Rows.Count > 0)
            {
                foreach (DataRow row in tb.Rows)
                {
                    ZhuiSuItem info = new ZhuiSuItem();
                    info.Iden = int.Parse(row[0].ToString());
                    info.ProductNo = row[1].ToString();
                    info.BatteryBarCode = row[2].ToString();
                    info.DeviceGroupDID = row[60].ToString();
                    info.ProcessDID = row[57].ToString();
                    info.FacilityDID = int.Parse(row[5].ToString());
                    info.No = row[6].ToString();
                    info.StartDate = DateTime.Parse(row[7].ToString());
                    info.Result = int.Parse(row[8].ToString());
                    info.BackReturn = row[9].ToString();
                    info.BackErrcode = int.Parse(row[10].ToString());
                    info.FrontRetrun = row[11].ToString();
                    info.FrontErrcode = int.Parse(row[12].ToString());
                    info.HipotReturn = row[13].ToString();
                    info.HipotErrcode = int.Parse(row[14].ToString());
                    info.SidestripHeight = double.Parse(row[15].ToString());
                    info.SidestripWidth = double.Parse(row[16].ToString());
                    info.TopstripHeight = double.Parse(row[17].ToString());
                    info.MainBodyWidth = double.Parse(row[18].ToString());
                    info.MainBodyHeight = double.Parse(row[19].ToString());
                    info.DistanceBetweenTabs = double.Parse(row[20].ToString());
                    info.DistanceBetweenTab1Left = double.Parse(row[21].ToString());
                    info.DistanceBetwwnTab2Left = double.Parse(row[22].ToString());
                    info.BagAreaWidth = double.Parse(row[23].ToString());
                    info.TabALToSlotDistanceRight = double.Parse(row[24].ToString());
                    info.TabALToSlotDistanceLeft = double.Parse(row[25].ToString());
                    info.SealantHeightOfLeft1 = double.Parse(row[26].ToString());
                    info.SealantHeightOfLeft2 = double.Parse(row[27].ToString());
                    info.SealantHeightOfRight1 = double.Parse(row[28].ToString());
                    info.SealantHeightOfRight2 = double.Parse(row[29].ToString());
                    info.SealantToSlotDistanceLeft = double.Parse(row[30].ToString());
                    info.SealantToSlotDistanceRight = double.Parse(row[31].ToString());
                    info.measmode = int.Parse(row[33].ToString());
                    info.SidePoint1 = double.Parse(row[34].ToString());
                    info.SidePoint2 = double.Parse(row[35].ToString());
                    info.SidePoint3 = double.Parse(row[36].ToString());
                    info.TopPoint1 = double.Parse(row[37].ToString());
                    info.TopPoint2 = double.Parse(row[38].ToString());
                    info.TopPoint3 = double.Parse(row[39].ToString());
                    info.TabPoint1 = double.Parse(row[40].ToString());
                    info.TabPoint2 = double.Parse(row[41].ToString());
                    info.EndProductno = row[48].ToString();
                    info.Operator = row[53].ToString();

                    lst.Add(info);
                }
            }
            IQueryable<ZhuiSuItem> superset = lst.AsQueryable();
            return new PagedList<ZhuiSuItem>(superset, pageNo, pageSize);
        }


        private DataTable getPagedZhuiSuData2(int? processDID)
        {
            DateTime? DateE = DateTime.Now.Date;
            string connectionString_ATL = @"server=127.0.0.1;database=IFactory;uid=root;pwd=root;Persist Security Info=True;Charset=utf8;";
            MySqlConnection connatl = new MySqlConnection(connectionString_ATL);
            connatl.Open();
            string sql = "";
            sql = string.Format(
                 @"select *,de.device_group_name,c.craft_name from autoinspection2_facility_production_data auto
                    join craft c on c.craft_did=auto.process_did 
                    join device_group de on de.device_group_did = auto.device_group_did
                    where StartDate >= '" + DateE.Value.ToString() +
                    "' and StartDate <= '" + DateE.Value.AddDays(1.0).ToString() + "'" + "order by auto.StartDate DESC");
            MySqlDataAdapter b = new MySqlDataAdapter(sql, connatl);
            DataTable bt = new DataTable();
            b.Fill(bt);
            connatl.Close();
            return bt;
        }

        /// <summary>
        /// 获取实时追溯信息2
        /// </summary>
        /// <param name="processDID">当前选择的工序</param>
        /// <param name="pageNo"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public IPagedList<ZhuiSuItem> GetPagedZhuiSuData2(int? processDID, int pageNo, int pageSize)
        {
            DataTable tb = getPagedZhuiSuData2(processDID);

            List<ZhuiSuItem> lst = new List<ZhuiSuItem>();
            if (tb != null && tb.Rows.Count > 0)
            {
                foreach (DataRow row in tb.Rows)
                {
                    ZhuiSuItem info = new ZhuiSuItem();
                    info.Iden = int.Parse(row[0].ToString());
                    info.ProductNo = row[1].ToString();
                    info.BatteryBarCode = row[2].ToString();
                    info.DeviceGroupDID = row[35].ToString();
                    info.ProcessDID = row[38].ToString();
                    info.No = row[4].ToString();
                    info.StartDate = DateTime.Parse(row[5].ToString());
                    info.Result = int.Parse(row[6].ToString());
                    info.LocateReturn = row[7].ToString();
                    info.LocateErrcode = int.Parse(row[8].ToString());
                    info.LsideReturn = row[9].ToString();
                    info.LsideErrcode = int.Parse(row[10].ToString());
					info.RsideReturn = row[11].ToString();
                    info.RsideErrcode = int.Parse(row[12].ToString());
					info.TailReturn = row[13].ToString();
                    info.TailErrcode = int.Parse(row[14].ToString());
                    info.MainBodyWidthTop = double.Parse(row[15].ToString());
                    info.MainBodyWidthButtom = double.Parse(row[16].ToString());
                    info.MainBodyHeight = double.Parse(row[17].ToString());
                    info.SideLeftFoldingHightTop = double.Parse(row[18].ToString());
                    info.SideLeftFoldingHightButtom = double.Parse(row[19].ToString());
                    info.SideRightFoldingHightTop = double.Parse(row[20].ToString());
                    info.SideRightFoldingHightButtom = double.Parse(row[21].ToString());
                    info.SideThickness1 = double.Parse(row[22].ToString());
					info.FinalResult = row[23].ToString();
                    info.EndProductno = row[30].ToString();
                    info.Operator = row[32].ToString();

                    lst.Add(info);
                }
            }
            IQueryable<ZhuiSuItem> superset = lst.AsQueryable();
            return new PagedList<ZhuiSuItem>(superset, pageNo, pageSize);
        }

        public string w2 { get; set; }
        public string w3 { get; set; }
        /// <summary>
        /// 获取历史追溯记录
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="TimeStart"></param>
        /// <param name="TimeEnd"></param>
        /// <param name="pageNo"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public IPagedList<ZhuiSuItem> GetPagedZhuiSuHistory(string Keyword, DateTime? TimeStart, DateTime? TimeEnd, int PageNum, int PageSize)
        {
            string connectionString_ATL = @"server=127.0.0.1;database=IFactory;uid=root;pwd=root;Persist Security Info=True;Charset=utf8;";
            MySqlConnection connatl = new MySqlConnection(connectionString_ATL);
            connatl.Open();
            string sql = "";
            sql = @"select *,c.craft_name from autoinspection1_facility_production_data auto 
                    join craft c on c.craft_did=auto.process_did 
                    join device_group de on de.device_group_did = auto.device_group_did where";
            string w1 = " auto.batteryBarCode like '%" + Keyword + "%'";
            if (TimeStart.ToString() != "")
                w2 = TimeStart.HasValue ? " and auto.StartDate >= '" + TimeStart.Value.ToString() + "' " : "";
            if (TimeEnd.ToString() != "")
                w3 = TimeStart.HasValue ? " and auto.StartDate < '" + TimeEnd.Value.AddDays(1.0).ToString() + "' " : "";
            else
                TimeEnd = TimeStart;
            string w4 = " order by auto.StartDate DESC";

            MySqlDataAdapter b = new MySqlDataAdapter(sql +w1 +w2 +w3 +w4, connatl);
            DataTable bt = new DataTable();
            b.Fill(bt);
            connatl.Close();

            List<ZhuiSuItem> lstZ = new List<ZhuiSuItem>();
            if (bt != null && bt.Rows.Count > 0)
            {
                foreach (DataRow row in bt.Rows)
                {
                    ZhuiSuItem info = new ZhuiSuItem();
                    info.Iden = int.Parse(row[0].ToString());
                    info.ProductNo = row[1].ToString();
                    info.BatteryBarCode = row[2].ToString();
                    info.DeviceGroupDID = row[60].ToString();
                    info.ProcessDID = row[57].ToString();
                    info.FacilityDID = int.Parse(row[5].ToString());
                    info.No = row[6].ToString();
                    info.StartDate = DateTime.Parse(row[7].ToString());
                    info.Result = int.Parse(row[8].ToString());
                    info.BackReturn = row[9].ToString();
                    info.BackErrcode = int.Parse(row[10].ToString());
                    info.FrontRetrun = row[11].ToString();
                    info.FrontErrcode = int.Parse(row[12].ToString());
                    info.HipotReturn = row[13].ToString();
                    info.HipotErrcode = int.Parse(row[14].ToString());
                    info.SidestripHeight = double.Parse(row[15].ToString());
                    info.SidestripWidth = double.Parse(row[16].ToString());
                    info.TopstripHeight = double.Parse(row[17].ToString());
                    info.MainBodyWidth = double.Parse(row[18].ToString());
                    info.MainBodyHeight = double.Parse(row[19].ToString());
                    info.DistanceBetweenTabs = double.Parse(row[20].ToString());
                    info.DistanceBetweenTab1Left = double.Parse(row[21].ToString());
                    info.DistanceBetwwnTab2Left = double.Parse(row[22].ToString());
                    info.BagAreaWidth = double.Parse(row[23].ToString());
                    info.TabALToSlotDistanceRight = double.Parse(row[24].ToString());
                    info.TabALToSlotDistanceLeft = double.Parse(row[25].ToString());
                    info.SealantHeightOfLeft1 = double.Parse(row[26].ToString());
                    info.SealantHeightOfLeft2 = double.Parse(row[27].ToString());
                    info.SealantHeightOfRight1 = double.Parse(row[28].ToString());
                    info.SealantHeightOfRight2 = double.Parse(row[29].ToString());
                    info.SealantToSlotDistanceLeft = double.Parse(row[30].ToString());
                    info.SealantToSlotDistanceRight = double.Parse(row[31].ToString());
                    info.measmode = int.Parse(row[33].ToString());
                    info.SidePoint1 = double.Parse(row[34].ToString());
                    info.SidePoint2 = double.Parse(row[35].ToString());
                    info.SidePoint3 = double.Parse(row[36].ToString());
                    info.TopPoint1 = double.Parse(row[37].ToString());
                    info.TopPoint2 = double.Parse(row[38].ToString());
                    info.TopPoint3 = double.Parse(row[39].ToString());
                    info.TabPoint1 = double.Parse(row[40].ToString());
                    info.TabPoint2 = double.Parse(row[41].ToString());
                    info.EndProductno = row[48].ToString();
                    info.Operator = row[53].ToString();
                    lstZ.Add(info);
                }
            }


            IQueryable<ZhuiSuItem> superset = lstZ.AsQueryable();
            return new PagedList<ZhuiSuItem>(superset, PageNum, PageSize);
        }


        /// <summary>
        /// 获取历史追溯记录2
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="TimeStart"></param>
        /// <param name="TimeEnd"></param>
        /// <param name="pageNo"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public IPagedList<ZhuiSuItem> GetPagedZhuiSuHistory2(string Keyword, DateTime? TimeStart, DateTime? TimeEnd, int PageNum, int PageSize)
        {
            string connectionString_ATL = @"server=127.0.0.1;database=IFactory;uid=root;pwd=root;Persist Security Info=True;Charset=utf8;";
            MySqlConnection connatl = new MySqlConnection(connectionString_ATL);
            connatl.Open();
            string sql = "";
            sql = @"select *,c.craft_name from autoinspection2_facility_production_data auto 
                    join craft c on c.craft_did=auto.process_did 
                    join device_group de on de.device_group_did = auto.device_group_did where";
            string w1 = " auto.batteryBarCode like '%" + Keyword + "%'";
            if (TimeStart.ToString() != "")
                w2 = TimeStart.HasValue ? " and auto.StartDate >= '" + TimeStart.Value.ToString() + "' " : "";
            if (TimeEnd.ToString() != "")
                w3 = TimeStart.HasValue ? " and auto.StartDate < '" + TimeEnd.Value.AddDays(1.0).ToString() + "' " : "";
            else
                TimeEnd = TimeStart;
            string w4 = " order by auto.StartDate DESC";
            MySqlDataAdapter b = new MySqlDataAdapter(sql + w1 + w2 + w3 +w4, connatl);
            DataTable bt = new DataTable();
            b.Fill(bt);
            connatl.Close();

            List<ZhuiSuItem> lstZ = new List<ZhuiSuItem>();
            if (bt != null && bt.Rows.Count > 0)
            {
                foreach (DataRow row in bt.Rows)
                {
                    ZhuiSuItem info = new ZhuiSuItem();
                    info.Iden = int.Parse(row[0].ToString());
                    info.ProductNo = row[1].ToString();
                    info.BatteryBarCode = row[2].ToString();
                    info.DeviceGroupDID = row[35].ToString();
                    info.ProcessDID = row[38].ToString();
                    info.No = row[4].ToString();
                    info.StartDate = DateTime.Parse(row[5].ToString());
                    info.Result = int.Parse(row[6].ToString());
                    info.LocateReturn = row[7].ToString();
                    info.LocateErrcode = int.Parse(row[8].ToString());
                    info.LsideReturn = row[9].ToString();
                    info.LsideErrcode = int.Parse(row[10].ToString());
					info.RsideReturn = row[11].ToString();
                    info.RsideErrcode = int.Parse(row[12].ToString());
					info.TailReturn = row[13].ToString();
                    info.TailErrcode = int.Parse(row[14].ToString());
                    info.MainBodyWidthTop = double.Parse(row[15].ToString());
                    info.MainBodyWidthButtom = double.Parse(row[16].ToString());
                    info.MainBodyHeight = double.Parse(row[17].ToString());
                    info.SideLeftFoldingHightTop = double.Parse(row[18].ToString());
                    info.SideLeftFoldingHightButtom = double.Parse(row[19].ToString());
                    info.SideRightFoldingHightTop = double.Parse(row[20].ToString());
                    info.SideRightFoldingHightButtom = double.Parse(row[21].ToString());
                    info.SideThickness1 = double.Parse(row[22].ToString());
					info.FinalResult = row[23].ToString();
                    info.EndProductno = row[30].ToString();
                    info.Operator = row[32].ToString();

                    lstZ.Add(info);
                }
            }

            
            IQueryable<ZhuiSuItem> superset = lstZ.AsQueryable();
            return new PagedList<ZhuiSuItem>(superset, PageNum, PageSize);
        }

    }
}