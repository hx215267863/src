using System;
using System.Collections.Generic;
using System.Data;
using IFactory.Domain.Models;
using MySql.Data.MySqlClient;

namespace IFactory.DB
{
    public class DataPicDB : BaseFacade
    {
        /// <summary>
        /// 生产产能
        /// </summary>
        /// <param name="TimeStart"></param>
        /// <param name="TimeEnd"></param>
        /// <returns></returns>
        public IList<DataPicItem> GetDataCapacity(DateTime? TimeStart, DateTime? TimeEnd, DateTime dates, DateTime datee)
        {
            string connectionString_ATL = @"server=127.0.0.1;database=IFactory;uid=root;pwd=root;Persist Security Info=True;Charset=utf8;";
            MySqlConnection connatl = new MySqlConnection(connectionString_ATL);
            connatl.Open();

            DateTime? DateS = dates;
            DateTime? DateE = datee;
            string sql = @"select  c.time
                            ,c.targetYield
                            ,c.nowYield
                            from craft_probably c
                            where c.nowYield <> 0 ";

            string w1 = DateS.HasValue ? "and c.time >= '" + DateS.ToString() + "'" : "";
            string w2 = DateE.HasValue ? " and c.time < '" + DateE.Value.AddDays(1.0).ToString() + "'" : "";
            string w3 = " order by time";
            MySqlDataAdapter b = new MySqlDataAdapter(sql +w1 +w2 +w3, connatl);
            DataTable bt = new DataTable();
            b.Fill(bt);
            connatl.Close();


            List<DataPicItem> lstDataCapacityModel = new List<DataPicItem>();
            
            if (bt != null && bt.Rows.Count > 0)
            {
                foreach (DataRow row in bt.Rows)
                {
                    DataPicItem info = new DataPicItem();
                    info.ProductTime = DateTime.Parse(row[0].ToString());
                    info.CapacityPage = (int)(float.Parse(row[2].ToString())/ float.Parse(row[1].ToString()) *100);

                    lstDataCapacityModel.Add(info);
                }
            }
            connatl.Close();
            return lstDataCapacityModel;
        }

        //工作日数
        public IList<DataPicItem> GetWorkDays()
        {
            string connectionString_ATL = @"server=127.0.0.1;database=IFactory;uid=root;pwd=root;Persist Security Info=True;Charset=utf8;";
            MySqlConnection connatl = new MySqlConnection(connectionString_ATL);
            connatl.Open();
            string sql = @"select  MCCollectDDate
                            from autoinspection1_run_total_collect
                            where year(MCCollectDDate) = year(Now())
                            order by MCCollectDDate";
            MySqlDataAdapter b = new MySqlDataAdapter(sql, connatl);
            DataTable bt = new DataTable();
            b.Fill(bt);
            connatl.Close();

            List<DataPicItem> lstDataCapacityModel = new List<DataPicItem>();

            if (bt != null && bt.Rows.Count > 0)
            {
                foreach (DataRow row in bt.Rows)
                {
                    DataPicItem info = new DataPicItem();
                    info.ProductTime = DateTime.Parse(row[0].ToString());

                    lstDataCapacityModel.Add(info);
                }
            }
            connatl.Close();
            return lstDataCapacityModel;
        }

        //工作日数2
        public IList<DataPicItem> GetWorkDays2()
        {
            string connectionString_ATL = @"server=127.0.0.1;database=IFactory;uid=root;pwd=root;Persist Security Info=True;Charset=utf8;";
            MySqlConnection connatl = new MySqlConnection(connectionString_ATL);
            connatl.Open();
            string sql = @"select  MCCollectDDate
                            from autoinspection2_run_total_collect
                            where year(MCCollectDDate) = year(Now())
                            order by MCCollectDDate";
            MySqlDataAdapter b = new MySqlDataAdapter(sql, connatl);
            DataTable bt = new DataTable();
            b.Fill(bt);
            connatl.Close();

            List<DataPicItem> lstDataCapacityModel = new List<DataPicItem>();

            if (bt != null && bt.Rows.Count > 0)
            {
                foreach (DataRow row in bt.Rows)
                {
                    DataPicItem info = new DataPicItem();
                    info.ProductTime = DateTime.Parse(row[0].ToString());

                    lstDataCapacityModel.Add(info);
                }
            }
            connatl.Close();
            return lstDataCapacityModel;
        }

