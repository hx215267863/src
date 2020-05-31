using IFactory.Platform.Common.Response.SystemParam;

namespace IFactory.Platform.Common.Request.SystemParam
{
    public class ProductParamDeleteRequest : BaseRequest<ProductParamDeleteResponse>
    {
        public string ITEM_CD;

        public override string ApiName
        {
            get
            {
                return "productParam.delete";
            }
        }
    }
}
