using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces {

    /// <summary>
    /// Allows the use of a facade pattern or wrapper instead of the HttpClient directly.
    /// This is atleast important for testing.
    /// 
    /// See: https://stackoverflow.com/questions/36425008/mocking-httpclient-in-unit-tests
    /// </summary>
    public interface IHttpHandler : IDisposable {
        HttpResponseMessage Get(string url);
        HttpResponseMessage Post(string url, HttpContent content);
        Task<HttpResponseMessage> GetAsync(string url);
        Task<HttpResponseMessage> PostAsync(string url, HttpContent content);
        TimeSpan Timeout { get; set; }
    }
}
