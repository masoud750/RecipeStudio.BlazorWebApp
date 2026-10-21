using Microsoft.EntityFrameworkCore;
using RecipeStudio.Application.Interfaces;
using Domain.Entities;
using RecipeStudio.DataAccess.Data;
using RecipeStudio.Shared.Enums;


namespace Infrastructure.Repositories
{
    public class UserRepository : IRepository<User>
    {
        public UserRepository(RecipeStudioDbContext dbContext)
        {
            ArgumentNullException.ThrowIfNull(dbContext, nameof(dbContext));
            _dbContext = dbContext;
        }

        private RecipeStudioDbContext _dbContext;
        public async Task AddAsync(User entity)
        {
            ArgumentNullException.ThrowIfNull(entity, nameof(entity));
            await _dbContext.Users.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(User entity)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> ExistAsync(User entity)
        {
            ArgumentNullException.ThrowIfNull(entity, nameof(entity));

            // Preferred: use AnyAsync for existence checks (more efficient)
            var exists = await _dbContext.Users.AnyAsync(u =>
                u.UserName == entity.UserName &&
                u.PasswordHash == entity.PasswordHash);
            return exists;
        }

        public async Task<List<User>> GetAllAsync()
        {
            return await _dbContext.Users.ToListAsync();
        }

   

        public async Task<User?> GetByIdAsync(int id)
        {
            if (id <= 0)
                return null;

            User? user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == id);
            return user;        
        }
       
        public async Task UpdateAsync(User entity)
        {
            ArgumentNullException.ThrowIfNull(entity, nameof(entity));
            _dbContext.Users.Update(entity);             
            await _dbContext.SaveChangesAsync();
        }

        Task<OperationResult> IRepository<User>.AddAsync(User entity)
        {
            throw new NotImplementedException();
        }
    }
}
