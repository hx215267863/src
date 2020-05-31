using IFactory.Domain.Models;
using System.Collections.Generic;


namespace IFactory.Platform.Common.Response.Alarm
{
   public class AlarmInfoListResponse : BaseResponse
    {
        public PagedData<AlarmInfoModel> AlarmInfoModel { get; set; }
    }
}  