﻿using Cheesemaking_recipes_API.Models;
using Cheesemaking_recipes_API.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Cheesemaking_recipes_API.Controllers
{
    [Route("[Controller]")]
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
            var notesDtos = _service.Get();
            return Ok(notesDtos);
        }

        [HttpPost("{templateId}")]
        public ActionResult Create([FromBody] CreateNoteDto dto, [FromRoute] int templateId)
        {
            _service.Create(dto, templateId);
            return Ok();
        }
    }
}
