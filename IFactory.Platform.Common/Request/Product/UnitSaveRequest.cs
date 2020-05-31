using IFactory.Platform.Common.Response.Product;

namespace IFactory.Platform.Common.Request.Product
{
    public class UnitSaveRequest : BaseRequest<UnitSaveResponse>
    {
        public override string ApiName
        {
            get
            {
                return "unit.save";
            }
        }

        public int DID { get; set; }

        public string NO { get; set; }

        public string Name { get; set; }
    }
}
