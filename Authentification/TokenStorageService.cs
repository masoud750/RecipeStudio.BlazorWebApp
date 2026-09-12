using RecipeStudioBlazorApp.Server.Authentification.Interfaces;

namespace RecipeStudioBlazorApp.Server.Authentification
{
    public class TokenStorageService : ITokenStorageService
    {
        public Task SetTokenAsync(string token)
        {
            _token = token;
            return Task.CompletedTask;
        }

        private string? _token;
        public Task<string?> GetTokenAsync()
        {
            return Task.FromResult(_token);
        }

        public Task RemoveTokenAsync()
        {
            _token = null;
            return Task.CompletedTask;
        }

    }
}
