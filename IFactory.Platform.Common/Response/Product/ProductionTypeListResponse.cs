using IFactory.Domain.Models;
using System.Collections.Generic;

namespace IFactory.Platform.Common.Response.Product
{
  public class ProductionTypeListResponse : BaseResponse
  {
    public IList<ProductionTypeModel> ProductionTypes { get; set; }
  }
}
