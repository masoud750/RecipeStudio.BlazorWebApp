using Microsoft.AspNetCore.Components.Authorization;
using RecipeStudioBlazorApp.Server.Authentification.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace RecipeStudioBlazorApp.Server.Auth
{

    public class CustomAuthenticationStateProvider : AuthenticationStateProvider
    {
        public CustomAuthenticationStateProvider(ITokenStorageService tokenStorage)
        {
            _tokenStorage = tokenStorage;
        }

        private readonly ITokenStorageService _tokenStorage;
        private readonly JwtSecurityTokenHandler tokenHandler = new();

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
             var token = await _tokenStorage.GetTokenAsync();
            if(!string.IsNullOrEmpty(token))
            {
                var jwtToken = tokenHandler.ReadJwtToken(token);
                var identity = new ClaimsIdentity(jwtToken.Claims, "jwt");
                var user = new ClaimsPrincipal(identity);
                return new AuthenticationState(user);
            }
            var anonymousUser = new ClaimsPrincipal(new ClaimsIdentity());
            return new AuthenticationState(anonymousUser);
        }

        public void NotifyUserAuthentication()
        {
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }

        public void NotifyUserLogout()
        {
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }
    }
}
