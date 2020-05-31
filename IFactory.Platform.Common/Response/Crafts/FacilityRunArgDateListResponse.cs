using System;
using System.Collections.Generic;

namespace IFactory.Platform.Common.Response.Crafts
{
    public class FacilityRunArgDateListResponse : BaseResponse
    {
        public IList<DateTime> CollectDates { get; set; }
    }
}
