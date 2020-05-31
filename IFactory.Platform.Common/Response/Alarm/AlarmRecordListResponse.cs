using IFactory.Domain.Models;

namespace IFactory.Platform.Common.Response.Alarm
{
    public class AlarmRecordListResponse : BaseResponse
    {
        public PagedData<AlarmRecordItem> AlarmRecords { get; set; }
    }
}
