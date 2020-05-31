using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml.Serialization;

namespace IFactory.Platform.Common.Parser
{
    public class XmlParser<T> : IParser<T> where T : BaseResponse
    {
        private static Regex regex = new Regex("<(\\w+?)[ >]", RegexOptions.Compiled);
        private static Dictionary<string, XmlSerializer> parsers = new Dictionary<string, XmlSerializer>();

        public T Parse(string body)
        {
            Type type = typeof(T);
            string rootElement = GetRootElement(body);
            string fullName = type.FullName;
            if ("error_response".Equals(rootElement))
                fullName += "_error_response";
            XmlSerializer xmlSerializer = null;
            if (!parsers.TryGetValue(fullName, out xmlSerializer) || xmlSerializer == null)
            {
                XmlAttributes attributes = new XmlAttributes();
                attributes.XmlRoot = new XmlRootAttribute(rootElement);
                XmlAttributeOverrides overrides = new XmlAttributeOverrides();
                overrides.Add(type, attributes);
                xmlSerializer = new XmlSerializer(type, overrides);
                parsers[fullName] = xmlSerializer;
            }
            object obj1 = null;
            using (Stream stream = new MemoryStream(Encoding.UTF8.GetBytes(body)))
                obj1 = xmlSerializer.Deserialize(stream);
            T obj2 = (T)obj1;
            if (obj2 != null)
                obj2.Body = body;
            return obj2;
        }

        private string GetRootElement(string body)
        {
            Match match = regex.Match(body);
            if (match.Success)
                return match.Groups[1].ToString();
            throw new WebApiException("Invalid XML response format!");
        }
    }
}
