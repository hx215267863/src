using IFactory.Data;
using IFactory.Domain.Entities;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace IFactory.LocalService
{
    public class AppService : BaseService<AppInfo>, IAppService, IBaseService<AppInfo>
    {
        public AppService(IDatabaseFactory databaseFactory)
          : base(databaseFactory)
        {
        }

        public AppInfo GetApp(string appKey)
        {
            return this.Table.Where<AppInfo>(m => m.AppKey == appKey).FirstOrDefault<AppInfo>();
        }
    }
}
