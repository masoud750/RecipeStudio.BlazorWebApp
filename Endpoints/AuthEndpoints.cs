using Azure.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using RecipeStudio.shared;
using RecipeStudioBlazorApp.Server.Authentification;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace RecipeStudioBlazorApp.Server.Endpoints
{

    using System.IdentityModel.Tokens.Jwt;
    using System.Security.Claims;
    using System.Text;
    using Microsoft.AspNetCore.Routing;
    using Microsoft.Extensions.Configuration;
    using Microsoft.IdentityModel.Tokens;

    public static class AuthEndpoints
    {
        public static void MapAuthEndpoints(this IEndpointRouteBuilder app)
        {
            app.MapPost("api/auth/login", (LoginRequest request, IConfiguration configuration) =>
            {
                if ((request.UserName is "admin" or "user") && request.Password == "password")
                {
                    var token = GenerateJwtToken(request.UserName, configuration);
                    return Results.Ok(new LoginResponse(token));
                }

                return Results.Unauthorized();
            });
        }

        private static string GenerateJwtToken(string username, IConfiguration configuration)
        {
            var securityKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(configuration["Jwt:Key"])
            );
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var roles = username == "admin" ? "Admin" : "User";

            var claims = new[]
            {
            new Claim(ClaimTypes.Name, username),
            new Claim(JwtRegisteredClaimNames.Sub, username),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(ClaimTypes.Role, roles)
        };

            var token = new JwtSecurityToken(
                issuer: configuration["Jwt:Issuer"],
                audience: configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }

    public record LoginRequest(string UserName, string Password);
    public record LoginResponse(string Token);

}




