using IFactory.Domain.Models;
using System.Collections.Generic;

namespace IFactory.Platform.Common.Response.Product
{
    public class ProcessListResponse : BaseResponse
    {
        public IList<ProcessModel> Processes { get; set; }
    }
}
