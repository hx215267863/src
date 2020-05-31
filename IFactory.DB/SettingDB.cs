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
namespace IFactory.DB
{
    public class SettingDB : BaseFacade
    {

        private DataTable getKanbanSetting(int kanbanSettingId)
        {
            Database equipDB = dataProvider.EQUIPDataBase;
            string sql = string.Format(
                @"select * from kanban_settings t where
                                                (t.KanbanSettingId = '{0}')"
                                        , kanbanSettingId);
            DataSet ds = equipDB.ExecuteDataSet(CommandType.Text, sql);
            return ds.Tables[0];
        }

        public KanbanSettingInfo GetKanbanSetting(int kanbanSettingId)
        {
            DataTable tb = getKanbanSetting(kanbanSettingId);
            
            if (tb != null && tb.Rows.Count > 0)
            {
                KanbanSettingInfo info = new KanbanSettingInfo();
                info.KanbanSettingId = int.Parse(tb.Rows[0][0].ToString());
                info.ProductionReportTimeSection = (TimeSectionType)Enum.Parse(typeof(TimeSectionType), tb.Rows[0][1].ToString());
                info.ExcellentRateReportTimeSection = (TimeSectionType)Enum.Parse(typeof(TimeSectionType), tb.Rows[0][2].ToString());
                info.AlarmReportTimeSection = (TimeSectionType)Enum.Parse(typeof(TimeSectionType), tb.Rows[0][3].ToString());
                info.DateFormat = tb.Rows[0][4].ToString();
                info.TimeFormat = tb.Rows[0][5].ToString();
                info.RefreshInterval = int.Parse(tb.Rows[0][6].ToString());
                return info;
            }
            else
            {
                throw new Exception("执行 getKanbanSetting 查询到空值");
            }
            
        }

        public void SaveKanbanSetting(KanbanSettingInfo kanbanSettingInfo)
        {
            Database equipDB = dataProvider.EQUIPDataBase;
            string sql = string.Format(
                @"update kanban_settings t set
                    t.ProductionReportTimeSection = {1},
                    t.ExcellentRateReportTimeSection = {2},
                    t.AlarmReportTimeSection = {3},
                    t.DateFormat = '{4}',
                    t.TimeFormat = '{5}',
                    t.RefreshInterval = {6}
                    where t.KanbanSettingId = '{0}'"
                 , kanbanSettingInfo.KanbanSettingId
                 , (int)kanbanSettingInfo.ProductionReportTimeSection
                 , (int)kanbanSettingInfo.ExcellentRateReportTimeSection
                 , (int)kanbanSettingInfo.AlarmReportTimeSection
                 , kanbanSettingInfo.DateFormat
                 , kanbanSettingInfo.TimeFormat
                 , kanbanSettingInfo.RefreshInterval
                 );

            equipDB.ExecuteNonQuery(CommandType.Text, sql);
        }
    }
}
