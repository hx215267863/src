using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using IFactory.Domain.Models;
using PagedList;
using MySql.Data.MySqlClient;

namespace IFactory.DB
{

    public class ProductNGDB : BaseFacade
    {
        private DataTable getPagedProductNGData(int? processDID)
        {
            string connectionString_ATL = @"server=127.0.0.1;database=IFactory;uid=root;pwd=root;Persist Security Info=True;Charset=utf8;";
            MySqlConnection connatl = new MySqlConnection(connectionString_ATL);
            connatl.Open();
            string sql = "";
            sql = string.Format(
                   @"select run.craft_did
                    , run.RuningTime
                    , run.StopTime
                    , run.id
                    , run.NGCount
                    , run.celltotal
                    from autoinspection1_run_total_collect run");

            MySqlDataAdapter b = new MySqlDataAdapter(sql, connatl);
            DataTable bt = new DataTable();
            b.Fill(bt);
            connatl.Close();
            return bt;
        }

        /// <summary>
        /// 获取异常生产报表信息
        /// </summary>
        /// <param name="processDID">当前选择的工序</param>
        /// <param name="pageNo"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public IPagedList<ProductNGItem> GetPagedProductNGData(int? processDID, int pageNo, int pageSize)
        {
            DataTable tb = getPagedProductNGData(processDID);

            List<ProductNGItem> lst = new List<ProductNGItem>();
            if (tb != null && tb.Rows.Count > 0)
            {
                foreach (DataRow row in tb.Rows)
                {
                    ProductNGItem info = new ProductNGItem();
                    info.DeviceNo = row[0].ToString();
                    info.ProductTime = int.Parse(row[1].ToString());
                    info.NGTime = int.Parse(row[2].ToString());
                    info.QiGangNG = int.Parse(row[3].ToString());
                    info.MotorNG = int.Parse(row[4].ToString());
                    info.GanYingNG = int.Parse(row[5].ToString());

                    lst.Add(info);
                }
            }
            IQueryable<ProductNGItem> superset = lst.AsQueryable();
            return new PagedList<ProductNGItem>(superset, pageNo, pageSize);
        }

        private DataTable getPagedProductNGData2(int? processDID)
        {
            string connectionString_ATL = @"server=127.0.0.1;database=IFactory;uid=root;pwd=root;Persist Security Info=True;Charset=utf8;";
            MySqlConnection connatl = new MySqlConnection(connectionString_ATL);
            connatl.Open();
            string sql = "";
            sql = string.Format(
                   @"select run.craft_did
                    , run.RuningTime
                    , run.StopTime
                    , run.id
                    , run.NGCount
                    , run.celltotal
                    from autoinspection2_run_total_collect run");

            MySqlDataAdapter b = new MySqlDataAdapter(sql, connatl);
            DataTable bt = new DataTable();
            b.Fill(bt);
            connatl.Close();
            return bt;
        }

        /// <summary>
        /// 获取异常生产报表2信息
        /// </summary>
        /// <param name="processDID">当前选择的工序</param>
        /// <param name="pageNo"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public IPagedList<ProductNGItem> GetPagedProductNGData2(int? processDID, int pageNo, int pageSize)
        {
            DataTable tb = getPagedProductNGData2(processDID);

            List<ProductNGItem> lst = new List<ProductNGItem>();
            if (tb != null && tb.Rows.Count > 0)
            {
                foreach (DataRow row in tb.Rows)
                {
                    ProductNGItem info = new ProductNGItem();
                    info.DeviceNo = row[0].ToString();
                    info.ProductTime = int.Parse(row[1].ToString());
                    info.NGTime = int.Parse(row[2].ToString());
                    info.QiGangNG = int.Parse(row[3].ToString());
                    info.MotorNG = int.Parse(row[4].ToString());
                    info.GanYingNG = int.Parse(row[5].ToString());

                    lst.Add(info);
                }
            }
            IQueryable<ProductNGItem> superset = lst.AsQueryable();
            return new PagedList<ProductNGItem>(superset, pageNo, pageSize);

        }
        
        private DataTable getPagedProductOKData(int? processDID)
        {
            string connectionString_ATL = @"server=127.0.0.1;database=IFactory;uid=root;pwd=root;Persist Security Info=True;Charset=utf8;";
            MySqlConnection connatl = new MySqlConnection(connectionString_ATL);
            connatl.Open();
            string sql = "";
            sql = @"select distinct
                      run.device_group_did
                    , auto.ProductNo
                    , run.datetime
                    , run.runingTime
                    , run.WaitTime
                    , run.StopTime
                    from autoinspection1_run_total_collect run
                    join autoinspection1_facility_production_data auto on run.device_group_did = auto.device_group_did";

            MySqlDataAdapter b = new MySqlDataAdapter(sql, connatl);
            DataTable bt = new DataTable();
            b.Fill(bt);
            connatl.Close();
            return bt;
        }

        /// <summary>
        /// 获取生产报表信息
        /// </summary>
        /// <param name="processDID">当前选择的工序</param>
        /// <param name="pageNo"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public IPagedList<ProductNGItem> GetPagedProductOKData(int? processDID, int pageNo, int pageSize)
        {
            DataTable tb = getPagedProductOKData(processDID);

            List<ProductNGItem> lst = new List<ProductNGItem>();
            if (tb != null && tb.Rows.Count > 0)
            {
                foreach (DataRow row in tb.Rows)
                {
                    ProductNGItem info = new ProductNGItem();
                    info.DeviceNo = row[0].ToString();
                    info.ProductionNo = row[1].ToString();
                    info.ProductionStart = DateTime.Parse(row[2].ToString());
                    info.ProductTime = int.Parse(row[3].ToString());
                    info.WaitTime = int.Parse(row[4].ToString());
                    info.NGTime = int.Parse(row[5].ToString());

                    //info.ProductionNo = info.ProductionStart.Date.ToString();

                    lst.Add(info);
                }
            }
            IQueryable<ProductNGItem> superset = lst.AsQueryable();
            return new PagedList<ProductNGItem>(superset, pageNo, pageSize);
        }


        private DataTable getPagedProductOKData2(int? processDID)
        {
            Database equipDB = dataProvider.EQUIPDataBase;
            string sql = "";
            sql = string.Format(
                @"select distinct
                      d.device_group_no
                    , auto.ProductNo
                    , c.time
                    , run.runingTime
                    , run.WaitTime
                    , run.StopTime
                    from autoinspection2_run_total_collect run
                    join device_group d on d.device_group_did = run.device_group_did
                    join craft_probably c on run.datetime = c.time
                    join autoinspection2_facility_production_data auto on run.device_group_did = auto.device_group_did;");

            DataSet ds = equipDB.ExecuteDataSet(CommandType.Text, sql);
            return ds.Tables[0];
        }

        /// <summary>
        /// 获取生产报表2信息
        /// </summary>
        /// <param name="processDID">当前选择的工序</param>
        /// <param name="pageNo"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public IPagedList<ProductNGItem> GetPagedProductOKData2(int? processDID, int pageNo, int pageSize)
        {
            DataTable tb = getPagedProductOKData2(processDID);

            List<ProductNGItem> lst = new List<ProductNGItem>();
            if (tb != null && tb.Rows.Count > 0)
            {
                foreach (DataRow row in tb.Rows)
                {
                    ProductNGItem info = new ProductNGItem();
                    info.DeviceNo = row[0].ToString();
                    info.ProductionNo = row[1].ToString();
                    info.ProductionStart = DateTime.Parse(row[2].ToString());
                    info.ProductTime = int.Parse(row[3].ToString());
                    info.WaitTime = int.Parse(row[4].ToString());
                    info.NGTime = int.Parse(row[5].ToString());

                    lst.Add(info);
                }
            }
            IQueryable<ProductNGItem> superset = lst.AsQueryable();
            return new PagedList<ProductNGItem>(superset, pageNo, pageSize);
        }

    }
}