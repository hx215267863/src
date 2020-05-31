using IFactory.Platform.Common.Response.Alarm;

namespace IFactory.Platform.Common.Request.Alarm
{
    public class AlarmTemporaryGetRequest : BaseRequest<AlarmTemporaryGetResponse>
    {
        public override string ApiName
        {
            get
            {
                return "alarm.temporary.get";
            }
        }

        public int AlarmTemporaryDID { get; set; }
    }
}
