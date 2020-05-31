using IFactory.Domain.Models;
using System.Collections.Generic;

namespace IFactory.Platform.Common.Response.Product
{
  public class UnitListResponse : BaseResponse
  {
    public IList<UnitModel> Units { get; set; }
  }
}
