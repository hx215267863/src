using IFactory.Domain.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;

namespace IFactory.Platform.Common.Parser
{
    public class PagedDataParser : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            IPagedData pagedData = (IPagedData)value;
            writer.WriteRawValue(new JObject(){
                {"PageCount",pagedData.PageCount},
                {"PageNumber",pagedData.PageNumber},
                {"PageSize",pagedData.PageSize},
                {"TotalItemCount",pagedData.TotalItemCount},
                {"Items",JArray.FromObject(value, JsonParser<BaseResponse>.GetDefaultJsonSerializer())}
            }.ToString());
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            JTokenReader jtokenReader = (JTokenReader)reader;
            IPagedData pagedData = (IPagedData)Activator.CreateInstance(objectType);
            JObject jobject = (JObject)jtokenReader.CurrentToken;
            pagedData.PageCount = jobject.Value<int>("PageCount");
            pagedData.PageNumber = jobject.Value<int>("PageNumber");
            pagedData.PageSize = jobject.Value<int>("PageSize");
            pagedData.TotalItemCount = jobject.Value<int>("TotalItemCount");
            if (jobject["Items"] != null)
            {
                foreach (JToken jtoken in (JArray)jobject["Items"])
                    pagedData.AddItem(jtoken.ToObject(objectType.GenericTypeArguments[0], JsonParser<BaseResponse>.GetDefaultJsonSerializer()));
            }
            return pagedData;
        }

        public override bool CanConvert(Type objectType)
        {
            return typeof(IPagedData).IsAssignableFrom(objectType);
        }
    }
}
