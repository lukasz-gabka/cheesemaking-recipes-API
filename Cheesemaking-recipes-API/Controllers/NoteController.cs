using Cheesemaking_recipes_API.Models;
using Cheesemaking_recipes_API.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Cheesemaking_recipes_API.Controllers
{
    [Route("[Controller]")]
    [ApiController]
    public class NoteController : ControllerBase
    {
        private readonly NoteService _service;

        public NoteController(NoteService service)
        {
            _service = service;
        }

        [HttpGet]
        public ActionResult<IEnumerable<NoteDto>> Get()
        {
            var notesDtos = _service.GetAll();
            return Ok(notesDtos);
        }

        [HttpGet("{noteId}")]
        public ActionResult<NoteDto> Get([FromRoute] int noteId)
        {
            var noteDto = _service.GetById(noteId);
            return Ok(noteDto);
        }

        [HttpPost("{templateId}")]
        public ActionResult Create([FromBody] CreateNoteDto dto, [FromRoute] int templateId)
        {
            int id = _service.Create(dto, templateId);
            return Created($"note/{id}", null);
        }
    }
}
