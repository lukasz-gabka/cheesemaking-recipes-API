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
    public class NoteController : ControllerBase
    {
        private readonly INoteService _service;

        public NoteController(INoteService service)
        {
            _service = service;
        }

        [HttpGet]
        public ActionResult<IEnumerable<NoteDto>> Get()
        {
            var notesDtos = _service.GetAll();
            if (notesDtos is null || notesDtos.Count == 0)
            {
                return NoContent();
            }

            return Ok(notesDtos);

        }

        [HttpGet("{noteId}")]
        public ActionResult<NoteDto> Get([FromRoute] int noteId)
        {
            var noteDto = _service.GetById(noteId);
            if (noteDto is null)
            {
                return NoContent();
            }

            return Ok(noteDto);
        }

        [HttpPost("{templateId}")]
        public ActionResult Create([FromBody] CreateNoteDto dto, [FromRoute] int templateId)
        {
            int id = _service.Create(dto, templateId);
            return Created($"note/{id}", null);
        }

        [HttpDelete("{noteId}")]
        public ActionResult Delete([FromRoute] int noteId)
        {
            var isDeleted = _service.Delete(noteId);

            if (isDeleted)
            {
                return NoContent();
            }

            return NotFound();
        }

        [HttpPut("{noteId}")]
        public ActionResult Update([FromRoute] int noteId, [FromBody] UpdateNoteDto noteDto)
        {
            var isUpdated = _service.Update(noteId, noteDto);

            if (isUpdated)
            {
                return NoContent();
            }

            return NotFound();
        }
    }
}
