using IFactory.Data;
using IFactory.Domain.Common;
using IFactory.Domain.Models.Report;
using System;
using System.Collections.Generic;
using System.Linq;

namespace IFactory.Service
{
    public class ReportService : BaseService, IReportService
    {
        public ReportService(IDatabaseFactory databaseFactory)
          : base(databaseFactory)
        {
        }

        public IList<TextValueModel<double>> GetExcellentRateReport(TimeSectionType timeSectionType)
        {
            List<TextValueModel<double>> textValueModelList = new List<TextValueModel<double>>();
            switch (timeSectionType)
            {
                case TimeSectionType.Day:
                    textValueModelList = this.DataContext.Database.SqlQuery<DateDataItem<double>>("select * from(select excellent_rate as Value,create_date as Date from excellent_rate where date(excellent_rate)< date(now()) order by Date desc) as t order by Date").Select(m => m.ToModel()).ToList();
                    break;
                case TimeSectionType.Week:
                    textValueModelList = this.DataContext.Database.SqlQuery<WeekDataItem<double>>("select * from(select AVG(excellent_rate) as Value,week(create_date) as Week from excellent_rate where week(excellent_rate)<= week(now()) group by Week desc limit 7) as t order by Week asc").Select(m => m.ToModel()).ToList();
                    break;
                case TimeSectionType.Month:
                    textValueModelList = this.DataContext.Database.SqlQuery<MonthDataItem<double>>("select * from(select AVG(excellent_rate) as Value,month(create_date) as Month from excellent_rate where month(excellent_rate)<= month(now()) group by Month desc limit 7) as t order by Month asc").Select(m => m.ToModel()).ToList();
                    break;
                case TimeSectionType.Quarter:
                    textValueModelList = this.DataContext.Database.SqlQuery<QuarterDataItem<double>>("select * from(select AVG(excellent_rate) as Value,quarter(create_date) as Quarter from excellent_rate where quarter(excellent_rate)<= quarter(now()) group by Quarter desc limit 7) as t order by Quarter asc").Select(m => m.ToModel()).ToList();
                    break;
                case TimeSectionType.Year:
                    textValueModelList = this.DataContext.Database.SqlQuery<YearDataItem<double>>("select * from(select AVG(excellent_rate) as Value,year(create_date) as Year from excellent_rate where year(excellent_rate)<= year(now()) group by Year desc limit 7) as t order by Year asc").Select(m => m.ToModel()).ToList();
                    break;
            }
            return textValueModelList;
        }

        public IList<TextValueModel<int>> GetAlarmReport(TimeSectionType timeSectionType)
        {
            List<TextValueModel<int>> textValueModelList = new List<TextValueModel<int>>();
            switch (timeSectionType)
            {
                case TimeSectionType.Day:
                    textValueModelList = DataContext.Database.SqlQuery<DateDataItem<int>>("select * from(select sum(alarm_count) as Value,date(alarm_time) as Date from alarm_record where date(alarm_time)< date(now()) group by Date order by Date desc limit 7) as t order by Date asc").Select(m => m.ToModel()).ToList();
                    break;
                case TimeSectionType.Week:
                    textValueModelList = this.DataContext.Database.SqlQuery<WeekDataItem<int>>("select * from(select sum(alarm_count) as Value,week(alarm_time) as Week from alarm_record where week(alarm_time)<= week(now()) group by Week desc limit 7) as t order by Week asc").Select(m => m.ToModel()).ToList();
                    break;
                case TimeSectionType.Month:
                    textValueModelList = this.DataContext.Database.SqlQuery<MonthDataItem<int>>("select * from(select sum(alarm_count) as Value,month(alarm_time) as Month from alarm_record where month(alarm_time)<= month(now()) group by Month desc limit 7) as t order by Month asc").Select(m => m.ToModel()).ToList();
                    break; 
                case TimeSectionType.Quarter:
                    textValueModelList = this.DataContext.Database.SqlQuery<QuarterDataItem<int>>("select * from(select sum(alarm_count) as Value,quarter(alarm_time) as Quarter from alarm_record where quarter(alarm_time)<= quarter(now()) group by Quarter desc limit 7) as t order by Quarter asc").Select(m => m.ToModel()).ToList();
                    break;      
                case TimeSectionType.Year:
                    textValueModelList = this.DataContext.Database.SqlQuery<YearDataItem<int>>("select * from(select sum(alarm_count) as Value,year(alarm_time) as Year from alarm_record where year(alarm_time)<= year(now()) group by Year desc limit 7) as t order by Year asc").Select(m => m.ToModel()).ToList();
                    break;
            }
            return textValueModelList;
        }

        public IList<TextValueModel<int>> GetProductionReport(TimeSectionType timeSectionType)
        {
            List<TextValueModel<int>> textValueModelList = new List<TextValueModel<int>>();
            switch (timeSectionType)
            {
                case TimeSectionType.Day:
                    textValueModelList = this.DataContext.Database.SqlQuery<DateDataItem<int>>("select * from(select count(did) as Value,date(starttime) as Date from production_alldata where date(starttime)< date(now()) group by Date order by Date desc limit 7) as t order by Date asc").Select<DateDataItem<int>, TextValueModel<int>>(m => m.ToModel<int>()).ToList<TextValueModel<int>>();
                    break;
                case TimeSectionType.Week:
                    textValueModelList = this.DataContext.Database.SqlQuery<WeekDataItem<int>>("select * from(select count(did) as Value,week(starttime) as Week from production_alldata where week(starttime)<= week(now()) group by Week desc limit 7) as t order by Week asc").Select<WeekDataItem<int>, TextValueModel<int>>((Func<WeekDataItem<int>, TextValueModel<int>>)(m => m.ToModel<int>())).ToList<TextValueModel<int>>();
                    break;
                case TimeSectionType.Month:
                    textValueModelList = this.DataContext.Database.SqlQuery<MonthDataItem<int>>("select * from(select count(did) as Value,month(starttime) as Month from production_alldata where month(starttime)<= month(now()) group by Month desc limit 7) as t order by Month asc").Select<MonthDataItem<int>, TextValueModel<int>>(m => m.ToModel<int>()).ToList<TextValueModel<int>>();
                    break;
                case TimeSectionType.Quarter:
                    textValueModelList = this.DataContext.Database.SqlQuery<QuarterDataItem<int>>("select * from(select count(did) as Value,quarter(starttime)  as Quarter from production_alldata where quarter(starttime)<= quarter(now()) group by Quarter desc limit 7) as t order by Quarter asc").Select(m => m.ToModel<int>()).ToList();
                    break;
                case TimeSectionType.Year:
                    textValueModelList = this.DataContext.Database.SqlQuery<YearDataItem<int>>("select * from(select count(did) as Value,year(starttime) as Year from production_alldata where year(starttime)<= year(now()) group by Year desc limit 7) as t order by Year asc").Select(m => m.ToModel()).ToList();
                    break;
            }
            return textValueModelList;
        }
    }
}
