using IFactory.Domain.Crafts.Base.Entities;
using IFactory.Domain.Models.Crafts;
using System;
using System.Collections.Generic;

namespace IFactory.LocalService.Crafts
{
    public interface IFacilityRunArgService : IBaseCraftService<FacilityRunArgInfo>
    {
        FacilityRunArgSumModel Sum(int[] facilityIds, DateTime dateTime);

        List<DateTime> GetFacilityRunArgDateTimes(int[] facilityIds, DateTime startTime, DateTime endTime);
    }
}
