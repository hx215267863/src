using IFactory.Platform.Common.Response.Alarm;

namespace IFactory.Platform.Common.Request.Alarm
{
    public class AlarmTemporaryUpdateHandledRequest : BaseRequest<AlarmTemporaryUpdateHandledResponse>
    {
        public override string ApiName
        {
            get
            {
                return "alarm.temporary.update.handled";
            }
        }

        public int HandlerId { get; set; }

        public int AlarmTemporaryDID { get; set; }
    }
}
