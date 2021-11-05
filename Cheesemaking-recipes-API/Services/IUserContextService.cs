using System.Security.Claims;

namespace Cheesemaking_recipes_API.Services
{
    public interface IUserContextService
    {
        int GetUserId { get; }
        ClaimsPrincipal User { get; }
    }
}