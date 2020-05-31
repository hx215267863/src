using System;
using System.Collections.Generic;
using System.Runtime.Caching;
using System.Text.RegularExpressions;

namespace IFactory.Common.Caching
{
    public class MemoryCacheManager : ICacheManager
    {
        protected ObjectCache Cache
        {
            get
            {
                return MemoryCache.Default;
            }
        }

        public T Get<T>(string key)
        {
            return (T)Cache[key];
        }

        public void Set(string key, object data, int cacheTime)
        {
            if (data == null)
                return;
            Cache.Add(new CacheItem(key, data), new CacheItemPolicy()
            {
                AbsoluteExpiration = DateTime.Now + TimeSpan.FromMinutes(cacheTime)
            });
        }

        public bool IsSet(string key)
        {
            return Cache.Contains(key, null);
        }

        public void Remove(string key)
        {
            Cache.Remove(key, null);
        }

        public void RemoveByPattern(string pattern)
        {
            Regex regex = new Regex(pattern, RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.Singleline);
            List<string> stringList = new List<string>();
            foreach (KeyValuePair<string, object> keyValuePair in Cache)
            {
                if (regex.IsMatch(keyValuePair.Key))
                    stringList.Add(keyValuePair.Key);
            }
            foreach (string key in stringList)
                Remove(key);
        }

        public void Clear()
        {
            foreach (KeyValuePair<string, object> keyValuePair in Cache)
                Remove(keyValuePair.Key);
        }
    }
}
