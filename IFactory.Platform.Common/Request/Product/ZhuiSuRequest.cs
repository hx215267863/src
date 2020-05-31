using IFactory.Platform.Common.Response.Product;
using System;

namespace IFactory.Platform.Common.Request.Product
{
    public class ZhuiSuRequest : BaseRequest<ZhuiSuResponse>
    {
        public override string ApiName
        {
            get
            {
                return "Zhui.Su.list";
            }
        }

        public int? ProcessDID { get; set; }

        public int PageNumber { get; set; }

        public int PageSize { get; set; }

        public DateTime? TimeStart { get; set; }

        public DateTime? TimeEnd { get; set; }

        public string Keyword { get; set; }
    }
}
