using Domain.Entities;
using RecipeStudio.Application.Interfaces;
using RecipeStudio.Shared.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace RecipeStudio.Application.Tests.Mocks
{
    internal class RecipeRepositoryMock : IRepository<Recipe>
    {
        public Task<OperationResult> AddAsync(Recipe entity)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(Recipe entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ExistAsync(Recipe entity)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Recipe>> GetAllAsync()
        {
            List<Recipe> recipes = await GetRecipesAsync();
            return recipes;
        }

        private async Task<List<Recipe>> GetRecipesAsync()
        {
            List<Recipe> recipes = new()
{
    new Recipe(
        "Spaghetti Bolognese",
        1,
        new List<Category>
        {
            new Category
            {
                Name = "Italian"
            }
        },
        new List<Step>
        {
            new Step { Name = "Cook the pasta." },
            new Step { Name = "Prepare the sauce." },
            new Step { Name = "Mix and serve." }
        },
        new List<Ingredient>
        {
            new Ingredient { Name = "Spaghetti" , Quantity = "200 gr."},
            new Ingredient { Name = "Ground Beef" , Quantity = "200 gr."},
            new Ingredient { Name = "Tomato Sauce" , Quantity = "200 gr."}
        }){User = new User { UserName = "Anthon", Id = 1 }},

    new Recipe(
        "Pancakes",
        2,
        new List<Category>
        {
            new Category
            {
                Name = "Breakfast"
            }
        },
        new List<Step>
        {
            new Step { Name = "Mix ingredients." },
            new Step { Name = "Pour batter into pan." },
            new Step { Name = "Cook both sides." }
        },
        new List<Ingredient>
        {
            new Ingredient { Name = "Flour", Quantity = "200 gr."  },
            new Ingredient { Name = "Milk" , Quantity = "200 gr."},
            new Ingredient { Name = "Eggs" , Quantity = "200 gr."}
        })
    {
         User = new User{ UserName = "Mary", Id = 2}
    },

    new Recipe(
        "Chicken Curry",
        3,
        new List<Category>
        {
            new Category
            {
                Name = "Asian"
            }
        },
        new List<Step>
        {
            new Step { Name = "Cut chicken." },
            new Step { Name = "Cook curry sauce." },
            new Step { Name = "Combine and simmer." }
        },
        new List<Ingredient>
        {
            new Ingredient { Name = "Chicken" , Quantity = "200 gr." },
            new Ingredient { Name = "Curry Powder" , Quantity = "200 gr."  },
            new Ingredient { Name = "Rice" , Quantity = "200 gr." }
        })
    {
        User = new User{ UserName = "Anthon", Id = 3}
    },

    new Recipe(
        "Caesar Salad",
        4,
        new List<Category>
        {
            new Category
            {
                Name = "Salad"
            }
        },
        new List<Step>
        {
            new Step { Name = "Wash lettuce." },
            new Step { Name = "Prepare dressing." },
            new Step { Name = "Mix ingredients." }
        },
        new List<Ingredient>
        {
            new Ingredient { Name = "Lettuce" , Quantity = "200 gr."},
            new Ingredient { Name = "Parmesan", Quantity = "200 gr." },
            new Ingredient { Name = "Croutons", Quantity = "200 gr." }
        }){User = new User { UserName = "Anthon", Id = 4 }}
            };
          
            return await Task.FromResult(recipes);
        }

        public Task<Recipe?> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Recipe entity)
        {
            throw new NotImplementedException();
        }
    }
}
