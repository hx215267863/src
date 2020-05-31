using IFactory.Domain.Models;

namespace IFactory.Platform.Common.Response.Alarm
{
    public class AlarmRecordGetResponse : BaseResponse
    {
        public AlarmRecordModel AlarmRecord { get; set; }
    }
}
