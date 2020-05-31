using IFactory.Domain.Models;

namespace IFactory.Platform.Common.Response.Product
{
  public class ProductionTypeGetResponse : BaseResponse
  {
    public ProductionTypeModel ProductionType { get; set; }
  }
}
