using IFactory.Domain.Models;
using System.Collections.Generic;

namespace IFactory.Platform.Common.Response.Crafts
{
    public class AVEResponse : BaseResponse
    {
        public IList<AVEItem> Aves { get; set; }
    }
}
