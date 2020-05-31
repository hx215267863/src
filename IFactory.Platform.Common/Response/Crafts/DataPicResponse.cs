using IFactory.Domain.Models;
using System.Collections.Generic;

namespace IFactory.Platform.Common.Response.Crafts
{
    public class DataPicResponse : BaseResponse
    {
        public IList<DataPicItem> DataPics { get; set; }
    }
}
