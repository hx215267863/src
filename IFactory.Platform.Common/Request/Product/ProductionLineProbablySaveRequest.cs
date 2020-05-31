using IFactory.Platform.Common.Response.Product;

namespace IFactory.Platform.Common.Request.Product
{
    public class ProductionLineProbablySaveRequest : BaseRequest<ProductionLineProbablySaveResponse>
    {
        public override string ApiName
        {
            get
            {
                return "production.line.probably.save";
            }
        }

        public int DID { get; set; }

        public string Name { get; set; }

        public string TargetYield { get; set; }
    }
}
