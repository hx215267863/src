using System;
using System.Collections.Generic;

namespace IFactory.Platform.Client
{
    public class IWebApiDictionary : Dictionary<string, string>
    {
        private const string DATE_TIME_FORMAT = "yyyy-MM-dd HH:mm:ss";

        public IWebApiDictionary()
        {
        }

        public IWebApiDictionary(IDictionary<string, string> dictionary) : base(dictionary)
        {
        }

        public void Add(string key, object value)
        {
            string value2;
            if (value == null)
            {
                value2 = null;
            }
            else if (value is string)
            {
                value2 = (string)value;
            }
            else if (value is DateTime?)
            {
                value2 = (value as DateTime?).Value.ToString("yyyy-MM-dd HH:mm:ss");
            }
            else if (value is int?)
            {
                value2 = (value as int?).Value.ToString();
            }
            else if (value is long?)
            {
                value2 = (value as long?).Value.ToString();
            }
            else if (value is double?)
            {
                value2 = (value as double?).Value.ToString();
            }
            else if (value is bool?)
            {
                value2 = (value as bool?).Value.ToString().ToLower();
            }
            else
            {
                value2 = value.ToString();
            }
            this.Add(key, value2);
        }

        public new void Add(string key, string value)
        {
            if (!string.IsNullOrEmpty(key) && !string.IsNullOrEmpty(value))
            {
                base.Add(key, value);
            }
        }

        public void AddAll(IDictionary<string, string> dict)
        {
            if (dict != null && dict.Count > 0)
            {
                IEnumerator<KeyValuePair<string, string>> enumerator = dict.GetEnumerator();
                while (enumerator.MoveNext())
                {
                    KeyValuePair<string, string> current = enumerator.Current;
                    this.Add(current.Key, current.Value);
                }
            }
        }
    }
}
