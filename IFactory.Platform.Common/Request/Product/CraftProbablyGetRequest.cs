using IFactory.Platform.Common.Response.Product;

namespace IFactory.Platform.Common.Request.Product
{
    public class CraftProbablyGetRequest : BaseRequest<CraftProbablyGetResponse>
    {
        public override string ApiName
        {
            get
            {
                return "craft.probably.get";
            }
        }

        public int CraftDID { get; set; }

        public string code { get; set; }
    }
}
