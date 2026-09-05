using Domain.Entities;
using Infrastructure.Repositories;
using RecipeStudio.Application.Interfaces;

namespace RecipeStudio.Application.Services
{
    public class CategoryService : ICategoryService
    {
        public CategoryService(IRepository<Category> repos) {
            _categoryRepos = repos;
        }

        IRepository<Category> _categoryRepos;
        public async Task<List<Category>> GetAllAsync()
        {
            //throw new NotImplementedException();
            List<Category> categories = new List<Category>();
            categories = await _categoryRepos.GetAllAsync();
            return categories.ToList();
        }

        public async Task<Category> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteAsync(Category category)
        {
            await _categoryRepos.DeleteAsync(category);
        }
    }
}
