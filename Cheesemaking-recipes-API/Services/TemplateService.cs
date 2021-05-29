using AutoMapper;
using Cheesemaking_recipes_API.Entities;
using Cheesemaking_recipes_API.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Cheesemaking_recipes_API.Services
{
    public class TemplateService
    {
        private readonly ApiDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly UserContextService _contextService;

        public TemplateService(ApiDbContext dbContext, IMapper mapper, UserContextService contextService)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _contextService = contextService;
        }

        public List<TemplateDto> GetAll()
        {
            var templates = _dbContext.Templates
                .Include(t => t.Categories.OrderBy(c => c.Order))
                .ThenInclude(c => c.Labels.OrderBy(l => l.Order))
                .Where(t => t.UserId == _contextService.GetUserId)
                .ToList();

            var templatesDtos = _mapper.Map<List<TemplateDto>>(templates);

            return templatesDtos;
        }

        public TemplateDto GetById(int id)
        {
            var template = _dbContext.Templates
                .Include(t => t.Categories.OrderBy(c => c.Order))
                .ThenInclude(c => c.Labels.OrderBy(l => l.Order))
                .Where(t => t.Id == id)
                .Where(t => t.UserId == _contextService.GetUserId)
                .SingleOrDefault();

            var templateDto = _mapper.Map<TemplateDto>(template);

            return templateDto;
        }

        public int Create(CreateTemplateDto dto)
        {
            var template = _mapper.Map<Template>(dto);
            template.UserId = _contextService.GetUserId;
            CountProperties(template);

            _dbContext.Add(template);
            _dbContext.SaveChanges();

            return template.Id;
        }

        private void CountProperties(Template template)
        {
            for (var i = 0; i < template.Categories.Count; i++)
            {
                template.Categories[i].Order = i + 1;

                for (var j = 0; j < template.Categories[i].Labels.Count; j++)
                {
                    template.Categories[i].Labels[j].Order = j + 1;
                }
            }
        }

        public void CreateDefaultTemplateForNewUser(int userId)
        {
            var template = GetDefaultTemplate();
            template.UserId = userId;

            _dbContext.Templates.Add(template);
            _dbContext.SaveChanges();
        }

        private Template GetDefaultTemplate()
        {
            return new Template()
            {
                Name = "Domyślny szablon",
                Categories = new List<Category>
                {
                    new Category()
                    {
                        Name = "Składniki",
                        Order = 1,
                        Labels =  new List<Label>()
                        {
                            new Label()
                            {
                                Name = "Rodzaj  mleka",
                                Order = 1
                            },
                            new Label()
                            {
                                Name = "Ilość  mleka",
                                Order = 2
                            },
                            new Label()
                            {
                                Name = "Chlorek wapnia",
                                Order = 3
                            },
                            new Label()
                            {
                                Name = "Rodzaj kultury bakteryjnej",
                                Order = 4
                            },
                            new Label()
                            {
                                Name = "Ilość kultury bakteryjnej",
                                Order = 5
                            },
                            new Label()
                            {
                                Name = "Rodzaj  podpuszczki",
                                Order = 6
                            },
                            new Label()
                            {
                                Name = "Ilość podpuszczki",
                                Order = 7
                            },
                            new Label()
                            {
                                Name = "Dodatki",
                                Order = 8
                            }
                        }
                    },
                    new Category()
                    {
                        Name = "Warzenie",
                        Order = 2,
                        Labels = new List<Label>()
                        {
                            new Label()
                            {
                                Name = "Temperatura dodania kultury",
                                Order = 1
                            },
                            new Label()
                            {
                                Name = "Czas zakwaszania",
                                Order = 2
                            },
                            new Label()
                            {
                                Name = "Temperatura dodania podpuszczki",
                                Order = 3
                            },
                            new Label()
                            {
                                Name = "Czas krzepnięcia",
                                Order = 4
                            }
                        }
                    },
                    new Category()
                    {
                        Name = "Krojenie skrzepu",
                        Order = 3,
                        Labels = new List<Label>()
                        {
                            new Label()
                            {
                                Name = "Rozmiar kostek skrzepu",
                                Order = 1
                            },
                            new Label()
                            {
                                Name = "Czas obkurczania skrzepu",
                                Order = 2
                            }
                        }
                    },
                    new Category()
                    {
                        Name = "Formowanie sera",
                        Order = 4,
                        Labels = new List<Label>()
                        {
                            new Label()
                            {
                                Name = "Czas formowania",
                                Order = 1
                            },
                            new Label()
                            {
                                Name = "Temperatura formowania",
                                Order = 2
                            }
                        }
                    },
                    new Category()
                    {
                        Name = "Solenie sera",
                        Order = 5,
                        Labels = new List<Label>()
                        {
                            new Label()
                            {
                                Name = "Ilość soli w solance",
                                Order = 1
                            },
                            new Label()
                            {
                                Name = "Ilość wody w solance",
                                Order = 2
                            },
                            new Label()
                            {
                                Name = "Czas solenia",
                                Order = 3
                            }
                        }
                    },
                    new Category()
                    {
                        Name = "Obserwacje",
                        Order = 6,
                        Labels = new List<Label>()
                        {
                            new Label()
                            {
                                Name = "Degustacja",
                                Order = 1
                            },
                            new Label()
                            {
                                Name = "Komentarz",
                                Order = 2
                            }
                        }
                    }
                }
            };
        }
    }
}
