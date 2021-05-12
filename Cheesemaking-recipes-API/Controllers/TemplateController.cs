using Cheesemaking_recipes_API.Entities;
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

        public TemplateController(ApiDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Template>> Get()
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

            return Ok(templates);
        }
    }
}
