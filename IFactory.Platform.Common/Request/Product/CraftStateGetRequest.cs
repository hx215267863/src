using IFactory.Platform.Common.Response.Product;

namespace IFactory.Platform.Common.Request.Product
{
    public class CraftStateGetRequest : BaseRequest<CraftStateGetResponse>
    {
        public override string ApiName
        {
            get
            {
                return "craft.state.get";
            }
        }

        public int CraftDID { get; set; }
    }
}
