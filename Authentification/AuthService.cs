using Microsoft.AspNetCore.Components.Authorization;
using RecipeStudioBlazorApp.Server.Auth;
using RecipeStudioBlazorApp.Server.Authentification.Interfaces;
using RecipeStudio.shared;
namespace RecipeStudioBlazorApp.Server.Authentification
{

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
                    var loginResponse = await response.Content.ReadFromJsonAsync<LoginResponse>();
    
                    if (loginResponse?.Token != null)
                    {
                        await _tokenStorage.SetTokenAsync(loginResponse.Token);

                        ((CustomAuthenticationStateProvider)_authenticationStateProvider)
                            .NotifyUserAuthentication();
                        return true;
                    }
                }
                return false;
            }
            catch (Exception e)
            {
                string s = e.Message;
                return false;
            }
        }

        public Task LogoutAsync()
        {
            throw new NotImplementedException();
        }
    }
}
