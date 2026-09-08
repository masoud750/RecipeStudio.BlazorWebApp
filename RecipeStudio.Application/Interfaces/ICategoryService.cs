using Domain.Entities;

namespace RecipeStudio.Application.Interfaces
{
    public interface ICategoryService
    {
        public Task<List<Category>> GetAllAsync();

        public Task<Category?> GetByIdAsync(int id);

        public Task DeleteAsync(Category category);

        public Task<bool> ExistAsync(Category category);

    }
}
