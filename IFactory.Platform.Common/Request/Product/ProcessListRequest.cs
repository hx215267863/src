using IFactory.Platform.Common.Response.Product;

namespace IFactory.Platform.Common.Request.Product
{
    public class ProcessListRequest : BaseRequest<ProcessListResponse>
    {
        public override string ApiName
        {
            get
            {
                return "process.list";
            }
        }

        public int CraftDID { get; set; }
    }
}
