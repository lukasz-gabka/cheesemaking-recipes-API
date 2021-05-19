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
    public class NoteController : ControllerBase
    {
        private readonly ApiDbContext _dbContext;
        private readonly IMapper _mapper;

        public NoteController(ApiDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<NoteDto>> Get()
        {
            var notes = _dbContext.Notes
                .Include(n => n.Inputs)
                .Include(n => n.Template)
                .ThenInclude(t => t.Categories.OrderBy(c => c.Order))
                .ThenInclude(c => c.Labels.OrderBy(l => l.Order))
                .ToList();

            var notesDtos = _mapper.Map<List<NoteDto>>(notes);

            return notesDtos;
        }

        [HttpPost("{templateId}")]
        public ActionResult Create([FromBody] CreateNoteDto dto, [FromRoute] int templateId)
        {
            var template = _dbContext
                .Templates
                .Include(t => t.Categories.OrderBy(c => c.Order))
                .ThenInclude(c => c.Labels.OrderBy(l => l.Order))
                .SingleOrDefault(t => t.Id == templateId);

            var note = new Note();
            note.Name = dto.Name;
            note.Template = template;

            var inputs = _mapper.Map<List<Input>>(dto.Inputs);
            CountInputs(inputs);
            note.Inputs = inputs;

            _dbContext.Add(note);
            _dbContext.SaveChanges();

            return Ok();
        }

        private void CountInputs(List<Input> inputs)
        {
            for (var i = 0; i < inputs.Count; i++)
            {
                inputs[i].Order = i + 1;
            }
        }
    }
}
