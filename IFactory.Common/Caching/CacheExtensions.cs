using System;

namespace IFactory.Common.Caching
{
    public static class CacheExtensions
    {
        public static T Get<T>(this ICacheManager cacheManager, string key, Func<T> acquire)
        {
            return cacheManager.Get<T>(key, 60, acquire);
        }

        public static T Get<T>(this ICacheManager cacheManager, string key, int cacheTime, Func<T> acquire)
        {
            if (cacheManager.IsSet(key))
                return cacheManager.Get<T>(key);
            T obj = acquire();
            cacheManager.Set(key, obj, cacheTime);
            return obj;
        }
    }
}
