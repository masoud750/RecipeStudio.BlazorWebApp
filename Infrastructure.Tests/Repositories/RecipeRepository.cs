using Domain.Entities;
using RecipeStudio.Application.Interfaces;
using RecipeStudio.Shared.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace RecipeStudio.Infrastructure.IntegrationTests.Repositories
{
    internal class RecipeRepository : IRepository<Recipe>
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

        public Task<List<Recipe>> GetAllAsync()
        {
            throw new NotImplementedException();
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
