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
    public class AlarmInfoDB : BaseFacade
    {

        public AlarmInfoDB() {


        }

        public DataTable getR_MCH_ALARM_DTL_T()
        {
            string connectionString_ATL = @"server=127.0.0.1;database=IFactory;uid=root;pwd=root;Persist Security Info=True;Charset=utf8;";
            MySqlConnection connatl = new MySqlConnection(connectionString_ATL);
            connatl.Open();
            string sql = "select * from R_MCH_ALARM_DTL_T;";
            MySqlDataAdapter b = new MySqlDataAdapter(sql, connatl);
            DataTable bt = new DataTable();
            b.Fill(bt);
            connatl.Close();
            return bt;
        }

        public IPagedList<AlarmInfoModel> GetR_MCH_ALARM_DTL_T(string keyword, DateTime? alarmDateStart, DateTime? alarmDateEnd, int pageNo, int pageSize, int CraftDID)
        {
            DataTable tb = getR_MCH_ALARM_DTL_T();

            List<AlarmInfoModel> list = new List<AlarmInfoModel>();
            if (tb != null && tb.Rows.Count > 0)
            {
                foreach (DataRow row in tb.Rows)
                {
                    AlarmInfoModel info = new AlarmInfoModel();
                    info.ALARM_ID = row[0].ToString();
                    info.MCH_CD = row[1].ToString();
                    info.MCH_NM = row[2].ToString();
                    info.MCH_ST = row[3].ToString();
                    info.ALARM_TY = row[4].ToString();
                    info.ALARM_INFO = row[5].ToString();
                    info.ITEM_CD = row[6].ToString();
                    info.MODEL_CD = row[7].ToString();
                    info.SN_NO = row[8].ToString();
                    info.ALARM_PART = row[9].ToString();
                    info.ALARM_CRAFT = row[10].ToString();
                    info.OPER_CD = row[11].ToString();

                    info.HAND_ST = row[12].ToString();
                    info.MO = row[13].ToString();

                    info.CRT_ID = row[14].ToString();
                    if (row[4].ToString() != "")
                   info.CRT_DT = DateTime.Parse(row[15].ToString());
                   

                    list.Add(info);
                }
            }
           IQueryable<AlarmInfoModel> superset = list.AsQueryable();
            return new PagedList<AlarmInfoModel>(superset, pageNo, pageSize);
        }
  }
}
