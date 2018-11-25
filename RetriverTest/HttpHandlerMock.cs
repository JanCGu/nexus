using Core.Interfaces;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace RetriverTest {
    internal class HttpHandlerMock : IHttpHandler {

        public HttpResponseMessage Response { get; set; }
        public TimeSpan Timeout { get; set; }
        public int WaitMs { get; set; }
        public bool ThrowTimeOut { get; set; }
        public string Url { get; set; }
        public HttpContent Content { get; set; }

        public HttpHandlerMock() { }

        public void Dispose() {
            //not needed as this is a mock.
        }

        public HttpResponseMessage Get(string url) {
            var ret = GetAsync(url);
            ret.Wait();
            return ret.Result;
        }

        public async Task<HttpResponseMessage> GetAsync(string url) {
            Url = url;
            if (WaitMs > 0)
                await Task.Delay(WaitMs);
            if (ThrowTimeOut)
                throw new TimeoutException();
            return Response;
        }

        public HttpResponseMessage Post(string url, HttpContent content) {
            var ret = PostAsync(url, content);
            ret.Wait();
            return ret.Result;
        }

        public Task<HttpResponseMessage> PostAsync(string url, HttpContent content) {
            Content = content;
            return GetAsync(url);
        }
    }
}
