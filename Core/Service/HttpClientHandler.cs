using Core.Interfaces;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Core.Service {
    /// <summary>
    /// Wrapper for the HttpClient in oder to statisfy the IHttpHandler
    /// </summary>
    public class HttpClientHandler : IHttpHandler {
        private readonly HttpClient client = new HttpClient();

        public TimeSpan Timeout { get => client.Timeout; set => client.Timeout = value; }

        public HttpResponseMessage Get(string url) {
            return GetAsync(url).Result;
        }

        public HttpResponseMessage Post(string url, HttpContent content) {
            return PostAsync(url, content).Result;
        }

        public async Task<HttpResponseMessage> GetAsync(string url) {
            return await client.GetAsync(url);
        }

        public async Task<HttpResponseMessage> PostAsync(string url, HttpContent content) {
            return await client.PostAsync(url, content);
        }

        public void Dispose() {
            client.Dispose();
        }
    }
}
