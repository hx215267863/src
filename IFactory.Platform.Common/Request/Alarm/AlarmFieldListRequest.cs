using IFactory.Platform.Common.Response.Alarm;

namespace IFactory.Platform.Common.Request.Alarm
{
    public class AlarmFieldListRequest : BaseRequest<AlarmFieldListResponse>
    {
        public override string ApiName
        {
            get
            {
                return "alarm.field.list";
            }
        }
    }
}
