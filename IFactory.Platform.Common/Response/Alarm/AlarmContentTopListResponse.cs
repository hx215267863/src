using IFactory.Domain.Models;
using System.Collections.Generic;

namespace IFactory.Platform.Common.Response.Alarm
{
    public class AlarmContentTopListResponse : BaseResponse
    {
        public IList<AlarmContentTopModel> AlarmContentTops { get; set; }
    }
}
