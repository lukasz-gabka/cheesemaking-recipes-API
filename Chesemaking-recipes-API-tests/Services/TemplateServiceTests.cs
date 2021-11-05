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
    class TemplateServiceTests
    {
        private Mock<ApiDbContext> dbContext;
        private IMapper mapper;
        private Mock<IUserContextService> userContextService;
        private ITemplateService service;

        [SetUp]
        public void SetUp()
        {
            dbContext = new Mock<ApiDbContext>();
            var config = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>());
            mapper = new Mapper(config);
            userContextService = new Mock<IUserContextService>();
            service = new TemplateService(dbContext.Object, mapper, userContextService.Object);
        }

        [Test]
        public void GetAll_EntitiesExist_ReturnsAllEntities()
        {
            //arrange
            SetUpDbContextForGettingTemplates(GetTemplateList());

            //act
            var result = service.GetAll();

            //assert
            Assert.That(result.Count, Is.EqualTo(2));
            Assert.That(result[0].Name, Is.EqualTo("Template 1"));
            Assert.That(result[1].Name, Is.EqualTo("Template 2"));
        }

        [Test]
        public void GetAll_EntitiesAreEmpty_ReturnsEmptyList()
        {
            //arrange
            SetUpDbContextForGettingTemplates(new List<Template>());

            //act
            var result = service.GetAll();

            //assert
            Assert.That(result.Count, Is.EqualTo(0));
        }

        [Test]
        public void GetById_ProvidesCorrectTemplateId_ReturnsEntityWithIdSpecified()
        {
            //arrange
            SetUpDbContextForGettingTemplates(GetTemplateList());

            //act
            var result = service.GetById(1);

            //assert
            Assert.That(result.Id, Is.EqualTo(1));
            Assert.That(result.Name, Is.EqualTo("Template 1"));
        }

        [Test]
        public void GetById_ProvidesIncorrectTemplateId_ReturnsNull()
        {
            //arrange
            SetUpDbContextForGettingTemplates(GetTemplateList());

            //act
            var result = service.GetById(3);

            //assert
            Assert.That(result, Is.Null);
        }

        [Test]
        public void Delete_ProvidesCorrectTemplateId_ReturnsTrue()
        {
            //arrange
            int templateId = 1;
            SetUpDbContextForGettingTemplates(GetTemplateList());
            SetUpDbContextForGettingNotes(GetNoteList());

            //act
            var result = service.Delete(templateId);

            //assert
            Assert.That(result, Is.True);
        }

        [Test]
        public void Delete_ProvidesIncorrectTemplateId_ReturnsFalse()
        {
            //arrange
            int templateId = 5;
            SetUpDbContextForGettingTemplates(GetTemplateList());
            SetUpDbContextForGettingNotes(GetNoteList());

            //act
            var result = service.Delete(templateId);

            //assert
            Assert.That(result, Is.False);
        }

        [Test]
        public void Delete_ProvidesCorrectTemplateIdButTemplateIsUsedByNote_ThrowsBadRequestException()
        {
            //arrange
            int templateId = 2;
            SetUpDbContextForGettingTemplates(GetTemplateList());
            SetUpDbContextForGettingNotes(GetNoteList());

            //act, assert
            Assert.Throws<BadRequestException>(() => service.Delete(templateId));
        }

        [Test]
        public void Create_ProvidesCorrectParams_SavesNewEntity()
        {
            //arrange
            var dto = new CreateTemplateDto()
            {
                Name = "New template",
                Categories = new List<CreateCategoryDto>()
                {
                    new CreateCategoryDto()
                    {
                        Name = "New category",
                        Labels = new List<CreateLabelDto>()
                        {
                            new CreateLabelDto()
                            {
                                Name = "New label"
                            }
                        }
                    }
                }
            };
            SetUpDbContextForGettingTemplates(GetTemplateList());

            //act
            var result = service.Create(dto);

            //assert
            dbContext.Verify(d => d.Add(It.IsAny<Template>()));
            dbContext.Verify(d => d.SaveChanges());
        }

        [Test]
        public void CreateDefaultTemplateForNewUser_ProvidesUserId_AddsNewDefaultTemplate()
        {
            //arrange
            int userId = 1;
            SetUpDbContextForGettingTemplates(GetTemplateList());

            //act
            service.CreateDefaultTemplateForNewUser(userId);

            //assert
            dbContext.Verify(d => d.Add(It.IsAny<Template>()));
            dbContext.Verify(d => d.SaveChanges());
        }

        private void SetUpDbContextForGettingTemplates(List<Template> templates)
        {
            var mockSet = GetTemplateMockSet(templates);
            dbContext.Setup(d => d.Templates).Returns(mockSet.Object);
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
                new Template()
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
                },
                new Template()
                {
                    Id = 2,
                    Name = "Template 2",
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
                }
            };
        }

        private void SetUpDbContextForGettingNotes(List<Note> notes)
        {
            var mockSet = GetNoteMockSet(notes);
            dbContext.Setup(d => d.Notes).Returns(mockSet.Object);
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
                    },
                    TemplateId = 2
                }
            };
        }
    }
}
