using AutoMapper;
using Cheesemaking_recipes_API.Entities;
using Cheesemaking_recipes_API.Models;
using Microsoft.AspNetCore.Identity;

namespace Cheesemaking_recipes_API.Services
{
    public class UserService
    {
        private readonly IMapper _mapper;
        private readonly PasswordHasher<User> _hasher;
        private readonly ApiDbContext _context;

        public UserService(IMapper mapper, PasswordHasher<User> hasher, ApiDbContext context)
        {
            _mapper = mapper;
            _hasher = hasher;
            _context = context;
        }

        public void Register(RegistrationDto dto)
        {
            var user = _mapper.Map<User>(dto);
            var passwordHash = _hasher.HashPassword(user, dto.Password);
            user.PasswordHash = passwordHash;

            _context.Users.Add(user);
            _context.SaveChanges();
        }
    }
}
