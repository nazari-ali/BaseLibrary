using BaseLibrary.Tool;
using BaseLibrary.Tool.Extensions;
using BaseLibrary.Tool.Models.HttpClientProxy;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace BaseLibrary.Tool.Utilities
{
    public static class HttpClientTools
    {
        /// <summary>
        /// Send a GET request to the specified Url with a cancellation token as an asynchronous operation.
        /// </summary>
        /// <param name="url"></param>
        /// <param name="headers"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static async Task<string> GetAsync(
            string url,
            Dictionary<string, string> headers = null,
            CancellationToken cancellationToken = default
        )
        {
            using var httpClient = new HttpClient();
            httpClient.SetHeaders(headers);

            using var r = await httpClient.GetAsync(new Uri(url), cancellationToken);
            var result = await r.Content.ReadAsStringAsync();

            return result;
        }

        /// <summary>
        /// Send a GET request to the specified Url and proxy configuration with a cancellation token as an asynchronous operation.
        /// </summary>
        /// <param name="url"></param>
        /// <param name="proxy"></param>
        /// <param name="headers"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static async Task<string> GetAsync(
            string url,
            HttpClientProxy proxy,
            Dictionary<string, string> headers = null,
            CancellationToken cancellationToken = default
        )
        {
            using var httpClient = new HttpClient(handler: SetProxy(proxy), disposeHandler: true);
            httpClient.SetHeaders(headers);

            using var r = await httpClient.GetAsync(new Uri(url), cancellationToken);
            var result = await r.Content.ReadAsStringAsync();

            return result;
        }

        /// <summary>
        /// Send a POST request with a cancellation token as an asynchronous operation.
        /// </summary>
        /// <param name="url"></param>
        /// <param name="data"></param>
        /// <param name="headers"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static async Task<string> PostAsync(
            string url,
            object data,
            Dictionary<string, string> headers = null,
            CancellationToken cancellationToken = default
        )
        {
            using var httpClient = new HttpClient();
            httpClient.SetHeaders(headers);

            HttpContent httpContent = new StringContent(data.Serialize());
            httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            using var r = await httpClient.PostAsync(new Uri(url), httpContent, cancellationToken);
            var result = await r.Content.ReadAsStringAsync();

            return result;
        }

        /// <summary>
        /// Send a PUT request with a cancellation token as an asynchronous operation.
        /// </summary>
        /// <param name="url"></param>
        /// <param name="data"></param>
        /// <param name="headers"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static async Task<string> PutAsync(
            string url,
            object data,
            Dictionary<string, string> headers = null,
            CancellationToken cancellationToken = default
        )
        {
            using var httpClient = new HttpClient();
            httpClient.SetHeaders(headers);

            HttpContent httpContent = new StringContent(data.Serialize());
            httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            using var r = await httpClient.PutAsync(new Uri(url), httpContent, cancellationToken);
            var result = await r.Content.ReadAsStringAsync();

            return result;
        }

        /// <summary>
        /// Sends a PATCH request with a cancellation token as an asynchronous operation.
        /// </summary>
        /// <param name="url"></param>
        /// <param name="data"></param>
        /// <param name="headers"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static async Task<string> PatchAsync(
            string url,
            object data,
            Dictionary<string, string> headers = null,
            CancellationToken cancellationToken = default
        )
        {
            using var httpClient = new HttpClient();
            httpClient.SetHeaders(headers);

            HttpContent httpContent = new StringContent(data.Serialize());
            httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            using var r = await httpClient.PatchAsync(new Uri(url), httpContent, cancellationToken);
            var result = await r.Content.ReadAsStringAsync();

            return result;
        }

        /// <summary>
        /// Send a DELETE request to the specified Uri with a cancellation token as an asynchronous operation.
        /// </summary>
        /// <param name="url"></param>
        /// <param name="headers"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static async Task<string> DeleteAsync(
            string url,
            Dictionary<string, string> headers = null,
            CancellationToken cancellationToken = default
        )
        {
            using var httpClient = new HttpClient();
            httpClient.SetHeaders(headers);

            using var r = await httpClient.DeleteAsync(new Uri(url), cancellationToken);
            var result = await r.Content.ReadAsStringAsync();

            return result;
        }

        #region Helper

        private static System.Net.Http.HttpClientHandler SetProxy(
            HttpClientProxy proxy
        )
        {
            // First create a proxy object
            var webProxy = new WebProxy()
            {
                Address = new Uri(proxy.Url),
                UseDefaultCredentials = proxy.UseDefaultCredentials,
            };

            #region region These creds are given to the proxy server, not the web server
            if (proxy.NeedCredential)
            {
                webProxy.Credentials = new NetworkCredential(
                    userName: proxy.Credentials.Username,
                    password: proxy.Credentials.Password
                );
            }
            #endregion

            // Now create a client handler which uses that proxy
            var httpClientHandler = new System.Net.Http.HttpClientHandler()
            {
                Proxy = webProxy,
            };

            #region Omit this part if you don't need to authenticate with the web server

            if (!proxy.NeedServerAuthentication) 
                return httpClientHandler;

            httpClientHandler.PreAuthenticate = proxy.HttpClientHandler.PreAuthenticate;
            httpClientHandler.UseDefaultCredentials = proxy.HttpClientHandler.UseDefaultCredentials;

            // *** These creds are given to the web server, not the proxy server ***
            httpClientHandler.Credentials = new NetworkCredential(
                userName: proxy.HttpClientHandler.Credentials.Username,
                password: proxy.HttpClientHandler.Credentials.Password
            );
            #endregion

            return httpClientHandler;
        }

        private static void SetHeaders(
            this HttpClient httpClient,
            Dictionary<string, string> headers
        )
        {
            if (headers == null) 
                return;

            foreach (var header in headers)
            {
                httpClient.DefaultRequestHeaders.Add(header.Key, header.Value);
            }
        }

        #endregion
    }
}