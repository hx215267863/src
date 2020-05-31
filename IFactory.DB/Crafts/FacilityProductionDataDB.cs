using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using IFactory.Domain.Crafts.Base.Entities;
using PagedList;
using IFactory.Common;
using IFactory.Domain.Crafts.Inspection1.Entities;
using IFactory.Domain.Crafts.Inspection2.Entities;

namespace IFactory.DB
{
    public class FacilityProductionDataDB : BaseFacade
    {
        public static bool logCell = true;
        
        public IPagedList<FacilityProductionDataInfo> GetPagedList(string CraftNO, int pageNo, int pageSize)
        {
            DataTable dt;
           
            if (CommonHelper.GetCraftShortNO(CraftNO) == "IN1")
            {
                List<Inspection1FacilityProductionDataInfo> lst = new List<Inspection1FacilityProductionDataInfo>();
                dt = get_mib_facility_production_data();
                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        Inspection1FacilityProductionDataInfo info = new Inspection1FacilityProductionDataInfo();
                        info.Iden = int.Parse(row[0].ToString());
                        info.BatteryBarCode = row[2].ToString();
                        info.DeviceGroupDID = int.Parse(row[3].ToString());
                        info.No = int.Parse(row[6].ToString());
                        if (row[7].ToString() != "")
                        {
                            info.StartDate = DateTime.Parse(row[7].ToString());
                        }
                        info.ProductNo = row[1].ToString();
                        //info.TabBarCode = row[6].ToString();
                        //info.Operator = row[7].ToString();
                        if (row[8].ToString() != "")
                        {
                            //info.InTime = DateTime.Parse(row[8].ToString());
                        }
                        if (row[9].ToString() != "")
                        {
                            //info.OutTime = DateTime.Parse(row[9].ToString());
                        }
                        //info.BoxNum = int.Parse(row[10].ToString());
                        //info.Floor = int.Parse(row[11].ToString());
                        //info.Location = int.Parse(row[12].ToString());
                        //info.TempratureIndex = int.Parse(row[13].ToString());
                        if (row[14].ToString() != "")
                        {
                           // info.Temprature1 = float.Parse(row[14].ToString());
                        }
                        if (row[15].ToString() != "")
                        {
                          //  info.Temprature2 = float.Parse(row[15].ToString());
                        }
                        if (row[16].ToString() != "")
                        {
                           // info.Vacuum = float.Parse(row[16].ToString());
                        }
                        //info.UserId = int.Parse(row[17].ToString());

                        lst.Add(info);
                    }
                }
                return new PagedList<Inspection1FacilityProductionDataInfo>(lst.OrderByDescending(m => m.Iden), pageNo, pageSize);
            }
            if (CommonHelper.GetCraftShortNO(CraftNO) == "IN2")
            {
                List<Inspection2FacilityProductionDataInfo> lst = new List<Inspection2FacilityProductionDataInfo>();
                dt = get_mib_facility_production_data();
                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        Inspection2FacilityProductionDataInfo info = new Inspection2FacilityProductionDataInfo();
                        info.Iden = int.Parse(row[0].ToString());
                        info.BatteryBarCode = row[1].ToString();
                        info.DeviceGroupDID = int.Parse(row[2].ToString());
                        info.No = int.Parse(row[3].ToString());
                        if (row[4].ToString() != "")
                        {
                            info.StartDate = DateTime.Parse(row[4].ToString());
                        }
                        info.ProductNo = row[5].ToString();
                        info.TabBarCode = row[6].ToString();
                        //info.Operator = row[7].ToString();
                        if (row[8].ToString() != "")
                        {
                            //info.InTime = DateTime.Parse(row[8].ToString());
                        }
                        if (row[9].ToString() != "")
                        {
                            //info.OutTime = DateTime.Parse(row[9].ToString());
                        }
                        //info.BoxNum = int.Parse(row[10].ToString());
                        //info.Floor = int.Parse(row[11].ToString());
                        //info.Location = int.Parse(row[12].ToString());
                        //info.TempratureIndex = int.Parse(row[13].ToString());
                        if (row[14].ToString() != "")
                        {
                            // info.Temprature1 = float.Parse(row[14].ToString());
                        }
                        if (row[15].ToString() != "")
                        {
                            //  info.Temprature2 = float.Parse(row[15].ToString());
                        }
                        if (row[16].ToString() != "")
                        {
                            // info.Vacuum = float.Parse(row[16].ToString());
                        }
                        //info.UserId = int.Parse(row[17].ToString());

                        lst.Add(info);
                    }
                }
                return new PagedList<Inspection2FacilityProductionDataInfo>(lst.OrderByDescending(m => m.Iden), pageNo, pageSize);
            }
            else
            {
                throw new Exception("无效的CraftNO");
            }
        }

        private DataTable get_mib_facility_production_data()
        {
            Database equipDB = dataProvider.EQUIPDataBase;
            string sql = "select * from autoinspection1_facility_production_data;";
            DataSet ds = equipDB.ExecuteDataSet(CommandType.Text, sql);
            return ds.Tables[0];
        }

        private DataTable get_fef_facility_production_data()
        {
            Database equipDB = dataProvider.EQUIPDataBase;
            string sql = "select * from fef_facility_production_data;";
            DataSet ds = equipDB.ExecuteDataSet(CommandType.Text, sql);
            return ds.Tables[0];
        }
    }
}
