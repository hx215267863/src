using IFactory.Platform.Common.Response.SystemParam;

namespace IFactory.Platform.Common.Request.SystemParam
{
    public class SystemParamListRequest : BaseRequest<SystemParamListResponse>
    {
        public override string ApiName
        {
            get
            {
                return "systemParam.list";
            }
        }

        public string ITEM_CD { get; set; }

        public int PageNumber { get; set; }

        public int PageSize { get; set; }
    }
}
