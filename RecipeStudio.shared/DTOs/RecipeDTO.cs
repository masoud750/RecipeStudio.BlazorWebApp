using RecipeStudio.Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace RecipeStudio.Shared.DTOs
{
    public class RecipeDTO
    {
        public int Id { get; init; }
        public string Name { get; init; }
        public string UserName { get; init; }

        public string Description { get; init; }
        public List<CategoryDTO> Categories { get; init; } = new();
        public List<StepDTO> Steps { get; init; } = new();
        public List<IngredientDTO> Ingredients { get; init; } =  new();
        public void AddCategory(CategoryDTO category)
        {
            ArgumentNullException.ThrowIfNull(
                                category, nameof(category));

            Categories.Add(category);
        }

      
    }



}
