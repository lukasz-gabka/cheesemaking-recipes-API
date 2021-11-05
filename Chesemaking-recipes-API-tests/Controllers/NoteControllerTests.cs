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
    class NoteControllerTests
    {
        private Mock<INoteService> service;
        private NoteController controller;
        private readonly List<NoteDto> notesDtos = new List<NoteDto>()
        {
            new NoteDto(),
            new NoteDto()
        };

        [SetUp]
        public void SetUp()
        {
            service = new Mock<INoteService>();
            controller = new NoteController(service.Object);
        }

        [Test]
        public void Get_EntitiesExist_ReturnsOk()
        {
            //arrange
            service.Setup(s => s.GetAll()).Returns(notesDtos);

            //act
            var actionResult = controller.Get();

            //assert
            Assert.That(actionResult.Result, Is.InstanceOf<OkObjectResult>());
        }

        [Test]
        public void Get_EntitiesExist_ReturnsAllEntities()
        {
            //arrange
            service.Setup(s => s.GetAll()).Returns(notesDtos);

            //act
            var actionResult = controller.Get();
            var result = actionResult.Result as OkObjectResult;
            List<NoteDto> notesResult = (List<NoteDto>)result.Value;

            //assert
            Assert.That(notesResult.Count, Is.EqualTo(2));
        }

        [Test]
        public void Get_EntitiesAreNull_ReturnsNoContent()
        {
            //arrange
            List<NoteDto> notesDtos = null;
            service.Setup(s => s.GetAll()).Returns(notesDtos);

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
            List<NoteDto> notesDtos = new List<NoteDto>();
            service.Setup(s => s.GetAll()).Returns(notesDtos);

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
            NoteDto noteDto = new NoteDto();
            service.Setup(s => s.GetById(1)).Returns(noteDto);

            //act
            var actionResult = controller.Get(1);

            //assert
            Assert.That(actionResult.Result, Is.InstanceOf<OkObjectResult>());
        }

        [Test]
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        public void Get_EntityWithIdSpecifiedExist_ReturnsEntity(int noteId)
        {
            //arrange
            NoteDto noteDto = new NoteDto() { Id = noteId };
            service.Setup(s => s.GetById(noteId)).Returns(noteDto);

            //act
            var actionResult = controller.Get(noteId);
            var result = actionResult.Result as OkObjectResult;
            NoteDto noteResult = (NoteDto)result.Value;

            //assert
            Assert.That(noteResult.Id, Is.EqualTo(noteId));
        }

        [Test]
        public void Get_EntityWithIdSpecifiedDoesntExist_ReturnsNoContent()
        {
            //arrange
            NoteDto noteDto = null;
            service.Setup(s => s.GetById(1)).Returns(noteDto);

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
            var dto = new CreateNoteDto();
            int templateId = 1;
            service.Setup(s => s.Create(dto, templateId)).Returns(1);

            //act
            var actionResult = controller.Create(dto, templateId);
            var result = actionResult as CreatedResult;

            //assert
            Assert.That(result, Is.InstanceOf<CreatedResult>());
        }

        [Test]
        [TestCase(1, 1)]
        [TestCase(1, 2)]
        [TestCase(2, 3)]
        [TestCase(2, 4)]
        public void Create_ProvidesCorrectParams_ReturnsPathToNewEntity(int templateId, int noteId)
        {
            //arrange
            var dto = new CreateNoteDto();
            service.Setup(s => s.Create(dto, templateId)).Returns(noteId);

            //act
            var actionResult = controller.Create(dto, templateId);
            var result = actionResult as CreatedResult;

            //assert
            Assert.That(result.Location, Is.EqualTo($"note/{noteId}"));
        }

        [Test]
        public void Delete_ProvidesCorrectNoteId_ReturnsNoContent()
        {
            //arrange
            int noteId = 1;
            service.Setup(s => s.Delete(noteId)).Returns(true);

            //act
            var actionResult = controller.Delete(noteId);
            var result = actionResult as NoContentResult;

            //assert
            Assert.That(result, Is.InstanceOf<NoContentResult>());
        }

        [Test]
        public void Delete_ProvidesIncorrectNoteId_ReturnsNotFound()
        {
            //arrange
            int noteId = 1;
            service.Setup(s => s.Delete(noteId)).Returns(false);

            //act
            var actionResult = controller.Delete(noteId);
            var result = actionResult as NotFoundResult;

            //assert
            Assert.That(result, Is.InstanceOf<NotFoundResult>());
        }

        [Test]
        public void Update_ProvidesCorrectParams_ReturnsNoContent()
        {
            //arrange
            int noteId = 1;
            var dto = new UpdateNoteDto();
            service.Setup(s => s.Update(noteId, dto)).Returns(true);

            //act
            var actionResult = controller.Update(noteId, dto);
            var result = actionResult as NoContentResult;

            //assert
            Assert.That(result, Is.InstanceOf<NoContentResult>());
        }

        [Test]
        public void Update_ProvidesIncorrectParams_ReturnsNoContent()
        {
            //arrange
            int noteId = 1;
            var dto = new UpdateNoteDto();
            service.Setup(s => s.Update(noteId, dto)).Returns(false);

            //act
            var actionResult = controller.Update(noteId, dto);
            var result = actionResult as NotFoundResult;

            //assert
            Assert.That(result, Is.InstanceOf<NotFoundResult>());
        }
    }
}
