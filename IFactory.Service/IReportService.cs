using IFactory.Domain.Common;
using IFactory.Domain.Models.Report;
using System.Collections.Generic;

namespace IFactory.Service
{
    public interface IReportService
    {
        IList<TextValueModel<int>> GetProductionReport(TimeSectionType timeSectionType);

        IList<TextValueModel<int>> GetAlarmReport(TimeSectionType timeSectionType);

        IList<TextValueModel<double>> GetExcellentRateReport(TimeSectionType timeSectionType);
    }
}
