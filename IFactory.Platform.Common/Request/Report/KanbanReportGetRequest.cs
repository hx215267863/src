using IFactory.Platform.Common.Response.Report;

namespace IFactory.Platform.Common.Request.Report
{
    public class KanbanReportGetRequest : BaseRequest<KanbanReportGetResponse>
    {
        public override string ApiName
        {
            get
            {
                return "kanban.report.get";
            }
        }
    }
}
