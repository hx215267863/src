using IFactory.Platform.Common.Response.Crafts;
using System;

namespace IFactory.Platform.Common.Request.Crafts
{
    public class FacilityRunArgSumRequest : BaseRequest<FacilityRunArgSumResponse>, ICraftsReqeust
    {
        public override string ApiName
        {
            get
            {
                return "facility.run.arg.sum";
            }
        }

        public int CraftDID { get; set; }

        public string CraftNO { get; set; }

        public DateTime CollectDate { get; set; }
    }
}
