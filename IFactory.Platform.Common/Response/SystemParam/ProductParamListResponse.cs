using IFactory.Domain.Models;

namespace IFactory.Platform.Common.Response.SystemParam
{
    public class ProductParamListResponse : BaseResponse
    {
        public PagedData<ProductParamModel> ProductParams { get; set; }
    }
}
