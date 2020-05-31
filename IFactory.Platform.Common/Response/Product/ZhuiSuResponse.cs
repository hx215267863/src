using IFactory.Domain.Models;

namespace IFactory.Platform.Common.Response.Product
{
    public class ZhuiSuResponse : BaseResponse
    {
        public PagedData<ZhuiSuItem> ZhuiSus { get; set; }
    }
}
