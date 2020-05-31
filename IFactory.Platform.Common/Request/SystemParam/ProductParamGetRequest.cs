using IFactory.Platform.Common.Response.SystemParam;

namespace IFactory.Platform.Common.Request.SystemParam
{
    public class ProductParamGetRequest : BaseRequest<ProductParamGetResponse>
    {
        public override string ApiName
        {
            get
            {
                return "productParam.get";
            }
        }

        public string ITEM_CD { get; set; }
    }
}
