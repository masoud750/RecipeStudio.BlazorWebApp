using Domain.Entities;
using RecipeStudio.DataAccess.Data;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Repositories;


namespace RecipeStudio.Infrastructure.Repositories
{
    public class CategoryRepository : IRepository<Category>
    {

        public CategoryRepository(RecipeStudioDbContext dbContext) 
        {
            ArgumentNullException.ThrowIfNull(dbContext, nameof(dbContext));
            _dbContext = dbContext;
        }
     

        private RecipeStudioDbContext _dbContext;
        public Task AddAsync(Category entity)
        {
            ArgumentNullException.ThrowIfNull(entity, nameof(entity));
            throw new NotImplementedException();
.        }

        public async Task DeleteAsync(Category entity)
        {
           ArgumentNullException.ThrowIfNull(entity, nameof(entity));
            //TODO: add try/catch block to handle exceptions for logging and error handling
            await _dbContext.Categories.Where(c => c.Id == entity.Id).ExecuteDeleteAsync();

            throw new NotImplementedException();
        }

        public async Task<List<Category>> GetAllAsync()
        {
            //throw new NotImplementedException();
            return await _dbContext.Categories.ToListAsync();
        }

        public Task<Category?> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Category entity)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> ExistAsync(Category entity)
        {
            ArgumentNullException.ThrowIfNull(entity,nameof(entity));
            Category? result = await _dbContext.FindAsync<Category>(entity.Id);
            if (result == null)
                return false;
            return true;

        }
    }
}
