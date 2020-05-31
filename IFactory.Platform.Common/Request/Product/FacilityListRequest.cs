using IFactory.Platform.Common.Response.Product;

namespace IFactory.Platform.Common.Request.Product
{
    public class FacilityListRequest : BaseRequest<FacilityListResponse>
    {
        public override string ApiName
        {
            get
            {
                return "facility.list";
            }
        }

        public int CraftDID { get; set; }
    }
}
