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
        private readonly TemplateService _service;

        public TemplateController(TemplateService service)
        {
            _service = service;
        }

        [HttpGet]
        public ActionResult<IEnumerable<TemplateDto>> Get()
        {
            var templatesDtos = _service.GetAll();
            return Ok(templatesDtos);
        }

        [HttpGet("{templateId}")]
        public ActionResult<TemplateDto> Get([FromRoute] int templateId)
        {
            var templateDto = _service.GetById(templateId);
            return Ok(templateDto);
        }

        [HttpPost]
        public ActionResult Create([FromBody] CreateTemplateDto dto)
        {
            int id = _service.Create(dto);
            return Created($"template/{id}", null);
        }
    }
}