        //工作日历
        public IList<DataPicItem> GetWorkDate()
        {
            string connectionString_ATL = @"server=127.0.0.1;database=IFactory;uid=root;pwd=root;Persist Security Info=True;Charset=utf8;";
            MySqlConnection connatl = new MySqlConnection(connectionString_ATL);
            connatl.Open();
            string sql = @"select  StartDate
                            from autoinspection1_facility_production_data
                            where month(StartDate) = month(Now())
                            order by StartDate";
            MySqlDataAdapter b = new MySqlDataAdapter(sql, connatl);
            DataTable bt = new DataTable();
            b.Fill(bt);
            connatl.Close();

            List<DataPicItem> lstDataCapacityModel = new List<DataPicItem>();

            if (bt != null && bt.Rows.Count > 0)
            {
                foreach (DataRow row in bt.Rows)
                {
                    DataPicItem info = new DataPicItem();
                    info.ProductTime = DateTime.Parse(row[0].ToString());

                    lstDataCapacityModel.Add(info);
                }
            }
            connatl.Close();
            return lstDataCapacityModel;
        }

        //工作日历2
        public IList<DataPicItem> GetWorkDate2()
        {
            string connectionString_ATL = @"server=127.0.0.1;database=IFactory;uid=root;pwd=root;Persist Security Info=True;Charset=utf8;";
            MySqlConnection connatl = new MySqlConnection(connectionString_ATL);
            connatl.Open();
            string sql = @"select  StartDate
                            from autoinspection2_facility_production_data
                            where month(StartDate) = month(Now())
                            order by StartDate";
            MySqlDataAdapter b = new MySqlDataAdapter(sql, connatl);
            DataTable bt = new DataTable();
            b.Fill(bt);
            connatl.Close();

            List<DataPicItem> lstDataCapacityModel = new List<DataPicItem>();

            if (bt != null && bt.Rows.Count > 0)
            {
                foreach (DataRow row in bt.Rows)
                {
                    DataPicItem info = new DataPicItem();
                    info.ProductTime = DateTime.Parse(row[0].ToString());

                    lstDataCapacityModel.Add(info);
                }
            }
            connatl.Close();
            return lstDataCapacityModel;
        }


        /// <summary>
        /// 生产产能2
        /// </summary>
        /// <param name="TimeStart"></param>
        /// <param name="TimeEnd"></param>
        /// <returns></returns>
        public IList<DataPicItem> GetDataCapacity2(DateTime? TimeStart, DateTime? TimeEnd, DateTime dates, DateTime datee)
        {
            string connectionString_ATL = @"server=127.0.0.1;database=IFactory;uid=root;pwd=root;Persist Security Info=True;Charset=utf8;";
            MySqlConnection connatl = new MySqlConnection(connectionString_ATL);
            connatl.Open();
            DateTime? DateS = dates;
            DateTime? DateE = datee;
            string sql = @"select  c.time
                            ,c.targetYield
                            ,c.nowYield
                            from craft_probably c
                            where c.nowYield <> 0 ";

            string w1 = DateS.HasValue ? "and c.time >= '" + DateS.ToString() + "'" : "";
            string w2 = DateE.HasValue ? " and c.time < '" + DateE.Value.AddDays(1.0).ToString() + "'" : "";
            string w3 = " order by time";
            MySqlDataAdapter b = new MySqlDataAdapter(sql +w1 +w2 + w3, connatl);
            DataTable bt = new DataTable();
            b.Fill(bt);
            connatl.Close();

            List<DataPicItem> lstDataCapacityModel = new List<DataPicItem>();

            if (bt != null && bt.Rows.Count > 0)
            {
                foreach (DataRow row in bt.Rows)
                {
                    DataPicItem info = new DataPicItem();
                    info.ProductTime = DateTime.Parse(row[0].ToString());
                    info.CapacityPage = (int)(float.Parse(row[2].ToString()) / float.Parse(row[1].ToString()) * 100);

                    lstDataCapacityModel.Add(info);
                }
            }
            connatl.Close();
            return lstDataCapacityModel;
        }

        /// <summary>
        /// 生产优率
        /// </summary>
        /// <param name="TimeStart"></param>
        /// <param name="TimeEnd"></param>
        /// <returns></returns>
        public IList<DataPicItem> GetDataQuality(DateTime? TimeStart, DateTime? TimeEnd, DateTime dates, DateTime datee)
        {
            string connectionString_ATL = @"server=127.0.0.1;database=IFactory;uid=root;pwd=root;Persist Security Info=True;Charset=utf8;";
            MySqlConnection connatl = new MySqlConnection(connectionString_ATL);
            connatl.Open();
            DateTime? DateS = dates;
            DateTime? DateE = datee;
            string sql = @"select  c.time
                            ,c.OKCount
                            ,c.nowYield
                            from craft_probably c
                            where c.nowYield <> 0
                            and c.OKCount <> 0 ";

            string w1 = DateS.HasValue ? "and c.time >= '" + DateS.ToString() + "'" : "";
            string w2 = DateE.HasValue ? " and c.time < '" + DateE.Value.AddDays(1.0).ToString() + "'" : "";
            string w3 = " order by time";
            MySqlDataAdapter b = new MySqlDataAdapter(sql + w1 + w2 + w3, connatl);
            DataTable bt = new DataTable();
            b.Fill(bt);
            connatl.Close();

            List<DataPicItem> lstAlarmCraftTopModel = new List<DataPicItem>();

            if (bt != null && bt.Rows.Count > 0)
            {
                foreach (DataRow row in bt.Rows)
                {
                    DataPicItem info = new DataPicItem();
                    info.ProductTime = DateTime.Parse(row[0].ToString());
                    info.OK = int.Parse(row[1].ToString());
                    info.total = int.Parse(row[2].ToString());
                    info.flo = double.Parse(row[1].ToString()) / double.Parse(row[2].ToString()) * 100;

                    lstAlarmCraftTopModel.Add(info);
                }
            }
            connatl.Close();
            return lstAlarmCraftTopModel;
        }


