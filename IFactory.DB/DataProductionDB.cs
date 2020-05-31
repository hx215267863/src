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
    public class DataProductionDB : BaseFacade
    {       
        /// <summary>
        /// 获取历史生产数据记录
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="TimeStart"></param>
        /// <param name="TimeEnd"></param>
        /// <param name="pageNo"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public IPagedList<DataProductionItem> GetPagedDataProductionHistory(string Keyword, DateTime? TimeStart, int PageNum, int PageSize,string code)
        {
            string connectionString_ATL = @"server=127.0.0.1;database=IFactory;uid=root;pwd=root;Persist Security Info=True;Charset=utf8;";
            MySqlConnection connatl = new MySqlConnection(connectionString_ATL);
            connatl.Open();
            string sql = @"select cap.编号
                    , cap.时间
                    , cap.产能
                    , cap.良品数
                    , cap.良品率
                    from capacityofproduction cap where";
            string w1 = " cap.编号 like '%" + Keyword + "%'";
            string w2 = TimeStart.HasValue ? " and cap.时间 >= '" + TimeStart.Value.ToString() + "' " : "";
            string w3 = TimeStart.HasValue ? " and cap.时间 < '" + TimeStart.Value.AddDays(1.0).ToString() + "' " : "";
            string w4 = " order by cap.时间 DESC";
            MySqlDataAdapter b = new MySqlDataAdapter(sql +w1 +w2 +w3 +w4, connatl);
            DataTable bt = new DataTable();
            b.Fill(bt);
            connatl.Close();

            List<DataProductionItem> lstZ = new List<DataProductionItem>();
            if (bt != null && bt.Rows.Count > 0)
            {
                foreach (DataRow row in bt.Rows)
                {
                    DataProductionItem info = new DataProductionItem();
                    info.Iden = int.Parse(row[0].ToString());
                    info.ProductTime = DateTime.Parse(row[1].ToString());
                    info.CellTotal = int.Parse(row[2].ToString());
                    info.OKCount = int.Parse(row[3].ToString());
                    info.OKRate = row[4].ToString();
                    lstZ.Add(info);
                }
            }

            connatl.Close();
            IQueryable<DataProductionItem> superset = lstZ.AsQueryable();
            return new PagedList<DataProductionItem>(superset, PageNum, PageSize);
        }

        /// <summary>
        /// 获取历史生产数据2记录
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="TimeStart"></param>
        /// <param name="TimeEnd"></param>
        /// <param name="pageNo"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public IPagedList<DataProductionItem> GetPagedDataProductionHistory2(string Keyword, DateTime? TimeStart, DateTime? TimeEnd, int PageNum, int PageSize,string code)
        {
            string connectionString_ATL = @"server=127.0.0.1;database=IFactory;uid=root;pwd=root;Persist Security Info=True;Charset=utf8;";
            MySqlConnection connatl = new MySqlConnection(connectionString_ATL);
            connatl.Open();
            string sql = @"select run.id
                    , run.datetime
                    , run.celltotal
                    , run.NGCount
                    , run.device_group_did
                    , run.RuningTime
                    , run.WaitTime
                    , run.StopTime
                    , run.end_product_no
                    from autoinspection2_run_total_collect run where";
            string w1 = " run.celltotal like '%" + Keyword + "%'";
            string w2 = TimeStart.HasValue ? " and auto.StartDate >= '" + TimeStart.Value.ToString() + "' " : "";
            string w3 = TimeStart.HasValue ? " and auto.StartDate < '" + TimeEnd.Value.AddDays(1.0).ToString() + "' " : "";
            string w4 = " order by run.datetime DESC";
            MySqlDataAdapter b = new MySqlDataAdapter(sql +w1+ w2 + w3 +w4, connatl);
            DataTable bt = new DataTable();
            b.Fill(bt);
            connatl.Close();

            List<DataProductionItem> lstZ = new List<DataProductionItem>();
            if (bt != null && bt.Rows.Count > 0)
            {
                foreach (DataRow row in bt.Rows)
                {
                    DataProductionItem info = new DataProductionItem();
                    info.Iden = int.Parse(row[0].ToString());
                    info.ProductTime = DateTime.Parse(row[1].ToString());
                    info.CellTotal = int.Parse(row[2].ToString());
                    info.OKCount = int.Parse(row[2].ToString()) - int.Parse(row[3].ToString());
                    info.DeviceDid = int.Parse(row[4].ToString());
                    info.EnableProduction = int.Parse(row[4].ToString());
                    info.RunningTime = int.Parse(row[5].ToString());
                    info.WaitTime = int.Parse(row[6].ToString());
                    info.StopTime = int.Parse(row[7].ToString());
                    if (int.Parse(row[2].ToString()) != 0)
                    {
                        info.OKRate = ((1 - int.Parse(row[3].ToString())
                        / int.Parse(row[2].ToString())) * 100).ToString();
                    }
                    else
                    {
                        info.OKRate = "0";
                    }
                    info.code = row[8].ToString();

                    lstZ.Add(info);
                }
            }
            connatl.Close();
            IQueryable<DataProductionItem> superset = lstZ.AsQueryable();
            return new PagedList<DataProductionItem>(superset, PageNum, PageSize);
        }

        /// <summary>
        /// 获取实时生产数据记录
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="TimeStart"></param>
        /// <param name="TimeEnd"></param>
        /// <param name="pageNo"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public IPagedList<DataProductionItem> GetPagedDataProductionReal(string Keyword, DateTime? TimeStart, DateTime? TimeEnd, int PageNum, int PageSize,string code)
        {
            DateTime? DateE = DateTime.Now.Date;
            string connectionString_ATL = @"server=127.0.0.1;database=IFactory;uid=root;pwd=root;Persist Security Info=True;Charset=utf8;";
            MySqlConnection connatl = new MySqlConnection(connectionString_ATL);
            connatl.Open();
            string sql = @"select cap.编号
                    , cap.时间
                    , cap.产能
                    , cap.良品数
                    , cap.良品率
                    from capacityofproduction cap where cap.时间 >= '"+ DateE.Value.ToString()+
                    "' and cap.时间 <= '"+ DateE.Value.AddDays(1.0).ToString() + "'";
            string w1 = " and cap.编号 like '%" + Keyword + "%'";
            string w2 = " order by cap.时间 DESC";
            MySqlDataAdapter b = new MySqlDataAdapter(sql + w1 + w2, connatl);
            DataTable bt = new DataTable();
            b.Fill(bt);
            connatl.Close();

            List<DataProductionItem> lstZ = new List<DataProductionItem>();
            if (bt != null && bt.Rows.Count > 0)
            {
                foreach (DataRow row in bt.Rows)
                {
                    DataProductionItem info = new DataProductionItem();
                    info.Iden = int.Parse(row[0].ToString());
                    info.ProductTime = DateTime.Parse(row[1].ToString());
                    info.CellTotal = int.Parse(row[2].ToString());
                    info.OKCount = int.Parse(row[3].ToString());
                    info.OKRate = row[4].ToString();
                    lstZ.Add(info);
                }
            }
            connatl.Close();
            IQueryable<DataProductionItem> superset = lstZ.AsQueryable();
            return new PagedList<DataProductionItem>(superset, PageNum, PageSize);
        }

        /// <summary>
        /// 获取实时生产数据记录2
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="TimeStart"></param>
        /// <param name="TimeEnd"></param>
        /// <param name="pageNo"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public IPagedList<DataProductionItem> GetPagedDataProductionReal2(string Keyword, DateTime? TimeStart, DateTime? TimeEnd, int PageNum, int PageSize, string code)
        {
            DateTime? DateE = DateTime.Now.Date;
            string connectionString_ATL = @"server = 127.0.0.1;database=ifactory;uid=root;pwd=root;Persist Security Info=True;Charset=utf8;";
            MySqlConnection connatl = new MySqlConnection(connectionString_ATL);
            connatl.Open();
            string sql = @"select run.id
                    , run.datetime
                    , run.celltotal
                    , run.NGCount
                    , run.device_group_did
                    , run.RuningTime
                    , run.WaitTime
                    , run.StopTime
                    , run.end_product_no
                    from autoinspection2_run_total_collect_real run
                    where run.datetime >= '" + DateE.Value.ToString() +
                    "' and run.datetime <= '" + DateE.Value.AddDays(1.0).ToString() + "'";
            string w1 = " and run.celltotal like '%" + Keyword + "%'";
            string w2 = " order by run.datetime DESC";
            MySqlDataAdapter b = new MySqlDataAdapter(sql + w1 + w2, connatl);
            DataTable bt = new DataTable();
            b.Fill(bt);
            connatl.Close();

            List<DataProductionItem> lstZ = new List<DataProductionItem>();
            if (bt != null && bt.Rows.Count > 0)
            {
                foreach (DataRow row in bt.Rows)
                {
                    DataProductionItem info = new DataProductionItem();
                    info.Iden = int.Parse(row[0].ToString());
                    info.ProductTime = DateTime.Parse(row[1].ToString());
                    info.CellTotal = int.Parse(row[2].ToString());
                    info.OKCount = int.Parse(row[2].ToString()) - int.Parse(row[3].ToString());
                    info.DeviceDid = int.Parse(row[4].ToString());
                    info.EnableProduction = int.Parse(row[4].ToString());
                    info.RunningTime = int.Parse(row[5].ToString());
                    info.WaitTime = int.Parse(row[6].ToString());
                    info.StopTime = int.Parse(row[7].ToString());
                    if (int.Parse(row[2].ToString()) != 0)
                    {
                        info.OKRate = ((1 - int.Parse(row[3].ToString())
                        / int.Parse(row[2].ToString())) * 100).ToString();
                    }
                    else
                    {
                        info.OKRate = "0";
                    }
                    info.code = row[8].ToString();  


                    lstZ.Add(info);
                }
            }

            connatl.Close();
            IQueryable<DataProductionItem> superset = lstZ.AsQueryable();
            return new PagedList<DataProductionItem>(superset, PageNum, PageSize);
        }

        /// <summary>
        /// 易损件数据
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="TimeStart"></param>
        /// <param name="TimeEnd"></param>
        /// <param name="pageNo"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public IPagedList<DataVulnerableItem> GetPagedDataVulnerable(string Keyword, DateTime? TimeStart, DateTime? TimeEnd, int PageNum, int PageSize, string code)
        {
            string connectionString_ATL = @"server=127.0.0.1;database=ifactory;uid=root;pwd=root;Persist Security Info=True;Charset=utf8;";
            MySqlConnection connatl = new MySqlConnection(connectionString_ATL);
            connatl.Open();
            string sql = @"select * from Vulnerable";
            string w1 = " where Name like '%" + Keyword + "%'";
            string w2 = TimeStart.HasValue ? " and time >= '" + TimeStart.Value.ToString() + "' " : "";
            string w3 = TimeStart.HasValue ? " and time < '" + TimeEnd.Value.AddDays(1.0).ToString() + "' " : "";
            MySqlDataAdapter b = new MySqlDataAdapter(sql + w1 + w2 + w3, connatl);
            DataTable bt = new DataTable();
            b.Fill(bt);
            connatl.Close();

            List<DataVulnerableItem> lstZ = new List<DataVulnerableItem>();
            if (bt != null && bt.Rows.Count > 0)
            {
                foreach (DataRow row in bt.Rows)
                {
                    DataVulnerableItem info = new DataVulnerableItem();
                    info.Iden = int.Parse(row[0].ToString());
                    info.Name = row[1].ToString();
                    info.Used = int.Parse(row[2].ToString());
                    info.Expect = int.Parse(row[3].ToString());
                    info.Exchange = int.Parse(row[4].ToString());
                    info.time = DateTime.Parse(row[5].ToString());
                    info.User = row[6].ToString();
                    info.PicNum1 = row[7].ToString();
                    info.PicNum2 = row[8].ToString();


                    lstZ.Add(info);
                }
            }

            connatl.Close();
            IQueryable<DataVulnerableItem> superset = lstZ.AsQueryable();
            return new PagedList<DataVulnerableItem>(superset, PageNum, PageSize);
        }

        /// <summary>
        /// 易损件数据
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="TimeStart"></param>
        /// <param name="TimeEnd"></param>
        /// <param name="pageNo"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public IList<DataVulnerableItem> GetDataVulnerable()
        {
            string connectionString_ATL = @"server = 127.0.0.1;database=ifactory;uid=root;pwd=root;Persist Security Info=True;Charset=utf8;";
            MySqlConnection connatl = new MySqlConnection(connectionString_ATL);
            connatl.Open();
            string sql = @"select * from Vulnerable";
            MySqlDataAdapter b = new MySqlDataAdapter(sql, connatl);
            DataTable bt = new DataTable();
            b.Fill(bt);
            connatl.Close();

            List<DataVulnerableItem> lstZ = new List<DataVulnerableItem>();
            if (bt != null && bt.Rows.Count > 0)
            {
                foreach (DataRow row in bt.Rows)
                {
                    DataVulnerableItem info = new DataVulnerableItem();
                    info.Iden = int.Parse(row[0].ToString());
                    info.Name = row[1].ToString();
                    info.Used = int.Parse(row[2].ToString());
                    info.Expect = int.Parse(row[3].ToString());
                    info.Exchange = int.Parse(row[4].ToString());
                    info.time = DateTime.Parse(row[5].ToString());
                    info.User = row[6].ToString();
                    info.PicNum1 = row[7].ToString();
                    info.PicNum2 = row[8].ToString();


                    lstZ.Add(info);
                }
            }

            connatl.Close();
            return lstZ;
        }
    }
}