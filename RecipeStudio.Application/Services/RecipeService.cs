using Domain.Entities;
using RecipeStudio.Application.Common.Interfaces;
using RecipeStudio.Application.Interfaces;
using RecipeStudio.Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace RecipeStudio.Application.Services
{
    public class RecipeService : IRecipeService
    {
        public RecipeService(IRepository<Recipe> repos, IAppLogger logger)
        {
            ArgumentNullException.ThrowIfNull(repos, nameof(repos));
            ArgumentNullException.ThrowIfNull(logger, nameof(logger));
            _recipeRepos = repos;
        }

        private IRepository<Recipe> _recipeRepos;
        private IAppLogger _logger;

        public async Task<List<RecipeDTO>> GetAllAsync()
        {
            var dtos = new List<RecipeDTO>();
            try
            {
                List<Recipe> recipes = await _recipeRepos.GetAllAsync();
                return dtos = RecipeToDTOconverter.ConvertToDTOs(recipes);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to load recipes from the Database.");

                return dtos;
            }
        }
    }

    class RecipeToDTOconverter
    {
        public static List<RecipeDTO> ConvertToDTOs(List<Recipe> recipes)
        {
            ArgumentNullException.ThrowIfNull(recipes, nameof(recipes));
            List<RecipeDTO> dtos = new();
            foreach (var recipe in recipes)
            {
                dtos.Add(ConvertToDTO(recipe));
            }
            return dtos;
        }

        public static RecipeDTO ConvertToDTO(Recipe recipe)
        {

            ArgumentNullException.ThrowIfNull(recipe, nameof(recipe));
            return new RecipeDTO()
            {
                Id = recipe.Id,
                Name = recipe.Name,
                UserName = recipe.User.UserName,
                Categories = CategoryConverter.ConvertToDTOs(recipe.Categories.ToList()),
                Ingredients = IngredientConverter.ConvertToDTOs(recipe.Ingredients.ToList()),
                Steps = StepConverter.ConvertToDTOs(recipe.Steps.ToList())
            };
        }

    }




public static class CategoryConverter
{
    public static List<CategoryDTO> ConvertToDTOs(List<Category> categories)
    {
        List<CategoryDTO> dtos = new();
        foreach (Category category in categories)
            {
                dtos.Add(item: ConvertToDTO(category));
        }
        return dtos;
    }
 
        public static CategoryDTO ConvertToDTO(Category category)
        {
            ArgumentNullException.ThrowIfNull(category);
            return new CategoryDTO(category.Id, category.Name);
        }

        public static CategoryDTO CreateDTO(int id, string name)
        {
            return new CategoryDTO(id, name);
        }
    }

    public static class IngredientConverter
    {

        public static List<IngredientDTO> ConvertToDTOs(List<Ingredient> ingredients)
        {
            List<IngredientDTO> dtos = new();
            foreach (var ingredient in ingredients)
            {
                dtos.Add(ConvertToDTO(ingredient));
            }
            return dtos;
        }
        public static IngredientDTO ConvertToDTO(Ingredient ingredient)
        {
            ArgumentNullException.ThrowIfNull(ingredient);
            return new IngredientDTO(ingredient.Id, ingredient.Name,ingredient.Quantity);
        }
    }

    public static class StepConverter
    {

        public static List<StepDTO> ConvertToDTOs(List<Step> steps)
        {
            List<StepDTO> dtos = new();
            foreach (var step in steps)
            {
                dtos.Add(ConvertToDTO(step));
            }
            return dtos;
        }
        public static StepDTO ConvertToDTO(Step step)
        {
            ArgumentNullException.ThrowIfNull(step);
            return new StepDTO(step.Id, step.Name);
        }
    }
}




    



