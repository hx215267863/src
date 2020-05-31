using IFactory.Domain.Models;

namespace IFactory.Platform.Common.Response.SystemParam
{
    public class SystemParamListResponse : BaseResponse
    {
        public PagedData<SystemParamModel> SystemParams { get; set; }
    }
}
