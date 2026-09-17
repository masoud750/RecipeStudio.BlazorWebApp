using System;
using System.Collections.Generic;
using System.Text;

namespace RecipeStudio.Shared.DTOs
{
    public class RegisterRequest
    {
        public required string UserName { get; init; } = string.Empty;
        public required string Password { get; init; } = string.Empty;
        public required string Email { get; init; } = string.Empty;
    }
}
