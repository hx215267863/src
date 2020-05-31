using IFactory.Domain.Models;
using System.Collections.Generic;

namespace IFactory.Platform.Common.Response.Product
{
    public class CraftListResponse : BaseResponse
    {
        public IList<CraftModel> Crafts { get; set; }
    }
}
