using IFactory.Platform.Common.Response.Alarm;

namespace IFactory.Platform.Common.Request.Alarm
{
    public class AlarmFieldSaveRequest : BaseRequest<AlarmFieldSaveResponse>
    {
        public override string ApiName
        {
            get
            {
                return "alarm.field.save";
            }
        }

        public int AlarmFieldId { get; set; }

        public string FieldName { get; set; }

        public string FieldDescription { get; set; }
    }
}
