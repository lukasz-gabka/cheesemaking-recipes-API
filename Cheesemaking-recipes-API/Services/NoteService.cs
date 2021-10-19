using AutoMapper;
using Cheesemaking_recipes_API.Entities;
using Cheesemaking_recipes_API.Exceptions;
using Cheesemaking_recipes_API.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Cheesemaking_recipes_API.Services
{
    public class NoteService : INoteService
    {
        private readonly ApiDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly UserContextService _contextService;

        public NoteService(ApiDbContext dbContext, IMapper mapper, UserContextService contextService)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _contextService = contextService;
        }

        public List<NoteDto> GetAll()
        {
            var notes = _dbContext.Notes
                .Include(n => n.Inputs.OrderBy(i => i.Order))
                .Include(n => n.Template)
                .ThenInclude(t => t.Categories.OrderBy(c => c.Order))
                .ThenInclude(c => c.Labels.OrderBy(l => l.Order))
                .Where(n => n.UserId == _contextService.GetUserId)
                .ToList();

            var notesDtos = _mapper.Map<List<NoteDto>>(notes);

            return notesDtos;
        }

        public NoteDto GetById(int id)
        {
            var note = _dbContext.Notes
                .Include(n => n.Inputs.OrderBy(i => i.Order))
                .Include(n => n.Template)
                .ThenInclude(t => t.Categories.OrderBy(c => c.Order))
                .ThenInclude(c => c.Labels.OrderBy(l => l.Order))
                .Where(n => n.Id == id)
                .Where(n => n.UserId == _contextService.GetUserId)
                .SingleOrDefault();

            var noteDto = _mapper.Map<NoteDto>(note);

            return noteDto;
        }

        public bool Update(int noteId, UpdateNoteDto dto)
        {
            var note = _dbContext.Notes
                .Include(n => n.Inputs.OrderBy(i => i.Order))
                .Include(n => n.Template)
                .ThenInclude(t => t.Categories.OrderBy(c => c.Order))
                .ThenInclude(c => c.Labels.OrderBy(l => l.Order))
                .Where(n => n.Id == noteId)
                .Where(n => n.UserId == _contextService.GetUserId)
                .SingleOrDefault();

            if (note is null)
            {
                return false;
            }

            if (note.Inputs.Count != dto.Inputs.Count)
            {
                throw new BadRequestException("Number of inputs does not match number of labels");
            }

            note.Name = dto.Name;
            for (int i = 0; i < note.Inputs.Count; i++)
            {
                note.Inputs[i].Value = dto.Inputs[i].Value;
            }

            _dbContext.SaveChanges();

            return true;
        }

        public bool Delete(int noteId)
        {
            var note = _dbContext.Notes
                .Where(n => n.UserId == _contextService.GetUserId)
                .FirstOrDefault(n => n.Id == noteId);

            if (note is null)
            {
                return false;
            }

            _dbContext.Notes.Remove(note);
            _dbContext.SaveChanges();

            return true;
        }

        public int Create(CreateNoteDto dto, int templateId)
        {
            var template = _dbContext.Templates
                .Include(t => t.Categories.OrderBy(c => c.Order))
                .ThenInclude(c => c.Labels.OrderBy(l => l.Order))
                .Where(t => t.UserId == _contextService.GetUserId)
                .Where(t => t.Id == templateId)
                .SingleOrDefault();

            if (template is null)
            {
                throw new BadRequestException("Template not found");
            }

            var inputs = _mapper.Map<List<Input>>(dto.Inputs);
            CountInputs(inputs);

            var note = new Note
            {
                Name = dto.Name,
                Template = template,
                Inputs = inputs,
                UserId = _contextService.GetUserId
            };

            var labelCounter = 0;
            foreach (var category in note.Template.Categories)
            {
                labelCounter += category.Labels.Count;
            }

            if (note.Inputs.Count != labelCounter)
            {
                throw new BadRequestException("Number of inputs does not match number of labels");
            }

            _dbContext.Add(note);
            _dbContext.SaveChanges();

            return note.Id;
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
