using IFactory.Domain.Crafts.Base.Entities;
using PagedList;

namespace IFactory.LocalService.Crafts
{
    public interface IFacilityProductionDataService : IBaseCraftService<FacilityProductionDataInfo>
    {
        IPagedList<FacilityProductionDataInfo> GetPagedList(int pageNo, int pageSize);
    }
}
