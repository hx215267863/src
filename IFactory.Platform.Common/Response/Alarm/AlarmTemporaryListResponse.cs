using IFactory.Domain.Models;

namespace IFactory.Platform.Common.Response.Alarm
{
    public class AlarmTemporaryListResponse : BaseResponse
    {
        public PagedData<AlarmTemporaryItem> AlarmTemporaries { get; set; }
    }
}
