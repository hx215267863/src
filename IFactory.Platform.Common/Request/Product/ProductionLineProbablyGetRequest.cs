using IFactory.Platform.Common.Response.Product;

namespace IFactory.Platform.Common.Request.Product
{
    public class ProductionLineProbablyGetRequest : BaseRequest<ProductionLineProbablyGetResponse>
    {
        public override string ApiName
        {
            get
            {
                return "production.line.probably.get";
            }
        }

        public int DID { get; set; }
    }
}
