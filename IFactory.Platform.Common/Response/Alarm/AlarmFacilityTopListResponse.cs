using IFactory.Domain.Models;
using System.Collections.Generic;

namespace IFactory.Platform.Common.Response.Alarm
{
    public class AlarmFacilityTopListResponse : BaseResponse
    {
        public IList<AlarmFacilityTopModel> AlarmFacilityTops { get; set; }
    }
}
