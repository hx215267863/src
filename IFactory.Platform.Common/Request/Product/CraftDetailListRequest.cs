using IFactory.Platform.Common.Response.Product;

namespace IFactory.Platform.Common.Request.Product
{
    public class CraftDetailListRequest : BaseRequest<CraftDetailListResponse>
    {
        public override string ApiName
        {
            get
            {
                return "craft.detail.list";
            }
        }
    }
}
