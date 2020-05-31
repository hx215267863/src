using IFactory.Domain.Models;

namespace IFactory.Platform.Common.Response.SystemParam
{
    public class ProductParamGetResponse : BaseResponse
    {
        public ProductParamModel ProductParam { get; set; }
    }
}
