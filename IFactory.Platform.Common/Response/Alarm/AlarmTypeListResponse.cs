using IFactory.Domain.Models;
using System.Collections.Generic;

namespace IFactory.Platform.Common.Response.Alarm
{
  public class AlarmTypeListResponse : BaseResponse
  {
    public IList<AlarmTypeModel> AlarmTypes { get; set; }
  }
}
