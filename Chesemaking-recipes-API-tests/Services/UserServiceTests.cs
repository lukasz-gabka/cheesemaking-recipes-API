using AutoMapper;
using Cheesemaking_recipes_API;
using Cheesemaking_recipes_API.Entities;
using Cheesemaking_recipes_API.Exceptions;
using Cheesemaking_recipes_API.Models;
using Cheesemaking_recipes_API.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace Chesemaking_recipes_API_tests.Services
{
    [TestFixture]
    class UserServiceTests
    {
        private Mock<ApiDbContext> dbContext;
        private Mock<IPasswordHasher<User>> hasher;
        private IMapper mapper;
        private Mock<ITemplateService> templateService;
        private Mock<ITokenGenerator> tokenGenerator;
        private IUserService service;

        [SetUp]
        public void SetUp()
        {
            dbContext = new Mock<ApiDbContext>();
            hasher = new Mock<IPasswordHasher<User>>();
            var config = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>());
            mapper = new Mapper(config);
            templateService = new Mock<ITemplateService>();
            tokenGenerator = new Mock<ITokenGenerator>();
            service = new UserService(mapper, hasher.Object, dbContext.Object, tokenGenerator.Object, templateService.Object);
        }

        [Test]
        public void Register_ProvidesCorrectDto_AddsNewUser()
        {
            //arrange
            var dto = new RegistrationDto()
            {
                Email = "mail@gmail.com",
                Name = "John",
                Password = "Password123",
                ConfirmPassword = "Password123"
            };
            templateService.Setup(t => t.CreateDefaultTemplateForNewUser(It.IsAny<int>())).Verifiable();
            SetUpDbContextForGettingUsers(GetUserList());

            //act
            service.Register(dto);

            //assert
            dbContext.Verify(d => d.Add(It.IsAny<User>()));
            dbContext.Verify(d => d.SaveChanges());
        }

        [Test]
        public void Login_ProvidesCorrectDto_ReturnsJwt()
        {
            //arrange
            var token = "token";
            var dto = new LoginDto()
            {
                Email = "mail@gmail.com",
                Password = "Password1"
            };
            hasher.Setup(h => h.VerifyHashedPassword(It.IsAny<User>(), It.IsAny<string>(), dto.Password))
                .Returns(PasswordVerificationResult.Success);
            SetUpDbContextForGettingUsers(GetUserList());
            tokenGenerator.Setup(t => t.GenerateToken(It.IsAny<User>())).Returns(token);

            //act
            string result = service.Login(dto);

            //assert
            Assert.That(result, Is.EqualTo(token));
        }

        [Test]
        public void Login_ProvidesIncorrectEmail_ThrowsBadRequestException()
        {
            //arrange
            var dto = new LoginDto()
            {
                Email = "incorrect@gmail.com",
                Password = "Password1"
            };
            SetUpDbContextForGettingUsers(GetUserList());

            //act, assert
            Assert.Throws<BadRequestException>(() => service.Login(dto));
        }

        [Test]
        public void Login_ProvidesIncorrectPassword_ThrowsBadRequestException()
        {
            //arrange
            var dto = new LoginDto()
            {
                Email = "mail@gmail.com",
                Password = "Incorrect"
            };
            hasher.Setup(h => h.VerifyHashedPassword(It.IsAny<User>(), It.IsAny<string>(), dto.Password))
                .Returns(PasswordVerificationResult.Failed);
            SetUpDbContextForGettingUsers(GetUserList());

            //act, assert
            Assert.Throws<BadRequestException>(() => service.Login(dto));
        }

        private void SetUpDbContextForGettingUsers(List<User> users)
        {
            var mockSet = GetUserMockSet(users);
            dbContext.Setup(d => d.Users).Returns(mockSet.Object);
        }

        private static Mock<DbSet<User>> GetUserMockSet(List<User> users)
        {
            var usersQueryable = users.AsQueryable();

            var mockSet = new Mock<DbSet<User>>();
            mockSet.As<IQueryable<User>>().Setup(m => m.Provider).Returns(usersQueryable.Provider);
            mockSet.As<IQueryable<User>>().Setup(m => m.Expression).Returns(usersQueryable.Expression);
            mockSet.As<IQueryable<User>>().Setup(m => m.ElementType).Returns(usersQueryable.ElementType);
            mockSet.As<IQueryable<User>>().Setup(m => m.GetEnumerator()).Returns(usersQueryable.GetEnumerator());

            return mockSet;
        }

        private static List<User> GetUserList()
        {
            return new List<User>()
            {
                new User()
                {
                    Id = 1,
                    Name = "John",
                    Email = "mail@gmail.com",
                    PasswordHash = "PasswordHash"
                }
            };
        }
    }
}
