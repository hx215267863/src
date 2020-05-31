using System.Collections.Generic;
using IFactory.Domain.Models;

namespace IFactory.Platform.Common.Response.Product
{
    public class DataProductionResponse : BaseResponse
    {
        public PagedData<DataProductionItem> DataProductions { get; set; }

        public PagedData<DataVulnerableItem> DataVulnerables { get; set; }

        public IList<DataVulnerableItem> CheckVulnerables { get; set; }
    }
}
