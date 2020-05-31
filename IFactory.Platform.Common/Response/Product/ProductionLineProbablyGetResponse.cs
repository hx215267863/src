using IFactory.Domain.Models;

namespace IFactory.Platform.Common.Response.Product
{
    public class ProductionLineProbablyGetResponse : BaseResponse
    {
        public ProductionLineProbablyModel ProductionLineProbably { get; set; }
    }
}
