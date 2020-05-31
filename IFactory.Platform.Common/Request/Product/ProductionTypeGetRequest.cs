using IFactory.Platform.Common.Response.Product;

namespace IFactory.Platform.Common.Request.Product
{
    public class ProductionTypeGetRequest : BaseRequest<ProductionTypeGetResponse>
    {
        public override string ApiName
        {
            get
            {
                return "production.type.get";
            }
        }

        public int DID { get; set; }
    }
}
