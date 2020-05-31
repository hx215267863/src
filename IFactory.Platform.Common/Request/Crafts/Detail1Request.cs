using IFactory.Platform.Common.Response.Crafts;
using System;

namespace IFactory.Platform.Common.Request.Crafts
{
    public class Detail1Request : BaseRequest<Detail1Response>
    {
        public override string ApiName
        {
            get
            {
                return "Detail.1";
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
