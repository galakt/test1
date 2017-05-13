using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppK.Tests
{
    internal static class RequestBuilder
    {
        internal static HttpRequestMessage CreateRequest(string baseUrl, string url, string mthv, HttpMethod method)
        {
            var request = new HttpRequestMessage();

            request.RequestUri = new Uri(baseUrl + url);
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue(mthv));
            request.Method = method;

            return request;
        }

        internal static HttpRequestMessage CreateRequest<T>(string baseUrl, string url, string mthv, HttpMethod method, T content, MediaTypeFormatter formatter) where T : class
        {
            HttpRequestMessage request = CreateRequest(baseUrl, url, mthv, method);
            request.Content = new ObjectContent<T>(content, formatter);

            return request;
        }

        internal static HttpRequestMessage CreateRequest(string baseUrl, string url, string mthv, HttpMethod method, HttpContent content)
        {
            HttpRequestMessage request = CreateRequest(baseUrl, url, mthv, method);
            request.Content = content;

            return request;
        }
    }
}
