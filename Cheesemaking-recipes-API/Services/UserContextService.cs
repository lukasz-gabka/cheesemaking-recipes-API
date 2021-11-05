using Microsoft.AspNetCore.Http;
using System;
using System.Security.Claims;

namespace Cheesemaking_recipes_API.Services
{
    public class UserContextService : IUserContextService
    {
        private readonly IHttpContextAccessor _contextAccessor;

        public UserContextService(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }

        public ClaimsPrincipal User => _contextAccessor.HttpContext.User;

        public int GetUserId => Convert.ToInt32(User.FindFirst(
            c => c.Type == ClaimTypes.NameIdentifier).Value);
    }
}
