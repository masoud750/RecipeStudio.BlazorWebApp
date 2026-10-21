using RecipeStudio.Application.Services;
using RecipeStudio.Application.Tests.Mocks;
using RecipeStudio.Shared.DTOs;
using System.Net;
using System.Numerics;

namespace RecipeStudio.Application.Tests.Services
{
    [TestFixture]
    internal class RegisterServicesTests
    {
        [Test]
        public void RegisterServices_WhenCalledwithNullHttpClient_ShouldThrowArgumentNullEx()
        {
            var ex = Assert.Throws<ArgumentNullException>(() =>
            {
                _ = new RegisterService(null!);
            });
            Assert.That(ex.ParamName, Is.EqualTo("httpClient"));
        }
        [Test]
        public void RegisterUserServices_WhenCalledWithInvalideLoginRequest_ShouldThrowArgumentNullEx()
        {
            
            var ex = Assert.Throws<ArgumentNullException>(async static () =>
                    {
                        HttpResponseMessage response = new()
                        {
                            StatusCode = System.Net.HttpStatusCode.OK,
                            Content = new StringContent("{\"success\": true}")
                        };

                        var httpMessageHandlerMock = new Mocks.HttpMessageHandlerMock<HttpResponseMessage>(response, System.Net.HttpStatusCode.OK);
                        HttpClient httpClient = new HttpClient(httpMessageHandlerMock);
                        var sut = new RegisterService(httpClient);
                        _ = await sut.RegisterUserServicesAsync(null!);

                    });
            Assert.That(ex.ParamName, Is.EqualTo("loginRequest"));
        }

        [Test]
        public async Task RegisterUserServices_WhenCalledWithValidLoginRequest_ShouldRegisterUser()
        {
            HttpResponseMessage response = new()
            {
                StatusCode = System.Net.HttpStatusCode.OK,
                Content = new StringContent("{\"succeeded\": true}")
            };

            var registerRequest = new RegisterRequest
            {
                UserName = "testuser",
                Password = "testpassword",
                Email = "testuser@example.com"
            };

            HttpMessageHandler handlerMock = new HttpMessageHandlerMock<RegisterResponse>(
                                             new RegisterResponse(true,null,null)   , HttpStatusCode.OK);

            var client = new HttpClient(handlerMock)
            {
                BaseAddress = new Uri("http://test.local") // WICHTIG!
            };

            var sut = new RegisterService(client);
            var result = await sut.RegisterUserServicesAsync(registerRequest);
            Assert.That(result.Succeeded, Is.True);
            
        }
    }
}
