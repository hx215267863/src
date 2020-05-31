﻿using IFactory.Platform.Common.Response.Alarm;
using System;

namespace IFactory.Platform.Common.Request.Alarm
{
    public class AlarmFacilityTopListRequest : BaseRequest<AlarmFacilityTopListResponse>
    {
        public override string ApiName
        {
            get
            {
                return "alarm.facility.top.list";
            }
        }

        public DateTime? AlarmDateStart { get; set; }

        public DateTime? AlarmDateEnd { get; set; }

        public int CraftsDid { get; set; }
    }
}
