using IFactory.Data;
using IFactory.Domain.Entities;
using PagedList;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace IFactory.LocalService
{
    public class RoleService : BaseService<RoleInfo>, IRoleService, IBaseService<RoleInfo>
    {
        public RoleService(IDatabaseFactory databaseFactory)
          : base(databaseFactory)
        {
        }

        public IPagedList<RoleInfo> GetPagedRoles(int pageNo, int pageSize)
        {
            return new PagedList<RoleInfo>(this.DataContext.RoleInfos.OrderBy<RoleInfo, int>(m => m.RoleId), pageNo, pageSize);
        }
    }
}
