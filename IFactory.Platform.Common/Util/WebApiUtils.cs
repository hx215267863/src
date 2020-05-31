using IFactory.Platform.Common.Parser;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Security.Cryptography;
using System.Text;

namespace IFactory.Platform.Common.Util
{
    public abstract class WebApiUtils
    {
        private const string DATE_TIME_FORMAT = "yyyy-MM-dd HH:mm:ss.fffffff";

        public static string FormatDateTime(DateTime dateTime)
        {
            return dateTime.ToString("yyyy-MM-dd HH:mm:ss.fffffff", DateTimeFormatInfo.InvariantInfo);
        }

        public static string SignYrqRequest(IDictionary<string, string> parameters, string secret)
        {
            return SignYrqRequest(parameters, secret, true);
        }

        public static string SignYrqRequest(IDictionary<string, string> parameters, string secret, bool qhs)
        {
            IEnumerator<KeyValuePair<string, string>> enumerator = ((IEnumerable<KeyValuePair<string, string>>)new SortedDictionary<string, string>(parameters)).GetEnumerator();
            StringBuilder stringBuilder1 = new StringBuilder(secret);
            while (enumerator.MoveNext())
            {
                KeyValuePair<string, string> current = enumerator.Current;
                string key = current.Key;
                current = enumerator.Current;
                string str = current.Value;
                if (!string.IsNullOrEmpty(key) && !string.IsNullOrEmpty(str))
                    stringBuilder1.Append(key).Append(str);
            }
            if (qhs)
                stringBuilder1.Append(secret);
            byte[] hash = MD5.Create().ComputeHash(Encoding.UTF8.GetBytes(stringBuilder1.ToString()));
            StringBuilder stringBuilder2 = new StringBuilder();
            for (int index = 0; index < hash.Length; ++index)
            {
                string @string = hash[index].ToString("X");
                if (@string.Length == 1)
                    stringBuilder2.Append("0");
                stringBuilder2.Append(@string);
            }
            return stringBuilder2.ToString();
        }

        public static IDictionary<string, string> DecodeYrqParams(string topParams)
        {
            return DecodeYrqParams(topParams, Encoding.UTF8);
        }

        public static IDictionary<string, string> DecodeYrqParams(string topParams, Encoding encoding)
        {
            if (string.IsNullOrEmpty(topParams))
                return null;
            byte[] bytes = Convert.FromBase64String(Uri.UnescapeDataString(topParams));
            return SplitUrlQuery(encoding.GetString(bytes));
        }

        public static IDictionary<string, string> SplitUrlQuery(string query)
        {
            IDictionary<string, string> dictionary = new Dictionary<string, string>();
            string[] strArray1 = query.Split('&');
            if (strArray1 != null && strArray1.Length != 0)
            {
                foreach (string str in strArray1)
                {
                    char[] separator = new char[1] { '=' };
                    int count = 2;
                    string[] strArray2 = str.Split(separator, count);
                    if (strArray2 != null && strArray2.Length == 2)
                        dictionary.Add(strArray2[0], strArray2[1]);
                }
            }
            return dictionary;
        }

        public static IDictionary<string, T> CleanupDictionary<T>(IDictionary<string, T> dict)
        {
            IDictionary<string, T> dictionary = new Dictionary<string, T>(dict.Count);
            IEnumerator<KeyValuePair<string, T>> enumerator = dict.GetEnumerator();
            while (enumerator.MoveNext())
            {
                KeyValuePair<string, T> current = enumerator.Current;
                string key = current.Key;
                current = enumerator.Current;
                T obj = current.Value;
                if (obj != null)
                    dictionary.Add(key, obj);
            }
            return dictionary;
        }

        public static string GetFileSuffix(byte[] fileData)
        {
            if (fileData == null || fileData.Length < 10)
                return null;
            if (fileData[0] == 71 && fileData[1] == 73 && fileData[2] == 70)
                return "GIF";
            if (fileData[1] == 80 && fileData[2] == 78 && fileData[3] == 71)
                return "PNG";
            if (fileData[6] == 74 && fileData[7] == 70 && (fileData[8] == 73 && fileData[9] == 70))
                return "JPG";
            if (fileData[0] == 66 && fileData[1] == 77)
                return "BMP";
            return null;
        }

        public static string GetMimeType(byte[] fileData)
        {
            string fileSuffix = GetFileSuffix(fileData);
            return fileSuffix == "JPG" ? "image/jpeg" : (fileSuffix == "GIF" ? "image/gif" : (fileSuffix == "PNG" ? "image/png" : (fileSuffix == "BMP" ? "image/bmp" : "application/octet-stream")));
        }

        public static string GetMimeType(string fileName)
        {
            fileName = fileName.ToLower();
            return !fileName.EndsWith(".bmp", StringComparison.CurrentCulture) ? (!fileName.EndsWith(".gif", StringComparison.CurrentCulture) ? (fileName.EndsWith(".jpg", StringComparison.CurrentCulture) || fileName.EndsWith(".jpeg", StringComparison.CurrentCulture) ? "image/jpeg" : (!fileName.EndsWith(".png", StringComparison.CurrentCulture) ? "application/octet-stream" : "image/png")) : "image/gif") : "image/bmp";
        }

        public static JsonConverter[] GetJsonConverters()
        {
            return new List<JsonConverter>()
              {
                new DateTimeConverter(),
                new PagedDataParser()
              }.ToArray();
        }

        public static JsonConverter[] GetDefaultJsonConverters()
        {
            return new List<JsonConverter>()
            {
                new DateTimeConverter()
            }.ToArray();
        }
    }
}
