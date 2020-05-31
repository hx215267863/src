using System.Collections.Generic;
using IFactory.Domain.Models;

namespace IFactory.Platform.Common.Response.Product
{
    public class OneKeyResponse : BaseResponse
    {
        public List<OneKeyItem> oneKeys { get; set; }
    }
}
