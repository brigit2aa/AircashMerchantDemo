using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Services.HttpRequest
{
    public interface IHttpRequestService
    {
        Task<HttpResponse> SendHttpRequest(object toSend, HttpMethod method, string uri);
    }
}
