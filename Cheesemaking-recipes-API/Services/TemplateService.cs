﻿using AutoMapper;
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

        public TemplateService(ApiDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public List<TemplateDto> Get()
        {
            var templates = _dbContext.Templates
                .Include(t => t.Categories)
                .ThenInclude(c => c.Labels)
                .ToList();

            var templatesDtos = _mapper.Map<List<TemplateDto>>(templates);
            SortProperties(templatesDtos);

            return templatesDtos;
        }

        public void SortProperties(List<TemplateDto> dtos)
        {
            foreach (var template in dtos)
            {
                template.Categories = template.Categories.OrderBy(c => c.Order).ToList();

                foreach (var category in template.Categories)
                {
                    category.Labels = category.Labels.OrderBy(l => l.Order).ToList();
                }
            }
        }

        public void Create(CreateTemplateDto dto)
        {
            var template = _mapper.Map<Template>(dto);
            CountProperties(template);

            _dbContext.Add(template);
            _dbContext.SaveChanges();
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
    }
}
