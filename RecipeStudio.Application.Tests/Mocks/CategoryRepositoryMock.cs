using Infrastructure.Repositories;
using Domain.Entities;

namespace RecipeStudio.Application.Tests.Mocks
{
    internal class CategoryRepositoryMock : IRepository<Category>
    {
       private readonly List<Category> _categories =     new List<Category>()
            {
                new Category { Id = 1, Name = "Test Category 1" } ,
                new Category { Id = 2, Name = "Test Category 2" },            
                new Category { Id = 3, Name = "Test Category 3" },
            };
public Task AddAsync(Category entity)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(Category entity)
        {
            _categories.Remove(entity);
            return Task.CompletedTask;
        }

        public Task<bool> ExistAsync(Category entity)
        {
            return Task.FromResult(_categories.Contains(entity));
        }

        public Task<List<Category>> GetAllAsync()
        {
            return Task.FromResult(_categories);
        }

        public Task<Category?> GetByIdAsync(int id)
        {
            var category = _categories.FirstOrDefault(c => c.Id == id);
            return Task.FromResult(category);
        }

        public Task UpdateAsync(Category entity)
        {
            throw new NotImplementedException();
        }
    }
}
    
       