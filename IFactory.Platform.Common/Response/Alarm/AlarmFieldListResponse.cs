using IFactory.Domain.Models;
using System.Collections.Generic;

namespace IFactory.Platform.Common.Response.Alarm
{
    public class AlarmFieldListResponse : BaseResponse
    {
        public IList<AlarmFieldModel> AlarmFields { get; set; }
    }
}
