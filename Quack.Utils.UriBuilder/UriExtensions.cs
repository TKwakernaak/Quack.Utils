using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Kwaklibs.Utils.UriBuilder
{
    public static class UriExtensions
    {
        //todo: rewrite with custom implementation.
        public static Uri AddOrUpdateParameter(this Uri url, string paramName, string paramValue)
        {
            var uriBuilder = new System.UriBuilder(url);
            var query = HttpUtility.ParseQueryString(uriBuilder.Query);

            if (query.AllKeys.Contains(paramName))
            {
                query[paramName] = paramValue;
            }
            else
            {
                query.Add(paramName, paramValue);
            }
            uriBuilder.Query = query.ToString();

            return uriBuilder.Uri;
        }

        public static Uri AddParameter(this Uri url, string paramName, string paramValue)
        {
            var uriBuilder = new System.UriBuilder(url);
            string queryToAppend = $"{paramName}={paramValue}";

            if (uriBuilder.Query != null && uriBuilder.Query.Length > 1)
                uriBuilder.Query = uriBuilder.Query.Substring(1) + "&" + queryToAppend;
            else
                uriBuilder.Query = queryToAppend;
            return uriBuilder.Uri;

        }

        public static Uri AddParameters(this Uri url, System.Collections.Specialized.NameValueCollection nameValues)
        {
            var uriBuilder = new System.UriBuilder(url);
            var query = HttpUtility.ParseQueryString(uriBuilder.Query);

            query.Add(nameValues);

            uriBuilder.Query = query.ToString();

            return uriBuilder.Uri;
        }

        /// <summary>
        /// Append a path to an uri.
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="paths"></param>
        /// <returns></returns>
        public static Uri Append(this Uri uri, params string[] paths)
        {
            return new Uri(paths.Aggregate(uri.AbsoluteUri, (current, path) => string.Format("{0}/{1}", current.TrimEnd('/'), path.TrimStart('/'))));
        }

        /// <summary>
        /// parse the supplied value from the uri.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url"></param>
        /// <param name="paramName"></param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException">unable to find specified param</exception>
        /// <exception cref="InvalidCastException">Unable to cast the param value to T. </exception>
        public static T GetParameterValue<T>(this Uri url, string paramName) where T : struct
        {
            var uriBuilder = new System.UriBuilder(url);
            var query = HttpUtility.ParseQueryString(uriBuilder.Query);

            if (!query.AllKeys.Contains(paramName))
                throw new InvalidOperationException($"{paramName} not present in Uri {url}");

            var value = query[paramName];

            return (T)Convert.ChangeType(value, typeof(T));
        }
    }
}
