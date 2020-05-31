using IFactory.Platform.Common.Util;
using Newtonsoft.Json;
using System;

namespace IFactory.Platform.Common.Parser
{
    public class DateTimeConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (!(value is DateTime))
                return;
            string str = WebApiUtils.FormatDateTime((DateTime)value);
            writer.WriteValue(str);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.ValueType == typeof(long))
                return new DateTime(1970, 1, 1).AddMilliseconds((long)reader.Value);
            if (objectType == typeof(DateTime?) && reader.Value == null)
                return null;
            DateTime result = new DateTime();
            if (DateTime.TryParse(reader.Value.ToString(), out result))
                return result;
            throw new Exception(string.Format("{0}:{1}转换成DateTime失败！", reader.Path, reader.Value));
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(DateTime) || objectType == typeof(DateTime?);
        }
    }
}
