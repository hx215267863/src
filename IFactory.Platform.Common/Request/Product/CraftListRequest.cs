using IFactory.Platform.Common.Response.Product;

namespace IFactory.Platform.Common.Request.Product
{
    public class CraftListRequest : BaseRequest<CraftListResponse>
    {
        public override string ApiName
        {
            get
            {
                return "craft.list";
            }
        }
    }
}
