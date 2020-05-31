using IFactory.Platform.Common.Response.Product;

namespace IFactory.Platform.Common.Request.Product
{
    public class PLCStateListRequest : BaseRequest<PLCStateListResponse>
    {
        public override string ApiName
        {
            get
            {
                return "plc.state.list";
            }
        }

        public int CraftDID { get; set; }
    }
}
