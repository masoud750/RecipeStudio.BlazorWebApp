using System;
using System.Collections.Generic;
using System.Text;

namespace RecipeStudio.Shared.DTOs
{
    public record class RegisterResponse(bool Succeeded, 
                    string? Message, IEnumerable<string>? Errors);
    
    
}
