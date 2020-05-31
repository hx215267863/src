using IFactory.Platform.Common.Response.Product;

namespace IFactory.Platform.Common.Request.Product
{
    public class UnitGetRequest : BaseRequest<UnitGetResponse>
    {
        public override string ApiName
        {
            get
            {
                return "unit.get";
            }
        }

        public int UnitDID { get; set; }
    }
}
