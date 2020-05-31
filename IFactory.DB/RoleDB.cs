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
using IFactory.Data;
using PagedList;

namespace IFactory.DB
{
    public class RoleDB : BaseFacade
    {
        public static bool logCell = true;

        private DataTable RolesTableQuery()
        {
            Database equipDB = dataProvider.EQUIPDataBase;
            string sql = "select * from roles order by roleId";
            DataSet ds = equipDB.ExecuteDataSet(CommandType.Text, sql);
            return ds.Tables[0];
        }

        public IPagedList<RoleInfo> GetPagedRoles(int pageNo, int pageSize)
        {
            List<RoleInfo> lst = new List<RoleInfo>();
            DataTable tb = RolesTableQuery();
            if (tb != null && tb.Rows.Count > 0)
            {
                foreach (DataRow row in tb.Rows)
                {
                    RoleInfo info = new RoleInfo();
                    info.RoleId = int.Parse(row[0].ToString());
                    info.RoleName = row[1].ToString();
                    info.PermissionCodes = row[2].ToString();
                    info.CreateTime = DateTime.Parse(row[3].ToString());
                    info.Remark = row[4].ToString();
                    lst.Add(info);
                }
            }
            return new PagedList<RoleInfo>(lst.OrderBy<RoleInfo, int>(m => m.RoleId), pageNo, pageSize);
        }
    }
}
