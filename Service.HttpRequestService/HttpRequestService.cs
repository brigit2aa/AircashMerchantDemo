using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Service.HttpRequestService
{
    public class HttpRequestService : IHttpRequestService
    {
        private readonly HttpClient HttpClient;

       /*public HttpRequestService(HttpClient httpClient)
        {
            HttpClient = httpClient;
        }*/

        public async Task<HttpResponse> SendHttpRequest(object toSend, HttpMethod method, string uri)
        {

            using (var request = new HttpRequestMessage(method, uri))
            {
                string json = JsonConvert.SerializeObject(toSend);
                request.Content = new StringContent(json, Encoding.UTF8, "application/json");
                using (var response = await HttpClient.SendAsync(request))
                {
                    HttpResponse httpResponse = new HttpResponse { ResponseContent = await response.Content.ReadAsStringAsync(), ResponseCode = response.StatusCode};
                    return httpResponse;
                }
            };
        }
    }
}
