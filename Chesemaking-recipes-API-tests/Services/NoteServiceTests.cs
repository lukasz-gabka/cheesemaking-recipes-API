using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Cheesemaking_recipes_API;
using Cheesemaking_recipes_API.Entities;
using Cheesemaking_recipes_API.Exceptions;
using Cheesemaking_recipes_API.Models;
using Cheesemaking_recipes_API.Services;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;

namespace Chesemaking_recipes_API_tests.Services
{
    [TestFixture]
    class NoteServiceTests
    {
        private Mock<ApiDbContext> dbContext;
        private IMapper mapper;
        private Mock<IUserContextService> userContextService;
        private INoteService service;
        private CreateNoteDto createNoteDto;
        private UpdateNoteDto updateNoteDto;

        [SetUp]
        public void SetUp()
        {
            dbContext = new Mock<ApiDbContext>();
            var config  = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>());
            mapper = new Mapper(config);
            userContextService = new Mock<IUserContextService>();
            service = new NoteService(dbContext.Object, mapper, userContextService.Object);
            createNoteDto = new CreateNoteDto()
            {
                Name = "Note1",
                Inputs = new List<InputDto>()
                {
                    new InputDto()
                    {
                        Value = "Value 1"
                    }
                }
            };

            updateNoteDto = new UpdateNoteDto()
            {
                Name = "Updated note1",
                Inputs = new List<UpdateInputDto>()
                {
                    new UpdateInputDto()
                    {
                        Value = "Updated value 1"
                    }
                }
            };
        }

        [Test]
        public void GetAll_EntitiesExist_ReturnsAllEntities()
        {
            //arrange
            SetUpDbContextForGettingNotes(GetNoteList());

            //act
            var result = service.GetAll();

            //assert
            Assert.That(result.Count, Is.EqualTo(2));
            Assert.That(result[0].Name, Is.EqualTo("Name 1"));
            Assert.That(result[1].Name, Is.EqualTo("Name 2"));
        }

        [Test]
        public void GetAll_EntitiesAreEmpty_ReturnsEmptyList()
        {
            //arrange
            SetUpDbContextForGettingNotes(new List<Note>());

            //act
            var result = service.GetAll();

            //assert
            Assert.That(result.Count, Is.EqualTo(0));
        }

        [Test]
        public void GetById_ProvidesCorrectNoteId_ReturnsEntityWithIdSpecified()
        {
            //arrange
            SetUpDbContextForGettingNotes(GetNoteList());

            //act
            var result = service.GetById(1);

            //assert
            Assert.That(result.Id, Is.EqualTo(1));
            Assert.That(result.Name, Is.EqualTo("Name 1"));
        }

        [Test]
        public void GetById_ProvidesIncorrectNoteId_ReturnsNull()
        {
            //arrange
            SetUpDbContextForGettingNotes(GetNoteList());

            //act
            var result = service.GetById(3);

            //assert
            Assert.That(result, Is.Null);
        }

        [Test]
        public void Update_ProvidesCorrectParams_ReturnsTrue()
        {
            //arrange
            int noteId = 1;
            SetUpDbContextForGettingNotes(GetNoteList());

            //act
            var result = service.Update(noteId, updateNoteDto);

            //assert
            Assert.That(result, Is.True);
        }

        [Test]
        public void Update_ProvidesIncorrectNoteId_ReturnsFalse()
        {
            //arrange
            int noteId = 5;
            SetUpDbContextForGettingNotes(GetNoteList());

            //act
            var result = service.Update(noteId, updateNoteDto);

            //assert
            Assert.That(result, Is.False);
        }

        [Test]
        public void Update_ProvidesUpdateNoteDtoWithIncorrectInputsCount_ThrowsBadRequestException()
        {
            //arrange
            int noteId = 1;
            updateNoteDto.Inputs.Add(new UpdateInputDto());
            SetUpDbContextForGettingNotes(GetNoteList());

            //act, assert
            Assert.Throws<BadRequestException>(() => service.Update(noteId, updateNoteDto));
        }

        [Test]
        public void Delete_ProvidesCorrectNoteId_ReturnsTrue()
        {
            //arrange
            int noteId = 1;
            SetUpDbContextForGettingNotes(GetNoteList());

            //act
            var result = service.Delete(noteId);

            //assert
            Assert.That(result, Is.True);
        }

