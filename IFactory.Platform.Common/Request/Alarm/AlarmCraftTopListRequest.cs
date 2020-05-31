using IFactory.Platform.Common.Response.Alarm;
using System;

namespace IFactory.Platform.Common.Request.Alarm
{
  public class AlarmCraftTopListRequest : BaseRequest<AlarmCraftTopListResponse>
  {
    public override string ApiName
    {
      get
      {
        return "alarm.craft.top.list";
      }
    }

    public DateTime? AlarmDateStart { get; set; }

    public DateTime? AlarmDateEnd { get; set; }

   public int CraftsDid { get; set; }
    }
}
