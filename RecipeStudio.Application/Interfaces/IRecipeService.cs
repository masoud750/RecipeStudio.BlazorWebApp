using RecipeStudio.Shared.DTOs;
using RecipeStudio.Shared.DTOs.Commands;
using RecipeStudio.Shared.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace RecipeStudio.Application.Interfaces
{
 
        public interface IRecipeService
        {
            public Task<List<RecipeDTO>> GetAllAsync();

        }
    
}
