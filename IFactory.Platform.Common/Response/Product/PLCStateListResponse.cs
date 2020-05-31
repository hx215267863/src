using IFactory.Domain.Models;
using System.Collections.Generic;

namespace IFactory.Platform.Common.Response.Product
{
    public class PLCStateListResponse : BaseResponse
    {
        public IList<PLCStateModel> PLCStates { get; set; }
    }
}
