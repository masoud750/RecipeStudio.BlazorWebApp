using Domain.Entities;

using RecipeStudio.Application.Interfaces;

using System.Security.Cryptography;
using System.Text;


namespace RecipeStudio.Application.Services
{
    public class UserRegisterService : IUserRegisterService
    {
      
        public UserRegisterService(IRepository<User> repo)
        {
            _repo = repo;
        }
        IRepository<User> _repo;
        public async Task<bool> RegisterUserAsync(string userName,
                                             string password,
                                             string email)
        {

            var hash = CreateSHA256Hash(password);
            var user = new User()
                {
                    UserName = userName,
                    PasswordHash = hash,
                    Email = email
                };

            await _repo.AddAsync(user);
            return true;                
            }  
        
        private string CreateSHA256Hash(string password)
        {
            using var sha = SHA256.Create();
            var bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(password));
            var hash = Convert.ToHexString(bytes);
            return hash;
        }
    }
}
               
         
