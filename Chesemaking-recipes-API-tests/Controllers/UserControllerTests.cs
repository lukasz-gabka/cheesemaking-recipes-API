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
        Mock<IUserService> service;
        UserController controller;

        [SetUp]
        public void SetUp()
        {
            service = new Mock<IUserService>();
            controller = new UserController(service.Object);
        }

        [Test]
        public void Authorize_UserIsAuthorized_ReturnOk()
        {
            var actionResult = controller.Authorize();

            Assert.That(actionResult, Is.InstanceOf<OkResult>());
        }

        [Test]
        public void Register_ProvidesCorrectParams_ReturnsNoContent()
        {
            RegistrationDto dto = new RegistrationDto();
            service.Setup(s => s.Register(dto)).Verifiable();

            var actionResult = controller.Register(dto);

            Assert.That(actionResult, Is.InstanceOf<NoContentResult>());
        }

        [Test]
        public void Login_ProvidesCorrectParams_ReturnsOk()
        {
            LoginDto dto = new LoginDto();
            var token = "token";
            service.Setup(s => s.Login(dto)).Returns(token);

            var actionResult = controller.Login(dto);

            Assert.That(actionResult.Result, Is.InstanceOf<OkObjectResult>());
        }

        [Test]
        public void Login_ProvidesCorrectParams_ReturnsToken()
        {
            LoginDto dto = new LoginDto();
            var token = "3b99g5b";
            service.Setup(s => s.Login(dto)).Returns(token);

            var actionResult = controller.Login(dto);
            var result = actionResult.Result as OkObjectResult;

            Assert.That(result.Value, Is.EqualTo(token));
        }
    }
}
