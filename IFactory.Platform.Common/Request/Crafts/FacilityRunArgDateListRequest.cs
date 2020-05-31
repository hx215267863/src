using IFactory.Platform.Common.Response.Crafts;
using System;

namespace IFactory.Platform.Common.Request.Crafts
{
    public class FacilityRunArgDateListRequest : BaseRequest<FacilityRunArgDateListResponse>, ICraftsReqeust
    {
        public override string ApiName
        {
            get
            {
                return "facility.run.arg.date.list";
            }
        }

        public int CraftDID { get; set; }

        public string CraftNO { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }
    }
}
