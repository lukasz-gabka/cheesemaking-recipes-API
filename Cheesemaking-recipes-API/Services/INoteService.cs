using Cheesemaking_recipes_API.Models;
using System.Collections.Generic;

namespace Cheesemaking_recipes_API.Services
{
    public interface INoteService
    {
        int Create(CreateNoteDto dto, int templateId);
        bool Delete(int noteId);
        List<NoteDto> GetAll();
        NoteDto GetById(int id);
        bool Update(int noteId, UpdateNoteDto dto);
    }
}