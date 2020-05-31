using IFactory.Domain.Models;

namespace IFactory.Platform.Common.Response.Product
{
    public class ProductNGResponse : BaseResponse
    {
        public PagedData<ProductNGItem> productNGs { get; set; }
    }
}
