using IFactory.Data.Crafts;
using IFactory.Domain.Crafts.Base.Entities;
using IFactory.Domain.Models.Crafts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace IFactory.Service.Crafts
{
    public class FacilityRunArgService : BaseCraftService<FacilityRunArgInfo>, IFacilityRunArgService, IBaseCraftService<FacilityRunArgInfo>
    {
        public FacilityRunArgService(ICraftDbFactory databaseFactory)
          : base(databaseFactory)
        {
        }

        public List<DateTime> GetFacilityRunArgDateTimes(int[] facilityIds, DateTime startTime, DateTime endTime)
        {
            return this.DataContext.FacilityRunArgInfos.Where(arg => facilityIds.Contains<int>(arg.FacilityDID) && arg.MCCollectDDate >= startTime && arg.MCCollectDDate < endTime).Select(arg => arg.MCCollectDDate).Distinct<DateTime>().ToList<DateTime>();
        }

        public FacilityRunArgSumModel Sum(int[] facilityIds, DateTime dateTime)
        {
            IQueryable<FacilityRunArgInfo> source = this.DataContext.FacilityRunArgInfos.Where<FacilityRunArgInfo>(arg => facilityIds.Contains<int>(arg.FacilityDID) && arg.MCCollectDDate == dateTime);
            source.ToString();
            FacilityRunArgSumModel facilityRunArgSumModel = new FacilityRunArgSumModel();
            facilityRunArgSumModel.MCAutoRunTime = source.Sum<FacilityRunArgInfo>(m => m.MCAutoRunTime) ?? 0L;
            facilityRunArgSumModel.MCAutoRunTotalTime = source.Sum<FacilityRunArgInfo>(m => m.MCAutoRunTotalTime) ?? 0L;
            facilityRunArgSumModel.MCAutoRunWarningTime = source.Sum<FacilityRunArgInfo>(m => m.MCAutoRunWarningTime) ?? 0L;
            facilityRunArgSumModel.MCAutoRunWarningTotalTime = source.Sum<FacilityRunArgInfo>(m => m.MCAutoRunWarningTotalTime) ?? 0L;
            facilityRunArgSumModel.MCBanCount = source.Sum<FacilityRunArgInfo>(m => m.MCBanCount) ?? 0L;
            facilityRunArgSumModel.MCCount = source.Sum<FacilityRunArgInfo>(m => m.MCCount) ?? 0L;
            facilityRunArgSumModel.MCOpenRunTime = source.Sum<FacilityRunArgInfo>(m => m.MCOpenRunTime) ?? 0L;
            facilityRunArgSumModel.MCOpenRunTotalTime = source.Sum<FacilityRunArgInfo>(m => m.MCOpenRunTotalTime) ?? 0L;
            facilityRunArgSumModel.MCRuningTime = source.Sum<FacilityRunArgInfo>(m => m.MCRuningTime) ?? 0L;
            facilityRunArgSumModel.MCRuningTotalTime = source.Sum<FacilityRunArgInfo>(m => m.MCRuningTotalTime) ?? 0L;
            facilityRunArgSumModel.MCStopTime = source.Sum<FacilityRunArgInfo>(m => m.MCStopTime) ?? 0L;
            facilityRunArgSumModel.MCStopTotalTime = source.Sum<FacilityRunArgInfo>(m => m.MCStopTotalTime) ?? 0L;
            facilityRunArgSumModel.MCTotalBadCount = source.Sum<FacilityRunArgInfo>(m => m.MCTotalBadCount) ?? 0L;
            facilityRunArgSumModel.MCTotalCount = source.Sum<FacilityRunArgInfo>(m => m.MCTotalCount) ?? 0L;
            facilityRunArgSumModel.MCWaitTime = source.Sum<FacilityRunArgInfo>(m => m.MCWaitTime) ?? 0L;
            facilityRunArgSumModel.MCWaitTotalTime = source.Sum<FacilityRunArgInfo>(m => m.MCWaitTotalTime) ?? 0L;
            return facilityRunArgSumModel;
        }
    }
}
