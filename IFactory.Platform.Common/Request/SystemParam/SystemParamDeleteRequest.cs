using IFactory.Platform.Common.Response.SystemParam;

namespace IFactory.Platform.Common.Request.SystemParam
{
    public class SystemParamDeleteRequest : BaseRequest<ProductParamDeleteResponse>
    {
        public string ITEM_CD;

        public string SLOT_SITE;

        public int IDX;

        public override string ApiName
        {
            get
            {
                return "systemParamParam.delete";
            }
        }
    }
}
