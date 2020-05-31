using IFactory.Domain.Models;
using System.Collections.Generic;

namespace IFactory.Platform.Common.Response.Alarm
{
    public class AlarmCraftTopListResponse : BaseResponse
    {
        public IList<AlarmCraftTopModel> AlarmCraftTops { get; set; }
    }
}
