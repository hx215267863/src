using IFactory.Platform.Common.Response.Product;
using System;

namespace IFactory.Platform.Common.Request.Product
{
    public class DataProductionRequest : BaseRequest<DataProductionResponse>
    {
        public override string ApiName
        {
            get
            {
                return "Data.Production.list";
            }
        }

        public int? ProcessDID { get; set; }

        public int PageNumber { get; set; }

        public int PageSize { get; set; }

        public DateTime? TimeStart { get; set; }

        public DateTime? TimeEnd { get; set; }

        public string Keyword { get; set; }

        public string code { get; set; }
    }
}
