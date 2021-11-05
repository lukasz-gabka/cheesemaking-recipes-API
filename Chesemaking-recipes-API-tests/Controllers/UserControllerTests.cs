using Cheesemaking_recipes_API.Controllers;
using Cheesemaking_recipes_API.Models;
using Cheesemaking_recipes_API.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;

namespace Chesemaking_recipes_API_tests.Controllers
{
    [TestFixture]
    class UserControllerTests
    {
        private Mock<IUserService> service;
        private UserController controller;

        [SetUp]
        public void SetUp()
        {
            service = new Mock<IUserService>();
            controller = new UserController(service.Object);
        }

        [Test]
        public void Authorize_UserIsAuthorized_ReturnOk()
        {
            //act
            var actionResult = controller.Authorize();

            //assert
            Assert.That(actionResult, Is.InstanceOf<OkResult>());
        }

        [Test]
        public void Register_ProvidesCorrectParams_ReturnsNoContent()
        {
            //arrange
            RegistrationDto dto = new RegistrationDto();
            service.Setup(s => s.Register(dto)).Verifiable();

            //act
            var actionResult = controller.Register(dto);

            //assert
            Assert.That(actionResult, Is.InstanceOf<NoContentResult>());
        }

        [Test]
        public void Login_ProvidesCorrectParams_ReturnsOk()
        {
            //arrangr
            LoginDto dto = new LoginDto();
            var token = "token";
            service.Setup(s => s.Login(dto)).Returns(token);

            //act
            var actionResult = controller.Login(dto);

            //assert
            Assert.That(actionResult.Result, Is.InstanceOf<OkObjectResult>());
        }

        [Test]
        public void Login_ProvidesCorrectParams_ReturnsToken()
        {
            //arrange
            LoginDto dto = new LoginDto();
            var token = "3b99g5b";
            service.Setup(s => s.Login(dto)).Returns(token);

            //act
            var actionResult = controller.Login(dto);
            var result = actionResult.Result as OkObjectResult;

            //assert
            Assert.That(result.Value, Is.EqualTo(token));
        }
    }
}
