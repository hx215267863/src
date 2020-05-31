﻿using IFactory.Platform.Common.Response.Alarm;

using System;

namespace IFactory.Platform.Common.Request.Alarm
{
   public class AlarmInfoListRequest : BaseRequest<AlarmInfoListResponse>
    {
        public override string ApiName
        {
            get
            {
                return "alarm.info.list";
            }
        }

        public int PageNumber { get; set; }

        public int PageSize { get; set; }

        public string Keyword { get; set; }

        public DateTime? AlarmDateStart { get; set; }

        public DateTime? AlarmDateEnd { get; set; }

        public int CraftsDid { get; set; }
    }
}
