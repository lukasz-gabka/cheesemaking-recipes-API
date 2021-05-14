using AutoMapper;
using Cheesemaking_recipes_API.Entities;
using Cheesemaking_recipes_API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Cheesemaking_recipes_API.Controllers
{
    [Route("[Controller]")]
    public class TemplateController : ControllerBase
    {
        private readonly ApiDbContext _dbContext;
        private readonly IMapper _mapper;

        public TemplateController(ApiDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<TemplateDto>> Get()
        {
            var templates = _dbContext.Templates
                .Include(t => t.Categories)
                .ThenInclude(c => c.Labels)
                .ToList();

            foreach (var template in templates)
            {
                template.Categories = template.Categories.OrderBy(c => c.Order).ToList();

                foreach (var category in template.Categories)
                {
                    category.Labels = category.Labels.OrderBy(l => l.Order).ToList();
                }
            }

            var templatesDtos = _mapper.Map<List<TemplateDto>>(templates);

            return Ok(templatesDtos);
        }

        [HttpPost]
        public ActionResult Create([FromBody] CreateTemplateDto dto)
        {
            var template = _mapper.Map<Template>(dto);

            for (var i = 0; i < template.Categories.Count; i++)
            {
                template.Categories[i].Order = i + 1;

                for (var j = 0; j < template.Categories[i].Labels.Count; j++)
                {
                    template.Categories[i].Labels[j].Order = j + 1;
                }
            }

            _dbContext.Add(template);
            _dbContext.SaveChanges();

            return Ok();
        }
    }
}
