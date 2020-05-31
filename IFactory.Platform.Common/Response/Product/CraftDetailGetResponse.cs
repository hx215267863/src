using IFactory.Domain.Models;

namespace IFactory.Platform.Common.Response.Product
{
    public class CraftDetailGetResponse : BaseResponse
    {
        public CraftDetailModel CraftDetail { get; set; }
    }
}
