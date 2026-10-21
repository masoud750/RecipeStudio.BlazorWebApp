using Domain.Entities;
using RecipeStudio.Shared.DTOs.Commands;
using RecipeStudio.Shared.Enums;
using RecipeStudio.Shared.DTOs;

namespace RecipeStudio.Application.Interfaces
{
    public interface ICategoryService
    {
        public Task<List<ItemDTO>> GetAllAsync();

        public Task<ItemDTO?> GetByIdAsync(int id);

        public Task DeleteAsync(DeleteCategoryCommand category);

        public Task<bool> ExistAsync(ExistCategoryCommand category);

        public Task<OperationResult> AddAsync(AddCategoryCommand categoryCommand);

    }
}
