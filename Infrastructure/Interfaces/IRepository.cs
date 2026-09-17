using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Interfaces
{
    public interface IRepository <T> where T : class
    {
      
        public Task<T?> GetByIdAsync(int id);

        public Task UpdateAsync(T entity);
   

        public Task<List<T>> GetAllAsync();
         
        Task AddAsync(T entity);
      
        Task DeleteAsync(T entity);

        Task<bool> ExistAsync(T entity);
    }
}

