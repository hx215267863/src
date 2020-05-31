using IFactory.Domain.Models;
using IFactory.Domain.Entities;
using System.Collections.Generic;

namespace IFactory.Platform.Common.Response.Product
{
    public class AlarmCheckResponse : BaseResponse
    {
        public IList<AlarmCheckInfo> AlarmCheck { get; set; }
    }
}
