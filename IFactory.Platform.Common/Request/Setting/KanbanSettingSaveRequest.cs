using IFactory.Domain.Common;
using IFactory.Platform.Common.Response.Setting;

namespace IFactory.Platform.Common.Request.Setting
{
    public class KanbanSettingSaveRequest : BaseRequest<KanbanSettingSaveResponse>
    {
        public override string ApiName
        {
            get
            {
                return "kanban.setting.save";
            }
        }

        public int KanbanSettingId { get; set; }

        public TimeSectionType ProductionReportTimeSection { get; set; }

        public TimeSectionType ExcellentRateReportTimeSection { get; set; }

        public TimeSectionType AlarmReportTimeSection { get; set; }

        public int RefreshInterval { get; set; }
    }
}
