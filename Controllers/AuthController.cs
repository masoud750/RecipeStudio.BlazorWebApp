//using Azure.Core;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.IdentityModel.Tokens;
//using RecipeStudio.shared;
//using System.IdentityModel.Tokens.Jwt;
//using System.Security.Claims;
//using System.Text;
//namespace RecipeStudioBlazorApp.Server.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class AuthController : ControllerBase
//    {
//        public AuthController(IConfiguration configuration)
//        {
//            _configuration = configuration;
//        }


//        private readonly IConfiguration _configuration;

//        [AllowAnonymous]
//        [HttpPost("login")]
//        public IActionResult Login([FromBody] LoginRequest request)
//        {
//            if (((request.UserName == "admin") || (request.UserName == "user")) && (request.Password == "password"))
//            {
//                var token = GenerateJwtToken(request.UserName);
//                return Ok(new LoginResponse(token));
//            }

//            return Unauthorized();
//        }



//        private string GenerateJwtToken(string username)
//        {
//            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
//            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

//            //var groups = username == "admin" ? "CanRead,CanWrite" : "CanRead";
//            var roles = username == "admin" ? "Admin" : "User";

//            var claims = new[]
//            {
//                new Claim(ClaimTypes.Name, username),
//                new Claim(JwtRegisteredClaimNames.Sub, username),
//                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
//                new Claim(ClaimTypes.Role, roles)
//                //new Claim("groups", groups)
//            };

//            var token = new JwtSecurityToken(
//                issuer: _configuration["Jwt:Issuer"],
//                audience: _configuration["Jwt:Audience"],
//                claims: claims,
//                expires: DateTime.Now.AddHours(1),
//                signingCredentials: credentials
//            );

//            return new JwtSecurityTokenHandler().WriteToken(token);
//        }
//    }
//}
