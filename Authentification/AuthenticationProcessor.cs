using Microsoft.AspNetCore.Authentication;

namespace RecipeStudioBlazorApp.Server.Authentification
{
    public class AuthenticationProcessor(IHttpContextAccessor httpContextAccessor)
    {
        public async Task<string?> GetAccessToken()
        {
            if (httpContextAccessor.HttpContext is null)
            {
                throw new Exception("HttpContext not available");
            }

            // Approach 1: Call 'GetTokenAsync'
            var accessToken = await httpContextAccessor.HttpContext
                .GetTokenAsync("access_token");

            // Approach 2: Authenticate the user and call 'GetTokenValue'
            /*
            var authResult = await httpContextAccessor.HttpContext.AuthenticateAsync();
            var accessToken = authResult?.Properties?.GetTokenValue("access_token");
            */

            return accessToken;
        }
    }
}
