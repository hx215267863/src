using IFactory.Data.Crafts;
using IFactory.Domain.Crafts.Base.Entities;
using PagedList;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace IFactory.Service.Crafts
{
    public class FacilityProductionDataService : BaseCraftService<FacilityProductionDataInfo>, IFacilityProductionDataService, IBaseCraftService<FacilityProductionDataInfo>
    {
        public FacilityProductionDataService(ICraftDbFactory databaseFactory)
          : base(databaseFactory)
        {
        }

        public IPagedList<FacilityProductionDataInfo> GetPagedList(int pageNo, int pageSize)
        {
            return new PagedList<FacilityProductionDataInfo>(this.DataContext.FacilityProductionDataInfos.OrderByDescending(m => m.Iden), pageNo, pageSize);
        }
    }
}
