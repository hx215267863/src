using IFactory.Platform.Common.Response.Product;

namespace IFactory.Platform.Common.Request.Product
{
    public class UnitDeleteRequest : BaseRequest<UnitDeleteResponse>
    {
        public override string ApiName
        {
            get
            {
                return "unit.delete";
            }
        }

        public int UnitDID { get; set; }
    }
}
