using IFactory.Domain.Crafts.Base.Models;
using IFactory.Domain.Models;

namespace IFactory.Platform.Common.Response.Crafts
{
    public class FacilityProductionDataListResponse : BaseResponse
    {
        public PagedData<FacilityProductionDataModel> FacilityProductionDatas { get; set; }
    }
}
