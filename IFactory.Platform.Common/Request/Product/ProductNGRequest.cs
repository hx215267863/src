using IFactory.Platform.Common.Response.Product;

namespace IFactory.Platform.Common.Request.Product
{
    public class ProductNGRequest : BaseRequest<ProductNGResponse>
    {
        public override string ApiName
        {
            get
            {
                return "Product.NG";
            }
        }

        public int? ProcessDID { get; set; }

        public int PageNumber { get; set; }

        public int PageSize { get; set; }
    }
}
