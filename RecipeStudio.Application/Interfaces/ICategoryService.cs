using Domain.Entities;

namespace RecipeStudio.Application.Interfaces
{
    public interface ICategoryService
    {
        public Task<List<Category>> GetAllAsync();

        public Task DeleteAsync(Category category);

    }
}
