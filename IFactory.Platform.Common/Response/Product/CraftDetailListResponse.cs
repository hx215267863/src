using IFactory.Domain.Models;
using System.Collections.Generic;

namespace IFactory.Platform.Common.Response.Product
{
    public class CraftDetailListResponse : BaseResponse
    {
        public IList<CraftDetailModel> CraftDetails { get; set; }
    }
}