        /// <summary>
        /// 生产质量2
        /// </summary>
        /// <param name="TimeStart"></param>
        /// <param name="TimeEnd"></param>
        /// <returns></returns>
        public IList<DataPicItem> GetDataQuality2(DateTime? TimeStart, DateTime? TimeEnd, DateTime dates, DateTime datee)
        {
            string connectionString_ATL = @"server=127.0.0.1;database=IFactory;uid=root;pwd=root;Persist Security Info=True;Charset=utf8;";
            MySqlConnection connatl = new MySqlConnection(connectionString_ATL);
            connatl.Open();
            DateTime? DateS = dates;
            DateTime? DateE = datee;
            string sql = @"select  c.time
                            ,c.OKCount
                            ,c.nowYield
                            from craft_probably c
                            where c.nowYield <> 0
                            and c.OKCount <> 0 ";

            string w1 = DateS.HasValue ? "and c.time >= '" + DateS.ToString() + "'" : "";
            string w2 = DateE.HasValue ? " and c.time < '" + DateE.Value.AddDays(1.0).ToString() + "'" : "";
            string w3 = " order by time";
            MySqlDataAdapter b = new MySqlDataAdapter(sql + w1 + w2 + w3, connatl);
            DataTable bt = new DataTable();
            b.Fill(bt);
            connatl.Close();
            //string w1 = TimeStart.HasValue ? "and alarm_time >= '" + TimeStart.ToString() + "'" : "";
            //string w2 = TimeEnd.HasValue ? " and alarm_time < '" + TimeEnd.Value.AddDays(1.0).ToString() + "'" : "";
            //string w3 = "group by r.craft_did order by count(*) desc limit 10";
            List<DataPicItem> lstAlarmCraftTopModel = new List<DataPicItem>();

            if (bt != null && bt.Rows.Count > 0)
            {
                foreach (DataRow row in bt.Rows)
                {
                    DataPicItem info = new DataPicItem();
                    info.ProductTime = DateTime.Parse(row[0].ToString());
                    info.OK = int.Parse(row[1].ToString());
                    info.total = int.Parse(row[2].ToString());
                    info.flo = double.Parse(row[1].ToString()) / double.Parse(row[2].ToString()) * 100;

                    lstAlarmCraftTopModel.Add(info);
                }
            }
            connatl.Close();
            return lstAlarmCraftTopModel;
        }

        //平均测量值
        public IList<AVEItem> GetAVE(DateTime? TimeStart, DateTime? TimeEnd,String side, DateTime? datee,string type)
        {
            string connectionString_ATL = @"server=127.0.0.1;database=IFactory;uid=root;pwd=root;Persist Security Info=True;Charset=utf8;";
            MySqlConnection connatl = new MySqlConnection(connectionString_ATL);
            connatl.Open();
            String filename = side;
            string battype = type;
            DateTime? DateE = datee;
            
            string sql = "select a.StartDate," + @filename + " from autoinspection1_facility_production_data a" +
                 " where  a.StartDate >= '" + DateE.Value.ToString() + "' and a.StartDate <= '" 
                 + DateE.Value.AddDays(1.0).ToString() + "' and "+ @filename + " <> 0 and a.ProductNo = '" + @battype + "'";

            MySqlDataAdapter b = new MySqlDataAdapter(sql, connatl); 
            DataTable bt = new DataTable();
            b.Fill(bt);
            connatl.Close();

            List<AVEItem> lstDataCapacityModel = new List<AVEItem>();

            if (bt != null && bt.Rows.Count > 0)
            {
                foreach (DataRow row in bt.Rows)
                {
                    AVEItem info = new AVEItem();
                    info.TimeStart = DateTime.Parse(row[0].ToString());
                    info.Size = Double.Parse(row[1].ToString());

                    lstDataCapacityModel.Add(info);
                }
            }
            connatl.Close();
            return lstDataCapacityModel;
        }

