using IFactory.Platform.Common.Response.Product;

namespace IFactory.Platform.Common.Request.Product
{
    public class CraftDetailGetRequest : BaseRequest<CraftDetailGetResponse>
    {
        public override string ApiName
        {
            get
            {
                return "craft.detail.get";
            }
        }

        public int CraftDID { get; set; }
    }
}
