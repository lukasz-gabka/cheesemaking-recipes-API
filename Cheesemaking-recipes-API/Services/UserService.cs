using AutoMapper;
using Cheesemaking_recipes_API.Entities;
using Cheesemaking_recipes_API.Exceptions;
using Cheesemaking_recipes_API.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace Cheesemaking_recipes_API.Services
{
    public class UserService
    {
        private readonly IMapper _mapper;
        private readonly PasswordHasher<User> _hasher;
        private readonly ApiDbContext _dbcontext;
        private readonly AuthenticationSettings _authenticationSettings;

        public UserService(
            IMapper mapper, 
            PasswordHasher<User> hasher, 
            ApiDbContext context, 
            AuthenticationSettings authenticationSettings)
        {
            _mapper = mapper;
            _hasher = hasher;
            _dbcontext = context;
            _authenticationSettings = authenticationSettings;
        }

        public void Register(RegistrationDto dto)
        {
            var user = _mapper.Map<User>(dto);
            var passwordHash = _hasher.HashPassword(user, dto.Password);
            user.PasswordHash = passwordHash;

            _dbcontext.Users.Add(user);
            _dbcontext.SaveChanges();
        }

        internal string Login(LoginDto dto)
        {
            var user = _dbcontext.Users.FirstOrDefault(u => u.Email == dto.Email);

            if (user is null)
            {
                throw new BadRequestException("User does not exist");
            }

            var isPasswordCorrect = _hasher.VerifyHashedPassword(user, user.PasswordHash, dto.Password);

            if (isPasswordCorrect == PasswordVerificationResult.Failed)
            {
                throw new BadRequestException("Incorrect password");
            }

            string token = GenerateToken(user);
            return token;
        }

        private string GenerateToken(User user)
        {
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimTypes.Email, user.Email)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                _authenticationSettings.JwtKey));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddMinutes(_authenticationSettings.JwtExpireMinutes);

            var token = new JwtSecurityToken(
                _authenticationSettings.JwtIssuer,
                _authenticationSettings.JwtIssuer,
                claims,
                expires: expires,
                signingCredentials: credentials
                );
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenSerialized = tokenHandler.WriteToken(token);
            return tokenSerialized;
        }
    }
}
