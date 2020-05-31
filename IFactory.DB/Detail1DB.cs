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
using IFactory.Data.Crafts;
using IFactory.Domain.Crafts.Base.Entities;
using PagedList;
using IFactory.Common;
using IFactory.Domain.Crafts.MIB.Entities;
using IFactory.Domain.Crafts.FEF.Entities;
using IFactory.Domain.Models.Crafts;
using MySql.Data.MySqlClient;

namespace IFactory.DB
{
    public class Detail1DB : BaseFacade
    {
        private DataTable getDetail1(int? processDID)
        {
            string connectionString_ATL = @"server=127.0.0.1;database=IFactory;uid=root;pwd=root;Persist Security Info=True;Charset=utf8;";
            MySqlConnection connatl = new MySqlConnection(connectionString_ATL);
            connatl.Open();
            string sql = "";
            sql = @"select * from autoinspection1_facility_production_data auto 
                    order by iden DESC;";
            MySqlDataAdapter b = new MySqlDataAdapter(sql, connatl);
            DataTable bt = new DataTable();
            b.Fill(bt);
            connatl.Close();
            return bt;
        }

        /// <summary>
        /// 生产数据
        /// </summary>
        /// <param name="processDID">当前选择的工序</param>
        /// <param name="pageNo"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public IPagedList<Detail1Item> GetDetail1(int? processDID, int pageNo, int pageSize)
        {
            DataTable tb = getDetail1(processDID);

            List<Detail1Item> lst = new List<Detail1Item>();
            if (tb != null && tb.Rows.Count > 0)
            {
                foreach (DataRow row in tb.Rows)
                {
                    Detail1Item info = new Detail1Item();
                    info.Iden = int.Parse(row[0].ToString());
                    info.ProductNo = row[1].ToString();
                    info.BatteryBarCode = row[2].ToString();
                    info.DeviceGroupDID = int.Parse(row[3].ToString());
                    info.ProcessDID = int.Parse(row[4].ToString());
                    info.FacilityDID = int.Parse(row[5].ToString());
                    info.No = row[6].ToString();
                    info.StartDate = DateTime.Parse(row[7].ToString());
                    info.Result = int.Parse(row[8].ToString());
                    info.BackReturn = int.Parse(row[9].ToString());
                    info.BackErrcode = int.Parse(row[10].ToString());
                    info.FrontRetrun = int.Parse(row[11].ToString());
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


                    lst.Add(info);
                }
            }
            IQueryable<Detail1Item> superset = lst.AsQueryable();
            return new PagedList<Detail1Item>(superset, pageNo, pageSize);
        }

        private DataTable getDetail12(int? processDID)
        {
            string connectionString_ATL = @"server=127.0.0.1;database=IFactory;uid=root;pwd=root;Persist Security Info=True;Charset=utf8;";
            MySqlConnection connatl = new MySqlConnection(connectionString_ATL);
            connatl.Open();
            string sql = "";
            sql = @"select * from autoinspection2_facility_production_data auto
                    order by iden DESC;";
            MySqlDataAdapter b = new MySqlDataAdapter(sql, connatl);
            DataTable bt = new DataTable();
            b.Fill(bt);
            connatl.Close();
            return bt;
        }

        /// <summary>
        /// 生产数据2
        /// </summary>
        /// <param name="processDID">当前选择的工序</param>
        /// <param name="pageNo"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public IPagedList<Detail1Item> GetDetail12(int? processDID, int pageNo, int pageSize)
        {
            DataTable tb = getDetail12(processDID);

            List<Detail1Item> lst = new List<Detail1Item>();
            if (tb != null && tb.Rows.Count > 0)
            {
                foreach (DataRow row in tb.Rows)
                {
                    Detail1Item info = new Detail1Item();
                    info.Iden = int.Parse(row[0].ToString());
                    info.ProductNo = row[1].ToString();
                    info.BatteryBarCode = row[2].ToString();
                    info.DeviceGroupDID = int.Parse(row[3].ToString());
                    info.No = row[4].ToString();
                    info.StartDate = DateTime.Parse(row[5].ToString());
                    info.Result = int.Parse(row[6].ToString());
                    info.BackReturn = int.Parse(row[7].ToString());
                    info.BackErrcode = int.Parse(row[8].ToString());
                    info.FrontRetrun = int.Parse(row[9].ToString());
                    info.FrontErrcode = int.Parse(row[10].ToString());
                    info.MainBodyWidthTop = double.Parse(row[11].ToString());
                    info.MainBodyWidthButtom = double.Parse(row[12].ToString());
                    info.MainBodyHeight = double.Parse(row[13].ToString());
                    info.TopsealHeight = double.Parse(row[14].ToString());
                    info.DistanceBetweenTab1Left = double.Parse(row[15].ToString());
                    info.DistanceBetwwnTab2Left = double.Parse(row[16].ToString());
                    info.DistanceBetweenTab = double.Parse(row[17].ToString());
                    info.LeftTabHeight = double.Parse(row[18].ToString());
                    info.RightTabHeight = double.Parse(row[19].ToString());
                    info.LeftTabWidth = double.Parse(row[20].ToString());
                    info.RightTabWidth = double.Parse(row[21].ToString());



                    lst.Add(info);
                }
            }
            IQueryable<Detail1Item> superset = lst.AsQueryable();
            return new PagedList<Detail1Item>(superset, pageNo, pageSize);
        }
    }
}