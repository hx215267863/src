using System;

namespace IFactory.Common
{
    public static class Extensions
    {
        public static long ToUnixTime(this DateTime dateTime)
        {
            return (long)(dateTime.ToUniversalTime() - new DateTime(1970, 1, 1)).TotalMilliseconds;
        }
    }
}
