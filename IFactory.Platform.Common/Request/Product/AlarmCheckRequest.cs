using IFactory.Platform.Common.Response.Product;

namespace IFactory.Platform.Common.Request.Product
{
    public class AlarmCheckRequest : BaseRequest<AlarmCheckResponse>
    {
        public override string ApiName
        {
            get
            {
                return "alarm.check";
            }
        }

        public string AlarmCheck { get; set; }

        public string FacilityDid { get; set; }
    }
}
