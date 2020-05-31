using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Xml.Serialization;

namespace IFactory.Platform.Common
{
    [Serializable]
    public class BaseResponse : IResponse
    {
        public const string ErrCode001 = "001";
        public const string ErrCode002 = "002";
        public const string ErrCode003 = "003";
        public const string ErrCode004 = "004";
        public const string ErrCode005 = "005";
        public const string ErrCode006 = "006";
        public const string ErrCode007 = "007";
        public const string ErrCode008 = "008";

        [XmlElement("Code")]
        [JsonProperty("Code")]
        public string ErrCode { get; set; }

        [XmlElement("Msg")]
        [JsonProperty("Msg")]
        public string ErrMsg { get; set; }

        [JsonIgnore]
        public string Body { get; set; }

        [JsonIgnore]
        public JObject Json { get; set; }

        public virtual bool IsError
        {
            get
            {
                if (string.IsNullOrEmpty(ErrCode) || string.Equals(ErrCode, "0", StringComparison.InvariantCultureIgnoreCase))
                    return !string.IsNullOrEmpty(ErrMsg);
                return true;
            }
        }
    }
}
