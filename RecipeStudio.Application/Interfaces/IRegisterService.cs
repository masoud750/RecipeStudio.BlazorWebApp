using RecipeStudio.Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace RecipeStudio.Application.Interfaces
{
    public interface IRegisterService
    {
        public Task<RegisterResponse> RegisterUserServicesAsync(RegisterRequest registerRequest);

    }
}
