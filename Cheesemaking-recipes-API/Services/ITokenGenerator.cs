using Cheesemaking_recipes_API.Entities;

namespace Cheesemaking_recipes_API.Services
{
    public interface ITokenGenerator
    {
        string GenerateToken(User user);
    }
}