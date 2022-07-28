using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Service.HttpRequestService
{
    public interface IHttpRequestService
    {
        Task<HttpResponse> SendHttpRequest(object toSend, HttpMethod method, string url);
    }
}
