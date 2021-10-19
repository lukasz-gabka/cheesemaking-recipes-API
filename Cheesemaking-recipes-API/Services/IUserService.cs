using Cheesemaking_recipes_API.Models;

namespace Cheesemaking_recipes_API.Services
{
    public interface IUserService
    {
        string Login(LoginDto dto);
        void Register(RegistrationDto dto);
    }
}