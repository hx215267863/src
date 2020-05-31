using IFactory.Platform.Common.Util;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

namespace IFactory.Platform.Common.Parser
{
    public class JsonParser<T> : IParser<T> where T : BaseResponse
    {
        private static JsonSerializer _jsonSerializer;
        private static JsonSerializer _defaultJsonSerializer;

        public T Parse(string body)
        {
            T obj = default(T);
            JObject jobject = JObject.Parse(body);
            if (jobject != null)
                obj = jobject.ToObject<T>(GetJsonSerializer());
            if (obj == null)
                obj = Activator.CreateInstance<T>();
            if (obj != null)
                obj.Body = body;
            return obj;
        }

        public static JsonSerializer GetJsonSerializer()
        {
            if (_jsonSerializer == null)
                _jsonSerializer = JsonSerializer.Create(new JsonSerializerSettings()
                {
                    Converters = new List<JsonConverter>(WebApiUtils.GetJsonConverters())
                });
            return _jsonSerializer;
        }

        public static JsonSerializer GetDefaultJsonSerializer()
        {
            if (_defaultJsonSerializer == null)
                _defaultJsonSerializer = JsonSerializer.Create(new JsonSerializerSettings()
                {
                    Converters = new List<JsonConverter>(WebApiUtils.GetDefaultJsonConverters())
                });
            return _defaultJsonSerializer;
        }
    }
}
