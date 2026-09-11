using Microsoft.AspNetCore.Components.Authorization;
using RecipeStudioBlazorApp.Server.Auth;
using RecipeStudioBlazorApp.Server.Authentification.Interfaces;
using RecipeStudio.shared;
namespace RecipeStudioBlazorApp.Server.Authentification
{
    public record LoginResponse(string Token);
    public class AuthService : IAuthService
    {

        public AuthService(
               HttpClient httpClient,
                    ITokenStorageService tokenStorage,
                                AuthenticationStateProvider authenticationStateProvider)
        {
            _httpClient = httpClient;            
            _tokenStorage = tokenStorage;
            _authenticationStateProvider = authenticationStateProvider;
        }

        private readonly HttpClient _httpClient;
        private readonly ITokenStorageService _tokenStorage;
        private readonly AuthenticationStateProvider _authenticationStateProvider;
        
        public async Task<bool> LoginAsync(LoginRequest loginRequest)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync(
    "api/auth/login",
    loginRequest
);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var loginResponse = System.Text.Json.JsonSerializer.Deserialize<LoginResponse>(content);

                    if (loginResponse != null)
                    {
                        
                        return true;
                    }
                }
                response = await _httpClient.PostAsJsonAsync(
  "api/auth/login", loginRequest);
                return false;
            }
            catch
            {
                return false;
            }
        }

        public Task LogoutAsync()
        {
            throw new NotImplementedException();
        }
    }
}
