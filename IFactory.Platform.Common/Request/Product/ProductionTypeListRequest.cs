using IFactory.Platform.Common.Response.Product;

namespace IFactory.Platform.Common.Request.Product
{
    public class ProductionTypeListRequest : BaseRequest<ProductionTypeListResponse>
    {
        public override string ApiName
        {
            get
            {
                return "production.type.list";
            }
        }

        public int CraftDID { get; set; }
    }
}
