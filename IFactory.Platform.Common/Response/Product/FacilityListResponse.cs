using IFactory.Domain.Models;
using System.Collections.Generic;

namespace IFactory.Platform.Common.Response.Product
{
    public class FacilityListResponse : BaseResponse
    {
        public IList<FacilityModel> Facilities { get; set; }
    }
}
