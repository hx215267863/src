using IFactory.Domain.Models;

namespace IFactory.Platform.Common.Response.Setting
{
    public class ArgumentResponse : BaseResponse
    {
        public PagedData<ArgumentItem> Augus { get; set; }
    }
}
