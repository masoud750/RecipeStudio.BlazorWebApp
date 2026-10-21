using RecipeStudio.Application.Interfaces;
using RecipeStudio.Shared.DTOs;
using System.Net.Http.Json;


namespace RecipeStudio.Application.Services
{
    public class RegisterService : IRegisterService
    {
        public RegisterService(HttpClient httpClient)
        {
            ArgumentNullException.ThrowIfNull(httpClient, 
                                                nameof(httpClient));
            _httpClient = httpClient;
        }

        private readonly HttpClient _httpClient;
        public async Task<RegisterResponse> RegisterUserServicesAsync(RegisterRequest registerRequest)
        {
            ArgumentNullException.ThrowIfNull(registerRequest);

            var response = await _httpClient.PostAsJsonAsync("api/register", registerRequest);

            if (response != null && response.IsSuccessStatusCode)
            {
                var regResponse = await response.Content.ReadFromJsonAsync<RegisterResponse>();
                return regResponse ?? new RegisterResponse(false, regResponse?.Message, regResponse?.Errors);
            }

            // read error text
            var errorMessage = await response!.Content.ReadAsStringAsync();

            return new RegisterResponse(false, errorMessage, null);
        }
    }
}
