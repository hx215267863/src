using IFactory.Platform.Common.Response.SystemParam;

namespace IFactory.Platform.Common.Request.SystemParam
{
    public class SystemParamGetRequest : BaseRequest<SystemParamGetResponse>
    {
        public override string ApiName
        {
            get
            {
                return "systemParam.get";
            }
        }

        public string ITEM_CD { get; set; }
    }
}
