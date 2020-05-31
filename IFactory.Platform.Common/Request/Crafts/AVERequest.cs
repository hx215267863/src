using IFactory.Platform.Common.Response.Crafts;
using System;

namespace IFactory.Platform.Common.Request.Crafts
{
    public class AVERequest : BaseRequest<AVEResponse>
    {
        public override string ApiName
        {
            get
            {
                return "AVE";
            }
        }

        public int? ProcessDID { get; set; }

        public int PageNumber { get; set; }

        public int PageSize { get; set; }

        public DateTime? TimeStart { get; set; }

        public DateTime? TimeEnd { get; set; }

        public DateTime? ProductTime { get; set; }

        public string Keyword { get; set; }

        public int Iden { get; set; }

        public String side { get; set; }
        public String type { get; set; }

        public DateTime dates { get; set; }
        public DateTime datee { get; set; }

        public IFactory.Domain.Common.SizeMeas? SizeMeas { get; set; }
    }
}
