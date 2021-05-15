using AutoMapper;
using Cheesemaking_recipes_API.Entities;
using Cheesemaking_recipes_API.Models;
using Cheesemaking_recipes_API.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Cheesemaking_recipes_API.Controllers
{
    [Route("[Controller]")]
    public class TemplateController : ControllerBase
    {
        private readonly ApiDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly TemplateService _service;

        public TemplateController(ApiDbContext dbContext, IMapper mapper, TemplateService service)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _service = service;
        }

        [HttpGet]
        public ActionResult<IEnumerable<TemplateDto>> Get()
        {
            var templatesDtos = _service.Get();
            return Ok(templatesDtos);
        }

        [HttpPost]
        public ActionResult Create([FromBody] CreateTemplateDto dto)
        {
            _service.Create(dto);
            return Ok();
        }
    }
}
