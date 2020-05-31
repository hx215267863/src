using IFactory.Domain.Models;

namespace IFactory.Platform.Common.Response.SystemParam
{
    public class SystemParamGetResponse : BaseResponse
    {
        public SystemParamModel SystemParam { get; set; }
    }
}
