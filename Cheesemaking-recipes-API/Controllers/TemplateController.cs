using Cheesemaking_recipes_API.Models;
using Cheesemaking_recipes_API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Cheesemaking_recipes_API.Controllers
{
    [Route("[Controller]")]
    [ApiController]
    [Authorize]
    public class TemplateController : ControllerBase
    {
        private readonly ITemplateService _service;

        public TemplateController(ITemplateService service)
        {
            _service = service;
        }

        [HttpGet]
        public ActionResult<IEnumerable<TemplateDto>> Get()
        {
            var templatesDtos = _service.GetAll();
            if (templatesDtos is null || templatesDtos.Count == 0)
            {
                return NoContent();
            }

            return Ok(templatesDtos);
        }

        [HttpGet("{templateId}")]
        public ActionResult<TemplateDto> Get([FromRoute] int templateId)
        {
            var templateDto = _service.GetById(templateId);
            if (templateDto is null)
            {
                return NoContent();
            }

            return Ok(templateDto);
        }

        [HttpPost]
        public ActionResult Create([FromBody] CreateTemplateDto dto)
        {
            int id = _service.Create(dto);
            return Created($"template/{id}", null);
        }

        [HttpDelete("{templateId}")]
        public ActionResult Delete([FromRoute] int templateId)
        {
            var isDeleted = _service.Delete(templateId);

            if (isDeleted)
            {
                return NoContent();
            }

            return NotFound();
        }
    }
}