        //平均测量值2
        public IList<AVEItem> GetAVE2(DateTime? TimeStart, DateTime? TimeEnd, String side, DateTime? datee, string type)
        {
            string connectionString_ATL = @"server=127.0.0.1;database=IFactory;uid=root;pwd=root;Persist Security Info=True;Charset=utf8;";
            MySqlConnection connatl = new MySqlConnection(connectionString_ATL);
            connatl.Open();
            String filename = side;
            string battype = type;
            DateTime? DateE = datee;

            string sql = "select a.StartDate," + @filename + " from autoinspection2_facility_production_data a" +
                 " where a.StartDate >= '" + DateE.Value.ToString() + "' and a.StartDate <= '"
                 + DateE.Value.AddDays(1.0).ToString() + "' and " + @filename + " <> 0 and a.ProductNo = '" + @battype + "'";

            MySqlDataAdapter b = new MySqlDataAdapter(sql, connatl);
            DataTable bt = new DataTable();
            b.Fill(bt);
            connatl.Close();

            List<AVEItem> lstDataCapacityModel = new List<AVEItem>();

            if (bt != null && bt.Rows.Count > 0)
            {
                foreach (DataRow row in bt.Rows)
                {
                    AVEItem info = new AVEItem();
                    info.TimeStart = DateTime.Parse(row[0].ToString());
                    info.Size = Double.Parse(row[1].ToString());

                    lstDataCapacityModel.Add(info);
                }
            }
            connatl.Close();
            return lstDataCapacityModel;
        }

        //
        public IList<AVEItem> GetType(DateTime? datee)
        {
            string connectionString_ATL = @"server=127.0.0.1;database=IFactory;uid=root;pwd=root;Persist Security Info=True;Charset=utf8;";
            MySqlConnection connatl = new MySqlConnection(connectionString_ATL);
            connatl.Open();
            DateTime? DateE = datee;

            string sql = "select distinct ProductNo from autoinspection1_facility_production_data a where a.StartDate >= '"
                + DateE.Value.ToString() + "' and a.StartDate <= '" + DateE.Value.AddDays(1.0).ToString() + "'";
                        
            MySqlDataAdapter b = new MySqlDataAdapter(sql, connatl);
            DataTable bt = new DataTable();
            b.Fill(bt);
            connatl.Close();

            List<AVEItem> lstDataCapacityModel = new List<AVEItem>();

            if (bt != null && bt.Rows.Count > 0)
            {
                foreach (DataRow row in bt.Rows)
                {
                    AVEItem info = new AVEItem();
                    info.Type = row[0].ToString();

                    lstDataCapacityModel.Add(info);
                }
            }
            connatl.Close();
            return lstDataCapacityModel;
        }

        //
        public IList<AVEItem> GetAlarm(DateTime? TimeStart, DateTime? TimeEnd, String size)
        {
            string connectionString_ATL = @"server=127.0.0.1;database=IFactory;uid=root;pwd=root;Persist Security Info=True;Charset=utf8;";
            MySqlConnection connatl = new MySqlConnection(connectionString_ATL);
            connatl.Open();

            String filename = size;

            if (filename == "")
            {
                filename = "0X0D0301";
            }
            string sql1 = "select alarm_time from alarm_record where rule_did = " + "'" + @filename + "'";
            MySqlDataAdapter b = new MySqlDataAdapter(sql1, connatl);
            DataTable bt = new DataTable();
            b.Fill(bt);
            connatl.Close();

            List<AVEItem> lstDataCapacityModel = new List<AVEItem>();

            if (bt != null && bt.Rows.Count > 0)
            {
                foreach (DataRow row in bt.Rows)
                {
                    AVEItem info = new AVEItem();
                    info.TimeStart = DateTime.Parse(row[0].ToString());

                    lstDataCapacityModel.Add(info);
                }
            }
            connatl.Close();
            return lstDataCapacityModel;
        }

        /// <summary>
        /// PPM
        /// </summary>
        /// <param name="TimeStart"></param>
        /// <param name="TimeEnd"></param>
        /// <returns></returns>
        public IList<DataPicItem> GetPPM(DateTime? TimeStart, DateTime? TimeEnd, DateTime dates, DateTime datee)
        {
            string connectionString_ATL = @"server=127.0.0.1;database=IFactory;uid=root;pwd=root;Persist Security Info=True;Charset=utf8;";
            MySqlConnection connatl = new MySqlConnection(connectionString_ATL);
            connatl.Open();
            DateTime? DateS = dates;
            DateTime? DateE = datee;
            string sql = @"select  c.time
                            ,c.PPM
                            from craft_probably c
                            where ";

            string w1 = DateS.HasValue ? "c.time >= '" + DateS.ToString() + "'" : "";
            string w2 = DateE.HasValue ? " and c.time < '" + DateE.Value.AddDays(1.0).ToString() + "'" : "";
            string w3 = " order by time";
            MySqlDataAdapter b = new MySqlDataAdapter(sql + w1 + w2 + w3, connatl);
            DataTable bt = new DataTable();
            b.Fill(bt);
            connatl.Close();

            List<DataPicItem> lstDataCapacityModel = new List<DataPicItem>();

            if (bt != null && bt.Rows.Count > 0)
            {
                foreach (DataRow row in bt.Rows)
                {
                    DataPicItem info = new DataPicItem();
                    info.ProductTime = DateTime.Parse(row[0].ToString());
                    info.PPM = int.Parse(row[1].ToString());

                    lstDataCapacityModel.Add(info);
                }
            }
            connatl.Close();
            return lstDataCapacityModel;
        }

