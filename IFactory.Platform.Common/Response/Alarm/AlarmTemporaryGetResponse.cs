using IFactory.Domain.Models;

namespace IFactory.Platform.Common.Response.Alarm
{
    public class AlarmTemporaryGetResponse : BaseResponse
    {
        public AlarmTemporaryModel AlarmTemporary { get; set; }
    }
}
