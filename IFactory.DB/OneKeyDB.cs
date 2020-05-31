using System.Collections.Generic;
using System.Data;
using IFactory.Domain.Models;
using MySql.Data.MySqlClient;

namespace IFactory.DB
{
    public class OneKeyDB : BaseFacade
    {
        private DataTable getPagedOneKeyData()
        {
            string connectionString_ATL = @"server=127.0.0.1;database=IFactory;uid=root;pwd=root;Persist Security Info=True;Charset=utf8;";
            MySqlConnection connatl = new MySqlConnection(connectionString_ATL);
            connatl.Open();
            string sql = "";
            sql = string.Format(
                @"select 
                    isready
                    from device_group");
            MySqlDataAdapter b = new MySqlDataAdapter(sql, connatl);
            DataTable bt = new DataTable();
            b.Fill(bt);
            connatl.Close();
            return bt;
        }

        /// <summary>
        /// 获取OneKey
        /// </summary>
        /// <returns></returns>
        public List<OneKeyItem> GetPagedOneKeyData()
        {
            DataTable tb = getPagedOneKeyData();

            List<OneKeyItem> lst = new List<OneKeyItem>();
            if (tb != null && tb.Rows.Count > 0)
            {
                foreach (DataRow row in tb.Rows)
                {
                    OneKeyItem info = new OneKeyItem();
                    info.OneKey_flag = int.Parse(row[0].ToString());

                    lst.Add(info);
                }
            }
            return lst;
        }

        private DataTable getPagedOneKeyData2()
        {
            string connectionString_ATL = @"server=127.0.0.1;database=IFactory;uid=root;pwd=root;Persist Security Info=True;Charset=utf8;";
            MySqlConnection connatl = new MySqlConnection(connectionString_ATL);
            connatl.Open();
            string sql = "";
            sql = string.Format(
                @"select 
                    isready
                    from device_group");
            MySqlDataAdapter b = new MySqlDataAdapter(sql, connatl);
            DataTable bt = new DataTable();
            b.Fill(bt);
            connatl.Close();
            return bt;
        }

        /// <summary>
        /// 获取OneKey2
        /// </summary>
        /// <returns></returns>
        public List<OneKeyItem> GetPagedOneKeyData2()
        {
            DataTable tb = getPagedOneKeyData2();

            List<OneKeyItem> lst = new List<OneKeyItem>();
            if (tb != null && tb.Rows.Count > 0)
            {
                foreach (DataRow row in tb.Rows)
                {
                    OneKeyItem info = new OneKeyItem();
                    info.OneKey_flag = int.Parse(row[0].ToString());

                    lst.Add(info);
                }
            }
            return lst;
        }

        public void IsReady_change(OneKeyItem okItem)
        {
            string connectionString_ATL = @"server=127.0.0.1;database=IFactory;uid=root;pwd=root;Persist Security Info=True;Charset=utf8;";
            MySqlConnection connatl = new MySqlConnection(connectionString_ATL);
            connatl.Open();
            string sql = string.Format(@" update device_group set isready = {0}"
                                        , okItem.OneKey_flag);
            MySqlDataAdapter b = new MySqlDataAdapter(sql, connatl);
            DataTable bt = new DataTable();
            b.Fill(bt);
            connatl.Close();
        }

        public void IsReady_change2(OneKeyItem okItem)
        {
            string connectionString_ATL = @"server=127.0.0.1;database=IFactory;uid=root;pwd=root;Persist Security Info=True;Charset=utf8;";
            MySqlConnection connatl = new MySqlConnection(connectionString_ATL);
            connatl.Open();
            string sql = string.Format(@" update device_group set isready = {0}"
                                        , okItem.OneKey_flag);
            MySqlDataAdapter b = new MySqlDataAdapter(sql, connatl);
            DataTable bt = new DataTable();
            b.Fill(bt);
            connatl.Close();
        }
    }
}