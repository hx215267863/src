using IFactory.Domain.Models;
using IFactory.Platform.Common.Response.Product;

namespace IFactory.Platform.Common.Request.Product
{
    public class CraftDetailSaveRequest : BaseRequest<CraftDetailSaveResponse>
    {
        public override string ApiName
        {
            get
            {
                return "craft.detail.save";
            }
        }

        public CraftDetailModel CraftDetail { get; set; }
    }
}
