using IFactory.Domain.Crafts.Base.Entities;
using PagedList;

namespace IFactory.Service.Crafts
{
    public interface IFacilityProductionDataService : IBaseCraftService<FacilityProductionDataInfo>
    {
        IPagedList<FacilityProductionDataInfo> GetPagedList(int pageNo, int pageSize);
    }
}
