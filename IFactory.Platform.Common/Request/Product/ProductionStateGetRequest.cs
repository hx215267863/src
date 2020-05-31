using IFactory.Platform.Common.Response.Product;

namespace IFactory.Platform.Common.Request.Product
{
    public class ProductionStateGetRequest : BaseRequest<ProductionStateGetResponse>
    {
        public override string ApiName
        {
            get
            {
                return "production.state.get";
            }
        }

        public int ProductionDID { get; set; }
    }
}
