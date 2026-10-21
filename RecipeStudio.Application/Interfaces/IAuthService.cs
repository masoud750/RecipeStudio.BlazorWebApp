using RecipeStudio.Shared.DTOs;

namespace RecipeStudio.Application.Interfaces
{
    public interface IAuthService
    {
        Task<bool> LoginAsync(LoginRequest loginRequest);
        Task LogoutAsync();
    }
}