        /// <summary>
        /// PPM2
        /// </summary>
        /// <param name="TimeStart"></param>
        /// <param name="TimeEnd"></param>
        /// <returns></returns>
        public IList<DataPicItem> GetPPM2(DateTime? TimeStart, DateTime? TimeEnd, DateTime dates, DateTime datee)
        {
            string connectionString_ATL = @"server=127.0.0.1;database=IFactory;uid=root;pwd=root;Persist Security Info=True;Charset=utf8;";
            MySqlConnection connatl = new MySqlConnection(connectionString_ATL);
            connatl.Open();
            DateTime? DateS = dates;
            DateTime? DateE = datee;
            string sql = @"select  c.time
                            ,c.PPM
                            from craft_probably c
                            where ";

            string w1 = DateS.HasValue ? "c.time >= '" + DateS.ToString() + "'" : "";
            string w2 = DateE.HasValue ? " and c.time < '" + DateE.Value.AddDays(1.0).ToString() + "'" : "";
            string w3 = " order by time";
            MySqlDataAdapter b = new MySqlDataAdapter(sql +w1+w2+w3, connatl);
            DataTable bt = new DataTable();
            b.Fill(bt);
            connatl.Close();

            List<DataPicItem> lstDataCapacityModel = new List<DataPicItem>();

            if (bt != null && bt.Rows.Count > 0)
            {
                foreach (DataRow row in bt.Rows)
                {
                    DataPicItem info = new DataPicItem();
                    info.ProductTime = DateTime.Parse(row[0].ToString());
                    info.PPM = int.Parse(row[1].ToString());

                    lstDataCapacityModel.Add(info);
                }
            }
            connatl.Close();
            return lstDataCapacityModel;
        }

        //工作时
        public IList<DataPicItem> GetWorkhours()
        {
            string connectionString_ATL = @"server=127.0.0.1;database=IFactory;uid=root;pwd=root;Persist Security Info=True;Charset=utf8;";
            MySqlConnection connatl = new MySqlConnection(connectionString_ATL);
            connatl.Open();
            string sql = @"select  StartDate
                            from autoinspection1_facility_production_data
                            where year(StartDate) = year(Now())
                            order by StartDate";
            MySqlDataAdapter b = new MySqlDataAdapter(sql, connatl);
            DataTable bt = new DataTable();
            b.Fill(bt);
            connatl.Close();

            List<DataPicItem> lstDataCapacityModel = new List<DataPicItem>();

            if (bt != null && bt.Rows.Count > 0)
            {
                foreach (DataRow row in bt.Rows)
                {
                    DataPicItem info = new DataPicItem();
                    info.ProductTime = DateTime.Parse(row[0].ToString());

                    lstDataCapacityModel.Add(info);
                }
            }
            connatl.Close();
            return lstDataCapacityModel;
        }

        //工作时2
        public IList<DataPicItem> GetWorkhours2()
        {
            string connectionString_ATL = @"server=127.0.0.1;database=IFactory;uid=root;pwd=root;Persist Security Info=True;Charset=utf8;";
            MySqlConnection connatl = new MySqlConnection(connectionString_ATL);
            connatl.Open();
            string sql = @"select  StartDate
                            from autoinspection2_facility_production_data
                            where year(StartDate) = year(Now())
                            order by StartDate";
            MySqlDataAdapter b = new MySqlDataAdapter(sql, connatl);
            DataTable bt = new DataTable();
            b.Fill(bt);
            connatl.Close();

            List<DataPicItem> lstDataCapacityModel = new List<DataPicItem>();

            if (bt != null && bt.Rows.Count > 0)
            {
                foreach (DataRow row in bt.Rows)
                {
                    DataPicItem info = new DataPicItem();
                    info.ProductTime = DateTime.Parse(row[0].ToString());

                    lstDataCapacityModel.Add(info);
                }
            }
            connatl.Close();
            return lstDataCapacityModel;
        }

