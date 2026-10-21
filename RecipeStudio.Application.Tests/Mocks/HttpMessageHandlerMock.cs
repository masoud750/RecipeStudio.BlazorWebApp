using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace RecipeStudio.Application.Tests.Mocks
{
    public class HttpMessageHandlerMock<T> : HttpMessageHandler
    {
        private readonly T? responseObject;
        private readonly HttpStatusCode statusCode;

        public HttpMessageHandlerMock(T? responseObject, HttpStatusCode statusCode)
        {
            this.responseObject = responseObject;
            this.statusCode = statusCode;
        }


        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var response = new HttpResponseMessage(statusCode)
            {
                RequestMessage = request // wichtig für Extensions!
            };

            if (responseObject != null)
            {
                string json = JsonSerializer.Serialize(responseObject);
                response.Content = new StringContent(json, Encoding.UTF8, "application/json");
            }

            return Task.FromResult(response);
        }

        
    }

    public class HttpRequestMessageMock : IDisposable
    {
        public void Dispose()
        {
            // In Tests: nothing to dispose
        }
    }
}