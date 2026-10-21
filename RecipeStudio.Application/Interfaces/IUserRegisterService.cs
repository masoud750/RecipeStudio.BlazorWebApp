using Domain.Entities;


namespace RecipeStudio.Application.Interfaces
{
    public interface IUserRegisterService
    {
        public Task<bool> RegisterUserAsync(string userName,
                                             string password,
                                                 string Email);


    }
}