        /// <summary>
        /// 良品数
        /// </summary>
        /// <param name="TimeStart"></param>
        /// <param name="TimeEnd"></param>
        /// <returns></returns>
        public IList<DataPicItem> GetDataOK(DateTime? TimeStart, DateTime? TimeEnd, DateTime dates, DateTime datee)
        {
            //string connectionString_ATL = @"server=127.0.0.1;database=IFactory;uid=root;pwd=123456;Persist Security Info=True;Charset=utf8;";
            string connectionString_ATL = @"server=127.0.0.1;database=IFactory;uid=root;pwd=root;Persist Security Info=True;Charset=utf8;";
            MySqlConnection connatl = new MySqlConnection(connectionString_ATL);
            connatl.Open();

            DateTime? DateS = dates;
            DateTime? DateE = datee;
            string sql = @"select  c.time
                            ,c.OKCount
                            from craft_probably c
                            where c.nowYield <> 0 ";

            string w1 = DateS.HasValue ? "and c.time >= '" + DateS.ToString() + "'" : "";
            string w2 = DateE.HasValue ? " and c.time < '" + DateE.Value.AddDays(1.0).ToString() + "'" : "";
            string w3 = " order by time";
            MySqlDataAdapter b = new MySqlDataAdapter(sql + w1 + w2 + w3, connatl);
            DataTable bt = new DataTable();
            b.Fill(bt);
            connatl.Close();


            List<DataPicItem> lstDataCapacityModel = new List<DataPicItem>();

            if (bt != null && bt.Rows.Count > 0)
            {
                foreach (DataRow row in bt.Rows)
                {
                    DataPicItem info = new DataPicItem();
                    info.ProductTime = DateTime.Parse(row[0].ToString());
                    info.OK = int.Parse(row[1].ToString());

                    lstDataCapacityModel.Add(info);
                }
            }
            connatl.Close();
            return lstDataCapacityModel;
        }

        /// <summary>
        /// 良品数2
        /// </summary>
        /// <param name="TimeStart"></param>
        /// <param name="TimeEnd"></param>
        /// <returns></returns>
        public IList<DataPicItem> GetDataOK2(DateTime? TimeStart, DateTime? TimeEnd, DateTime dates, DateTime datee)
        {
            //string connectionString_ATL = @"server=127.0.0.1;database=IFactory;uid=root;pwd=123456;Persist Security Info=True;Charset=utf8;";
            string connectionString_ATL = @"server=127.0.0.1;database=IFactory;uid=root;pwd=root;Persist Security Info=True;Charset=utf8;";
            MySqlConnection connatl = new MySqlConnection(connectionString_ATL);
            connatl.Open();

            DateTime? DateS = dates;
            DateTime? DateE = datee;
            string sql = @"select  c.time
                            ,c.OKCount
                            from craft_probably c
                            where c.nowYield <> 0 ";

            string w1 = DateS.HasValue ? "and c.time >= '" + DateS.ToString() + "'" : "";
            string w2 = DateE.HasValue ? " and c.time < '" + DateE.Value.AddDays(1.0).ToString() + "'" : "";
            string w3 = " order by time";
            MySqlDataAdapter b = new MySqlDataAdapter(sql + w1 + w2 + w3, connatl);
            DataTable bt = new DataTable();
            b.Fill(bt);
            connatl.Close();


            List<DataPicItem> lstDataCapacityModel = new List<DataPicItem>();

            if (bt != null && bt.Rows.Count > 0)
            {
                foreach (DataRow row in bt.Rows)
                {
                    DataPicItem info = new DataPicItem();
                    info.ProductTime = DateTime.Parse(row[0].ToString());
                    info.OK = int.Parse(row[1].ToString());

                    lstDataCapacityModel.Add(info);
                }
            }
            connatl.Close();
            return lstDataCapacityModel;
        }

        /// <summary>
        /// 报警统计
        /// </summary>
        /// <param name="TimeStart"></param>
        /// <param name="TimeEnd"></param>
        /// <returns></returns>
        public IList<DataPicItem> GetDataAlarm(DateTime? TimeStart, DateTime? TimeEnd, DateTime dates, DateTime datee)
        {
            //string connectionString_ATL = @"server=127.0.0.1;database=IFactory;uid=root;pwd=123456;Persist Security Info=True;Charset=utf8;";
            string connectionString_ATL = @"server=127.0.0.1;database=IFactory;uid=root;pwd=root;Persist Security Info=True;Charset=utf8;";
            MySqlConnection connatl = new MySqlConnection(connectionString_ATL);
            connatl.Open();

            DateTime? DateS = dates;
            DateTime? DateE = datee;
            string sql = @"select ru.alarm_content,
	                       count(0) as count
                           from alarm_temporary re
                           join alarm_rule ru ON re.rule_did = ru.rule_did
                           where ru.craft_did = 4 ";

            string w1 = DateS.HasValue ? "and re.alarm_time >= '" + DateS.ToString() + "'" : "";
            string w2 = DateE.HasValue ? " and re.alarm_time < '" + DateE.Value.AddDays(1.0).ToString() + "'" : "";
            string w3 = " group by ru.alarm_content order by count DESC";
            MySqlDataAdapter b = new MySqlDataAdapter(sql +w1 +w2 +w3, connatl);
            DataTable bt = new DataTable();
            b.Fill(bt);
            connatl.Close();


            List<DataPicItem> lstDataCapacityModel = new List<DataPicItem>();

            if (bt != null && bt.Rows.Count > 0)
            {
                foreach (DataRow row in bt.Rows)
                {
                    DataPicItem info = new DataPicItem();
                    info.Keyword = row[0].ToString();
                    info.Count = int.Parse(row[1].ToString());

                    lstDataCapacityModel.Add(info);
                }
            }
            connatl.Close();
            return lstDataCapacityModel;
        }

