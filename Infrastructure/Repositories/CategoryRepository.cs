using Domain.Entities;
using RecipeStudio.DataAccess.Data;
using Microsoft.EntityFrameworkCore;
using RecipeStudio.Application.Interfaces;
using RecipeStudio.Application.Common.Interfaces;


using NLog;
using System.ClientModel.Primitives;


namespace RecipeStudio.Infrastructure.Repositories
{
    public class CategoryRepository : IRepository<Category>
    {

        public CategoryRepository(RecipeStudioDbContext dbContext,IAppLogger logger) 
        {
            ArgumentNullException.ThrowIfNull(dbContext, nameof(dbContext));
            ArgumentNullException.ThrowIfNull(logger);
            _dbContext = dbContext;
            _logger = logger;

        }
     

        private RecipeStudioDbContext _dbContext;
        private IAppLogger _logger;

public async Task<RecipeStudio.Shared.Enums.OperationResult> AddAsync(Category entity)
{
    try
    {
        ArgumentNullException.ThrowIfNull(entity, nameof(entity));
        entity.Name = entity.Name.ToLowerInvariant();
                entity.ValidFrom = DateTime.UtcNow;
                entity.ValidTo = new DateTime(2099, 12, 01);
      
        await _dbContext.AddAsync(entity);
        await _dbContext.SaveChangesAsync();
    }
    catch (DbUpdateException ex)
    {
        _logger.LogWarning(ex, "Adding new category failed.");
        _dbContext.Entry(entity).State = EntityState.Detached;
        return RecipeStudio.Shared.Enums.OperationResult.AlreadyExists;
    }
    catch (Exception ex)
    {
        _logger.LogWarning(ex, "Adding new category failed.");
                _dbContext.Entry(entity).State = EntityState.Detached;
                return RecipeStudio.Shared.Enums.OperationResult.Error;
    }

    return RecipeStudio.Shared.Enums.OperationResult.Success;
}

           
       


        public async Task DeleteAsync(Category entity)
        {
           ArgumentNullException.ThrowIfNull(entity, nameof(entity));
            //TODO: add try/catch block to handle exceptions for logging and error handling
            await _dbContext.Categories.Where(c => c.Id == entity.Id).ExecuteDeleteAsync();
        }

        public async Task<List<Category>> GetAllAsync()
        {
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
