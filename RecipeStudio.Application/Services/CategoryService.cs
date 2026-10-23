using Domain.Entities;
using RecipeStudio.Application.Interfaces;

using RecipeStudio.Shared.DTOs.Commands;
using RecipeStudio.Shared.DTOs;
using RecipeStudio.Shared.Enums;
using RecipeStudio.Application.Common.Interfaces;



namespace RecipeStudio.Application.Services
{
    public class CategoryService : ICategoryService
    {
        public CategoryService(IRepository<Category> repos,IAppLogger logger)
        {
            ArgumentNullException.ThrowIfNull(repos, nameof(repos));
            ArgumentNullException.ThrowIfNull(logger, nameof(logger));
            _categoryRepos = repos;
            _logger = logger;
        }

        private IRepository<Category> _categoryRepos;
        private IAppLogger _logger;
        public async Task<List<CategoryDTO>> GetAllAsync()
        {
            //throw new NotImplementedException();
            List<Category> categories = new List<Category>();
            categories = await _categoryRepos.GetAllAsync();
            List<CategoryDTO> categoryDTOs = new();
            categoryDTOs = ConvertToDTOs(categories);
            return categoryDTOs;
        }

        public List<CategoryDTO> ConvertToDTOs(List<Category> categories)
        {
            return CategoryConverter.ConvertToDTOs(categories);
        }

        public CategoryDTO CreateDTO(int id, string name)
        {
            return CategoryConverter.CreateDTO(id, name);
        }

       

        public async Task<CategoryDTO?> GetByIdAsync(int id)
        {
            Category? category = await _categoryRepos.GetByIdAsync(id);
            return (category is null) ? null :
               CategoryConverter.ConvertToDTO(category);
        }



        public async Task<bool> ExistAsync(ExistCategoryCommand category)
        {
            ArgumentNullException.ThrowIfNull(category, nameof(category));
            return await _categoryRepos.ExistAsync(
                 new Category()
                 {
                     Id = category.Id,
                     Name = category.Name
                 });

        }


        public async Task<OperationResult> AddAsync(AddCategoryCommand category)
        {
            ArgumentNullException.ThrowIfNull(category, nameof(category));
            string name = category.Name;
            var item = new Category()
            {
                Name = name.Normalize()
            };


            try
            {
                return await _categoryRepos.AddAsync(item);
                
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,
                    $"An error occurred while adding category '{category.Name}'.");

                return OperationResult.Error;
            }

        }



        public async Task DeleteAsync(DeleteCategoryCommand category)
        {
            ArgumentNullException.ThrowIfNull(category);
            await _categoryRepos.DeleteAsync(new Category()
            {
                Name = category.Name
            });
        }
    }
}