        [Test]
        public void Delete_ProvidesIncorrectNoteId_ReturnsFalse()
        {
            //arrange
            int noteId = 5;
            SetUpDbContextForGettingNotes(GetNoteList());

            //act
            var result = service.Delete(noteId);

            //assert
            Assert.That(result, Is.False);
        }

        [Test]
        public void Create_ProvidesCorrectParams_SavesNewEntity()
        {
            //arrange
            int templateId = 1;
            SetUpDbContextForGettingNotes(GetNoteList());
            SetUpDbContextForGettingTemplates();

            //act
            var result = service.Create(createNoteDto, templateId);

            //assert
            dbContext.Verify(d => d.Add(It.IsAny<Note>()));
            dbContext.Verify(d => d.SaveChanges());
        }

        [Test]
        public void Create_ProvidesIncorrectTemplateId_ThrowsBadRequestException()
        {
            //arrange
            int templateId = 5;
            SetUpDbContextForGettingNotes(GetNoteList());
            SetUpDbContextForGettingTemplates();

            //act, assert
            Assert.Throws<BadRequestException>(() => service.Create(createNoteDto, templateId));
        }

        [Test]
        public void Create_ProvidesIncorrectInputsCountInCreateNoteDto_ThrowsBadRequestException()
        {
            //arrange
            int templateId = 1;
            createNoteDto.Inputs.Add(new InputDto());
            SetUpDbContextForGettingNotes(GetNoteList());
            SetUpDbContextForGettingTemplates();

            //act, assert
            Assert.Throws<BadRequestException>(() => service.Create(createNoteDto, templateId));
        }

        private void SetUpDbContextForGettingNotes(List<Note> notes)
        {
            var mockSet = GetNoteMockSet(notes);
            dbContext.Setup(d => d.Notes).Returns(mockSet.Object);
        }

        private void SetUpDbContextForGettingTemplates()
        {
            var mockSet = GetTemplateMockSet(GetTemplateList());
            dbContext.Setup(d => d.Templates).Returns(mockSet.Object);
        }

        private static Mock<DbSet<Note>> GetNoteMockSet(List<Note> noteList)
        {
            var notes = noteList.AsQueryable();

            var mockSet = new Mock<DbSet<Note>>();
            mockSet.As<IQueryable<Note>>().Setup(m => m.Provider).Returns(notes.Provider);
            mockSet.As<IQueryable<Note>>().Setup(m => m.Expression).Returns(notes.Expression);
            mockSet.As<IQueryable<Note>>().Setup(m => m.ElementType).Returns(notes.ElementType);
            mockSet.As<IQueryable<Note>>().Setup(m => m.GetEnumerator()).Returns(notes.GetEnumerator());

            return mockSet;
        }

        private List<Note> GetNoteList()
        {
            return new List<Note>()
            {
                new Note()
                {
                    Id = 1,
                    Name = "Name 1",
                    Inputs = new List<Input>()
                    {
                        new Input()
                        {
                            Value = "Value 1"
                        }
                    }
                },
                new Note()
                {
                    Id = 2,
                    Name = "Name 2",
                    Template = GetTemplate(),
                    Inputs = new List<Input>()
                    {
                        new Input()
                        {
                            Value = "Value 1"
                        }
                    }
                }
            };
        }

        private static Mock<DbSet<Template>> GetTemplateMockSet(List<Template> templates)
        {
            var templatesQueryable = templates.AsQueryable();

            var mockSet = new Mock<DbSet<Template>>();
            mockSet.As<IQueryable<Template>>().Setup(m => m.Provider).Returns(templatesQueryable.Provider);
            mockSet.As<IQueryable<Template>>().Setup(m => m.Expression).Returns(templatesQueryable.Expression);
            mockSet.As<IQueryable<Template>>().Setup(m => m.ElementType).Returns(templatesQueryable.ElementType);
            mockSet.As<IQueryable<Template>>().Setup(m => m.GetEnumerator()).Returns(templatesQueryable.GetEnumerator());

            return mockSet;
        }

        private static List<Template> GetTemplateList()
        {
            return new List<Template>()
            {
                GetTemplate()
            };
        }

        private static Template GetTemplate()
        {
            return new Template()
            {
                Id = 1,
                Name = "Template 1",
                Categories = new List<Category>
                {
                    new Category()
                    {
                        Id = 1,
                        Name = "Category 1",
                        Labels = new List<Label>()
                        {
                            new Label()
                            {
                                Id = 1,
                                Name = "Label 1"
                            }
                        }
                    }
                }
            };
        }
    }
}
