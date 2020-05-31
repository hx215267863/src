using IFactory.Platform.Common.Response.Product;

namespace IFactory.Platform.Common.Request.Product
{
    public class UnitListRequest : BaseRequest<UnitListResponse>
    {
        public override string ApiName
        {
            get
            {
                return "unit.list";
            }
        }
    }
}
