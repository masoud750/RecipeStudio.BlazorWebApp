using Domain.Entities;
using RecipeStudio.Shared.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace RecipeStudio.Application.Interfaces
{
    public interface IRepository <T> where T : class
    {
      
        public Task<T?> GetByIdAsync(int id);

        public Task UpdateAsync(T entity);
   

        public Task<List<T>> GetAllAsync();
         
        Task<OperationResult> AddAsync(T entity);
       
        Task DeleteAsync(T entity);

        Task<bool> ExistAsync(T entity);
    }
}

