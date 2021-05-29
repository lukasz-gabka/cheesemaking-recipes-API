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

        private int GetUserId()
        {
            return (int) _contextService.GetUserId;
        }
    }
}
