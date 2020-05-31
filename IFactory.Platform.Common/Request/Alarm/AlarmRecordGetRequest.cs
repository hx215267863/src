using IFactory.Platform.Common.Response.Alarm;

namespace IFactory.Platform.Common.Request.Alarm
{
    public class AlarmRecordGetRequest : BaseRequest<AlarmRecordGetResponse>
    {
        public override string ApiName
        {
            get
            {
                return "alarm.record.get";
            }
        }

        public int AlarmRecordDID { get; set; }
    }
}
