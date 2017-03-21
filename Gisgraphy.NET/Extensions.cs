using System.Collections.Generic;
using System.Net;

namespace Gisgraphy.NET
{
    public static class Extensions
    {
        /// <summary>
        /// Convert a dictionary to a query string for web
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static string ToQueryString<TKey, TValue>(this Dictionary<TKey, TValue> parameters)
        {
            var array = new string[parameters.Keys.Count];
            var i = 0;
            foreach (var p in parameters)
            {
                array[i] = string.Format("{0}={1}", WebUtility.UrlEncode(p.Key.ToString()), WebUtility.UrlEncode(p.Value.ToString()));
                i++;
            }
            return "?" + string.Join("&", array);
        }
    }
}
