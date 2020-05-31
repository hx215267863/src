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

namespace IFactory.DB
{
    public class FacilityRunArgDB : BaseFacade
    {
        public static bool logCell = true;

        private DataTable Get_MIB_FacilityRunArgDateTimes(int[] facilityIds, DateTime startTime, DateTime endTime)
        {
            //Database equipDB = dataProvider.EQUIPDataBase;
            //string sql = string.Format(
            //    @"select * from production_line_probably t where ( t.did = {0});"
            //                            , did);
            //DataSet ds = equipDB.ExecuteDataSet(CommandType.Text, sql);
            //return ds.Tables[0];
            return null;
        }
        public List<DateTime> GetFacilityRunArgDateTimes(string CraftNO, int[] facilityIds, DateTime startTime, DateTime endTime)
        {
            //return this.DataContext.FacilityRunArgInfos.Where(arg => facilityIds.Contains<int>(arg.FacilityDID) && arg.MCCollectDDate >= startTime && arg.MCCollectDDate < endTime).Select(arg => arg.MCCollectDDate).Distinct<DateTime>().ToList<DateTime>();
            List<DateTime> lst = new List<DateTime>();
            return lst;
        }
    }
}
