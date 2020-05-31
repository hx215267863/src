using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Web;

namespace IFactory.Common.Caching
{
    public class PerRequestCacheManager : ICacheManager
    {
        private readonly HttpContextBase _context;

        public PerRequestCacheManager(HttpContextBase context)
        {
            _context = context;
        }

        protected IDictionary GetItems()
        {
            if (_context != null)
                return _context.Items;
            return null;
        }

        public T Get<T>(string key)
        {
            IDictionary items = GetItems();
            if (items == null)
                return default(T);
            return (T)items[key];
        }

        public void Set(string key, object data, int cacheTime)
        {
            IDictionary items = GetItems();
            if (items == null || data == null)
                return;
            if (items.Contains(key))
                items[key] = data;
            else
                items.Add(key, data);
        }

        public bool IsSet(string key)
        {
            IDictionary items = GetItems();
            if (items == null)
                return false;
            return items[key] != null;
        }

        public void Remove(string key)
        {
            IDictionary items = GetItems();
            if (items == null)
                return;
            items.Remove(key);
        }

        public void RemoveByPattern(string pattern)
        {
            IDictionary items = GetItems();
            if (items == null)
                return;
            IDictionaryEnumerator enumerator = items.GetEnumerator();
            Regex regex = new Regex(pattern, RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.Singleline);
            List<string> stringList = new List<string>();
            while (enumerator.MoveNext())
            {
                if (regex.IsMatch(enumerator.Key.ToString()))
                    stringList.Add(enumerator.Key.ToString());
            }
            foreach (string str in stringList)
                items.Remove(str);
        }

        public void Clear()
        {
            IDictionary items = GetItems();
            if (items == null)
                return;
            IDictionaryEnumerator enumerator = items.GetEnumerator();
            List<string> stringList = new List<string>();
            while (enumerator.MoveNext())
                stringList.Add(enumerator.Key.ToString());
            foreach (string str in stringList)
                items.Remove(str);
        }
    }
}
