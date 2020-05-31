using IFactory.Domain.Models;

namespace IFactory.Platform.Common.Response.Product
{
  public class UnitGetResponse : BaseResponse
  {
    public UnitModel Unit { get; set; }
  }
}
