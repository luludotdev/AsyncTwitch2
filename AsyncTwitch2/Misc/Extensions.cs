using System;
using System.Collections.Generic;

namespace AsyncTwitch.Misc
{
    static class Extensions
    {
        public static DateTime FromUnixTime(this long unixTime)
        {
            var offset = DateTimeOffset.FromUnixTimeMilliseconds(unixTime);
            return offset.UtcDateTime;
        }

        public static TV GetValue<TK, TV>(this IDictionary<TK, TV> dict, TK key, TV defaultValue = default(TV))
        {
            return dict.TryGetValue(key, out TV value) ? value : defaultValue;
        }
    }
}
