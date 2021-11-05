using Cheesemaking_recipes_API.Entities;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Cheesemaking_recipes_API.Services
{
    public class TokenGenerator : ITokenGenerator
    {
        private readonly AuthenticationSettings _authenticationSettings;

        public TokenGenerator(AuthenticationSettings authenticationSettings)
        {
            _authenticationSettings = authenticationSettings;
        }

        public string GenerateToken(User user)
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
