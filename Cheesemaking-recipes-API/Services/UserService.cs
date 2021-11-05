using AutoMapper;
using Cheesemaking_recipes_API.Entities;
using Cheesemaking_recipes_API.Exceptions;
using Cheesemaking_recipes_API.Models;
using Microsoft.AspNetCore.Identity;
using System.Linq;

namespace Cheesemaking_recipes_API.Services
{
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly IPasswordHasher<User> _hasher;
        private readonly ApiDbContext _dbcontext;
        private readonly ITokenGenerator _tokenGenerator;
        private readonly ITemplateService _templateService;

        public UserService(
            IMapper mapper,
            IPasswordHasher<User> hasher,
            ApiDbContext context,
            ITokenGenerator tokenGenerator,
            ITemplateService templateService)
        {
            _mapper = mapper;
            _hasher = hasher;
            _dbcontext = context;
            _tokenGenerator = tokenGenerator;
            _templateService = templateService;
        }

        public void Register(RegistrationDto dto)
        {
            var user = _mapper.Map<User>(dto);
            var passwordHash = _hasher.HashPassword(user, dto.Password);
            user.PasswordHash = passwordHash;

            _dbcontext.Add(user);
            _dbcontext.SaveChanges();
            _templateService.CreateDefaultTemplateForNewUser(user.Id);
        }

        public string Login(LoginDto dto)
        {
            var user = _dbcontext.Users.FirstOrDefault(u => u.Email == dto.Email);

            if (user is null)
            {
                throw new BadRequestException("Użytkownik o podanym adresie e-mail nie istnieje");
            }

            var isPasswordCorrect = _hasher.VerifyHashedPassword(user, user.PasswordHash, dto.Password);

            if (isPasswordCorrect == PasswordVerificationResult.Failed)
            {
                throw new BadRequestException("Wprowadzono nieprawidłowe hasło");
            }

            string token = _tokenGenerator.GenerateToken(user);
            return token;
        }
    }
}
