using IFactory.Platform.Common.Util;
using Newtonsoft.Json;

namespace IFactory.Platform.Common.Request
{
    public abstract class BaseRequest<T> : IRequest<T> where T : BaseResponse
    {
        [JsonIgnore]
        public abstract string ApiName { get; }

        public virtual void Validate()
        {
        }

        public string GetParamJson()
        {
            return JsonConvert.SerializeObject(this, WebApiUtils.GetJsonConverters());
        }
    }
}
