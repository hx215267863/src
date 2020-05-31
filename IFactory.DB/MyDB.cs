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
    public class MyDB : BaseFacade
    {
        public void InsertAlarm(string AlarmContent)
        {            
            string Time = DateTime.Now.ToString();
            Random rand = new Random();
            int duration = rand.Next(0, 3);
            string disTime = DateTime.Now.AddMinutes(duration).ToString();
            Database equipDB = dataProvider.EQUIPDataBase;
            string sql = string.Format(@"insert alarm_table set 报警内容 = '{0}', 报警时间 = '{1}', dispose_time = '{2}', duration = {3};"
                                            , AlarmContent, Time, disTime, duration);
            equipDB.ExecuteNonQuery(CommandType.Text, sql);
        }

        public void InsertCapacityOfProduction(int Capacity, int ok, double rate)
        {
            string Time = DateTime.Now.ToString("yyyy-MM-dd HH");
            string strRate = rate.ToString("0.0") + "%";
            string Num = DateTime.Now.ToString("yyyyMMddHH");
            Database equipDB = dataProvider.EQUIPDataBase;
            string sql = string.Format(@"insert into capacityofproduction (编号, 时间, 产能, 良品数, 良品率) values ('{4}', '{0}', {1}, {2}, '{3}')
                                            on duplicate key update 产能 = {1}, 良品数 = {2}, 良品率 = '{3}', 编号 = '{4}';"
                                            , Time, Capacity, ok, strRate, Num);
            equipDB.ExecuteNonQuery(CommandType.Text, sql);
        }

        public void InsertProductionData(string Barcode)
        {
            string Time = DateTime.Now.Date.ToString();
            Database equipDB = dataProvider.EQUIPDataBase;
            string sql = string.Format(@"insert into LoadUnloadProduction (barcode, inputTime) values ('{0}','{1}');"
                                            , Barcode, Time);
            equipDB.ExecuteNonQuery(CommandType.Text, sql);
        }
    }   
}
