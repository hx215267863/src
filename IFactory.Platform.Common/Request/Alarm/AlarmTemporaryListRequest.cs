using IFactory.Platform.Common.Response.Alarm;

namespace IFactory.Platform.Common.Request.Alarm
{
    public class AlarmTemporaryListRequest : BaseRequest<AlarmTemporaryListResponse>
    {
        public override string ApiName
        {
            get
            {
                return "alarm.temporary.list";
            }
        }

        public int? ProcessDID { get; set; }

        public int CraftsDid { get; set; }

        public int PageNumber { get; set; }

        public int PageSize { get; set; }
    }
}
