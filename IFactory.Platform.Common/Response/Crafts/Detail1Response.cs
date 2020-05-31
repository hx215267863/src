using IFactory.Domain.Models;

namespace IFactory.Platform.Common.Response.Crafts
{
    public class Detail1Response : BaseResponse
    {
        public PagedData<Detail1Item> Detail1s { get; set; }
    }
}
