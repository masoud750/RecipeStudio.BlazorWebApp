using RecipeStudio.shared;

namespace RecipeStudioBlazorApp.Server.Authentification.Interfaces
{
    public interface IAuthService
    {
        Task<bool> LoginAsync(LoginRequest loginRequest);
        Task LogoutAsync();
    }
}
