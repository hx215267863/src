using IFactory.Platform.Common.Response.Alarm;

namespace IFactory.Platform.Common.Request.Alarm
{
    public class AlarmTypeListRequest : BaseRequest<AlarmTypeListResponse>
    {
        public override string ApiName
        {
            get
            {
                return "alarm.type.list";
            }
        }
    }
}
