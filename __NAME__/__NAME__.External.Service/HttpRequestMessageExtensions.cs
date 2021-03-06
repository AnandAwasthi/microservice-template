using System;
using System.Collections.Generic;
using System.Net.Http;

namespace __NAME__.External.Service
{
    public static class HttpRequestMessageExtensions
    {
        public static class CustomHeaderNames
        {
            public static readonly string Authentication = "Authentication";
            public static readonly string Forwarded = "Forwarded";

            public static readonly string XForwardedFor = "X-Forwarded-For";
            public static readonly string XForwardedHost = "X-Forwarded-Host";
            public static readonly string XForwardedProto = "X-Forwarded-Proto";

            public static readonly string XForwardedPathBase = "X-Forwarded-PathBase";
        }

        /// <summary>
        /// Request headers which are either set by the framework or shouldn't be copied.
        /// </summary>
        private static List<string> _ignoredRequestHeaders = new List<string>
        {
            "Connection",
            "Content-Length",
            "Date",
            "Expect",

            // forwarding the original Host is not permitted by https://tools.ietf.org/html/rfc7230#section-5.4
            // However there are also recommendations against this:
            // https://docs.oracle.com/cd/E40519_01/studio.310/studio_install/src/cidi_studio_reverse_proxy_preserve_host_headers.html
            "Host",
            "If-Modified-Since",
            "Range",
            "Transfer-Encoding",
            "Proxy-Connection"
        };


        public static void AddHeaders(this HttpRequestMessage target, IDictionary<string,List<string>> headers)
        {
            if (target == null)
            {
                throw new ArgumentNullException(nameof(target));
            }
            if (headers == null)
            {
                throw new ArgumentNullException(nameof(headers));
            }

            foreach(var header in headers)
            {
                if (!target.Headers.TryAddWithoutValidation(header.Key, header.Value.ToArray()) && target.Content != null)
                {
                    target.Content.Headers.TryAddWithoutValidation(header.Key, header.Value.ToArray());
                }
            }
        }
        public static HttpMethod TryVerb(this string verb)
        {
            HttpMethod method = null;
            switch (verb.ToUpper())
            {
                case "GET":
                    method = new HttpMethod("Get");
                    break;
                case "POST":
                    method = new HttpMethod("Post");
                    break;
                case "PUT":
                    method = new HttpMethod("Put");
                    break;
                case "DELETE":
                    method = new HttpMethod("Delete");
                    break;
                default:
                    throw new InvalidOperationException($"Invalid Operation {verb}");
            }
            return method;
        }

    }
}
