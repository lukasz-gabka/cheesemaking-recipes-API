using Cheesemaking_recipes_API.Controllers;
using Cheesemaking_recipes_API.Models;
using Cheesemaking_recipes_API.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;

namespace Chesemaking_recipes_API_tests.Controllers
{
    [TestFixture]
    class TemplateControllerTests
    {
        private Mock<ITemplateService> service;
        private TemplateController controller;
        private List<TemplateDto> templatesDtos = new List<TemplateDto>()
        {
            new TemplateDto(),
            new TemplateDto()
        };

        [SetUp]
        public void SetUp()
        {
            service = new Mock<ITemplateService>();
            controller = new TemplateController(service.Object);
        }

        [Test]
        public void Get_EntitiesExist_ReturnsOk()
        {
            //arrange
            service.Setup(s => s.GetAll()).Returns(templatesDtos);

            //act
            var actionResult = controller.Get();

            //assert
            Assert.That(actionResult.Result, Is.InstanceOf<OkObjectResult>());
        }

        [Test]
        public void Get_EntitiesExist_ReturnsAllEntities()
        {
            //arrange
            service.Setup(s => s.GetAll()).Returns(this.templatesDtos);

            //act
            var actionResult = controller.Get();
            var result = actionResult.Result as OkObjectResult;
            List<TemplateDto> templatesDtos = (List<TemplateDto>)result.Value;

            //assert
            Assert.That(templatesDtos.Count, Is.EqualTo(2));
        }

        [Test]
        public void Get_EntitiesAreNull_ReturnsNoContent()
        {
            //arrange
            List<TemplateDto> templatesDtos = null;
            service.Setup(s => s.GetAll()).Returns(templatesDtos);

            //act
            var actionResult = controller.Get();
            var result = actionResult.Result as NoContentResult;

            //assert
            Assert.That(actionResult.Result, Is.TypeOf<NoContentResult>());
        }

        [Test]
        public void Get_EntitiesAreEmpty_ReturnNoContent()
        {
            //arrange
            List<TemplateDto> templatesDtos = new List<TemplateDto>();
            service.Setup(s => s.GetAll()).Returns(templatesDtos);

            //act
            var actionResult = controller.Get();
            var result = actionResult.Result as NoContentResult;

            //assert
            Assert.That(actionResult.Result, Is.TypeOf<NoContentResult>());
        }

        [Test]
        public void Get_EntityWithIdSpecifiedExist_ReturnOk()
        {
            //arrange
            TemplateDto templateDto = new TemplateDto();
            service.Setup(s => s.GetById(1)).Returns(templateDto);

            //act
            var actionResult = controller.Get(1);

            //assert
            Assert.That(actionResult.Result, Is.InstanceOf<OkObjectResult>());
        }

        [Test]
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        public void Get_EntityWithIdSpecifiedExist_ReturnsEntity(int templateId)
        {
            //arrange
            TemplateDto templateDto = new TemplateDto() { Id = templateId };
            service.Setup(s => s.GetById(templateId)).Returns(templateDto);

            //act
            var actionResult = controller.Get(templateId);
            var result = actionResult.Result as OkObjectResult;
            TemplateDto templateResult = (TemplateDto)result.Value;

            //assert
            Assert.That(templateResult.Id, Is.EqualTo(templateId));
        }

        [Test]
        public void Get_EntityWithIdSpecifiedDoesntExist_ReturnsNoContent()
        {
            //arrange
            TemplateDto templateDto = null;
            service.Setup(s => s.GetById(1)).Returns(templateDto);

            //act
            var actionResult = controller.Get(1);
            var result = actionResult.Result as NoContentResult;

            //assert
            Assert.That(result, Is.InstanceOf<NoContentResult>());
        }

        [Test]
        public void Create_ProvidesCorrectParams_ReturnsCreated()
        {
            //arrange
            var dto = new CreateTemplateDto();
            service.Setup(s => s.Create(dto)).Returns(1);

            //act
            var actionResult = controller.Create(dto);
            var result = actionResult as CreatedResult;

            //assert
            Assert.That(result, Is.InstanceOf<CreatedResult>());
        }

        [Test]
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        [TestCase(4)]
        public void Create_ProvidesCorrectParams_ReturnsPathToNewEntity(int templateId)
        {
            //arrange
            var dto = new CreateTemplateDto();
            service.Setup(s => s.Create(dto)).Returns(templateId);

            //act
            var actionResult = controller.Create(dto);
            var result = actionResult as CreatedResult;

            //assert
            Assert.That(result.Location, Is.EqualTo($"template/{templateId}"));
        }

        [Test]
        public void Delete_ProvidesCorrectTemplateId_ReturnsNoContent()
        {
            //arrange
            int templateId = 1;
            service.Setup(s => s.Delete(templateId)).Returns(true);

            //act
            var actionResult = controller.Delete(templateId);
            var result = actionResult as NoContentResult;

            //assert
            Assert.That(result, Is.InstanceOf<NoContentResult>());
        }

        [Test]
        public void Delete_ProvidesIncorrectTemplateId_ReturnsNotFound()
        {
            //arrange
            int templateId = 1;
            service.Setup(s => s.Delete(templateId)).Returns(false);

            //act
            var actionResult = controller.Delete(templateId);
            var result = actionResult as NotFoundResult;

            //assert
            Assert.That(result, Is.InstanceOf<NotFoundResult>());
        }
    }
}
