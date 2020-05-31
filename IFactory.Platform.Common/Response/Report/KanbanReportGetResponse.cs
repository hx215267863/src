using IFactory.Domain.Models;
using IFactory.Domain.Models.Report;
using System.Collections.Generic;

namespace IFactory.Platform.Common.Response.Report
{
    public class KanbanReportGetResponse : BaseResponse
    {
        public IList<TextValueModel<int>> ProductionReportData { get; set; }

        public IList<TextValueModel<int>> AlarmReportData { get; set; }

        public IList<TextValueModel<double>> ExcellentRateReportData { get; set; }

        public KanbanSettingModel KanbanSetting { get; set; }
    }
}