        /// <summary>
        /// 报警统计2
        /// </summary>
        /// <param name="TimeStart"></param>
        /// <param name="TimeEnd"></param>
        /// <returns></returns>
        public IList<DataPicItem> GetDataAlarm2(DateTime? TimeStart, DateTime? TimeEnd, DateTime dates, DateTime datee)
        {
            //string connectionString_ATL = @"server=127.0.0.1;database=IFactory;uid=root;pwd=123456;Persist Security Info=True;Charset=utf8;";
            string connectionString_ATL = @"server=127.0.0.1;database=IFactory;uid=root;pwd=root;Persist Security Info=True;Charset=utf8;";
            MySqlConnection connatl = new MySqlConnection(connectionString_ATL);
            connatl.Open();

            DateTime? DateS = dates;
            DateTime? DateE = datee;
            string sql = @"select ru.alarm_content,
	                       count(0) as count
                           from alarm_record re
                           join alarm_rule ru ON re.rule_did = ru.rule_did
                           where ru.craft_did = 12 ";

            string w1 = DateS.HasValue ? "and re.alarm_time >= '" + DateS.ToString() + "'" : "";
            string w2 = DateE.HasValue ? " and re.alarm_time < '" + DateE.Value.AddDays(1.0).ToString() + "'" : "";
            string w3 = " group by ru.alarm_content order by count DESC";
            MySqlDataAdapter b = new MySqlDataAdapter(sql +w1 +w2 +w3, connatl);
            DataTable bt = new DataTable();
            b.Fill(bt);
            connatl.Close();


            List<DataPicItem> lstDataCapacityModel = new List<DataPicItem>();

            if (bt != null && bt.Rows.Count > 0)
            {
                foreach (DataRow row in bt.Rows)
                {
                    DataPicItem info = new DataPicItem();
                    info.Keyword = row[0].ToString();
                    info.Count = int.Parse(row[1].ToString());

                    lstDataCapacityModel.Add(info);
                }
            }
            connatl.Close();
            return lstDataCapacityModel;
        }

        /// <summary>
        /// 报警统计A
        /// </summary>
        /// <param name="TimeStart"></param>
        /// <param name="TimeEnd"></param>
        /// <returns></returns>
        public IList<DataPicItem> GetDataAlarmA(DateTime? TimeStart, DateTime? TimeEnd, DateTime dates, DateTime datee)
        {
            //string connectionString_ATL = @"server=127.0.0.1;database=IFactory;uid=root;pwd=123456;Persist Security Info=True;Charset=utf8;";
            string connectionString_ATL = @"server=127.0.0.1;database=IFactory;uid=root;pwd=root;Persist Security Info=True;Charset=utf8;";
            MySqlConnection connatl = new MySqlConnection(connectionString_ATL);
            connatl.Open();

            DateTime? DateS = dates;
            DateTime? DateE = datee;
            string sql = @"select ru.alarm_content,
	                       count(0) as count
                           from alarm_temporary re
                           join alarm_rule ru ON re.rule_did = ru.rule_did
                           where ru.craft_did = 4 group by ru.alarm_content 
                           order by count DESC";

            MySqlDataAdapter b = new MySqlDataAdapter(sql, connatl);
            DataTable bt = new DataTable();
            b.Fill(bt);
            connatl.Close();


            List<DataPicItem> lstDataCapacityModel = new List<DataPicItem>();

            if (bt != null && bt.Rows.Count > 0)
            {
                foreach (DataRow row in bt.Rows)
                {
                    DataPicItem info = new DataPicItem();
                    info.Keyword = row[0].ToString();
                    info.Count = int.Parse(row[1].ToString());

                    lstDataCapacityModel.Add(info);
                }
            }
            connatl.Close();
            return lstDataCapacityModel;
        }

        /// <summary>
        /// 报警统计A2
        /// </summary>
        /// <param name="TimeStart"></param>
        /// <param name="TimeEnd"></param>
        /// <returns></returns>
        public IList<DataPicItem> GetDataAlarmA2(DateTime? TimeStart, DateTime? TimeEnd, DateTime dates, DateTime datee)
        {
            //string connectionString_ATL = @"server=127.0.0.1;database=IFactory;uid=root;pwd=123456;Persist Security Info=True;Charset=utf8;";
            string connectionString_ATL = @"server=127.0.0.1;database=IFactory;uid=root;pwd=root;Persist Security Info=True;Charset=utf8;";
            MySqlConnection connatl = new MySqlConnection(connectionString_ATL);
            connatl.Open();

            DateTime? DateS = dates;
            DateTime? DateE = datee;
            string sql = @"select ru.alarm_content,
	                       count(0) as count
                           from alarm_record re
                           join alarm_rule ru ON re.rule_did = ru.rule_did
                           where ru.craft_did = 12 group by ru.alarm_content 
                           order by count DESC";

            MySqlDataAdapter b = new MySqlDataAdapter(sql, connatl);
            DataTable bt = new DataTable();
            b.Fill(bt);
            connatl.Close();


            List<DataPicItem> lstDataCapacityModel = new List<DataPicItem>();

            if (bt != null && bt.Rows.Count > 0)
            {
                foreach (DataRow row in bt.Rows)
                {
                    DataPicItem info = new DataPicItem();
                    info.Keyword = row[0].ToString();
                    info.Count = int.Parse(row[1].ToString());

                    lstDataCapacityModel.Add(info);
                }
            }
            connatl.Close();
            return lstDataCapacityModel;
        }

        /// <summary>
        /// 坏品数
        /// </summary>
        /// <param name="TimeStart"></param>
        /// <param name="TimeEnd"></param>
        /// <returns></returns>
        public IList<DataPicItem> GetDataNG(DateTime? TimeStart, DateTime? TimeEnd, DateTime dates, DateTime datee)
        {
            //string connectionString_ATL = @"server = 127.0.0.1;database=ifactory;uid=root;pwd=123456;Persist Security Info=True;Charset=utf8;";
            string connectionString_ATL = @"server = 127.0.0.1;database=ifactory;uid=root;pwd=root;Persist Security Info=True;Charset=utf8;";
            MySqlConnection connatl = new MySqlConnection(connectionString_ATL);
            connatl.Open();

            DateTime? DateS = dates;
            DateTime? DateE = datee;
            string sql = @"select  c.time
                            ,c.NGCount
                            from craft_probably c
                            where c.nowYield <> 0 ";

            string w1 = DateS.HasValue ? "and c.time >= '" + DateS.ToString() + "'" : "";
            string w2 = DateE.HasValue ? " and c.time < '" + DateE.Value.AddDays(1.0).ToString() + "'" : "";
            string w3 = " order by time";
            MySqlDataAdapter b = new MySqlDataAdapter(sql + w1 + w2 + w3, connatl);
            DataTable bt = new DataTable();
            b.Fill(bt);
            connatl.Close();


            List<DataPicItem> lstDataCapacityModel = new List<DataPicItem>();

            if (bt != null && bt.Rows.Count > 0)
            {
                foreach (DataRow row in bt.Rows)
                {
                    DataPicItem info = new DataPicItem();
                    info.ProductTime = DateTime.Parse(row[0].ToString());
                    info.OK = int.Parse(row[1].ToString());

                    lstDataCapacityModel.Add(info);
                }
            }
            connatl.Close();
            return lstDataCapacityModel;
        }

        /// <summary>
        /// 坏品数2
        /// </summary>
        /// <param name="TimeStart"></param>
        /// <param name="TimeEnd"></param>
        /// <returns></returns>
        public IList<DataPicItem> GetDataNG2(DateTime? TimeStart, DateTime? TimeEnd, DateTime dates, DateTime datee)
        {
            //string connectionString_ATL = @"server = 127.0.0.1;database=ifactory;uid=root;pwd=123456;Persist Security Info=True;Charset=utf8;";
            string connectionString_ATL = @"server = 127.0.0.1;database=ifactory;uid=root;pwd=root;Persist Security Info=True;Charset=utf8;";
            MySqlConnection connatl = new MySqlConnection(connectionString_ATL);
            connatl.Open();

            DateTime? DateS = dates;
            DateTime? DateE = datee;
            string sql = @"select  c.time
                            ,c.NGCount
                            from craft_probably c
                            where c.nowYield <> 0 ";

            string w1 = DateS.HasValue ? "and c.time >= '" + DateS.ToString() + "'" : "";
            string w2 = DateE.HasValue ? " and c.time < '" + DateE.Value.AddDays(1.0).ToString() + "'" : "";
            string w3 = " order by time";
            MySqlDataAdapter b = new MySqlDataAdapter(sql + w1 + w2 + w3, connatl);
            DataTable bt = new DataTable();
            b.Fill(bt);
            connatl.Close();


            List<DataPicItem> lstDataCapacityModel = new List<DataPicItem>();

            if (bt != null && bt.Rows.Count > 0)
            {
                foreach (DataRow row in bt.Rows)
                {
                    DataPicItem info = new DataPicItem();
                    info.ProductTime = DateTime.Parse(row[0].ToString());
                    info.OK = int.Parse(row[1].ToString());

                    lstDataCapacityModel.Add(info);
                }
            }
            connatl.Close();
            return lstDataCapacityModel;
        }
    }
}
