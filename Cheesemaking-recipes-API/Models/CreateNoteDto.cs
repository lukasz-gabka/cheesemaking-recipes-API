using System.Collections.Generic;

namespace Cheesemaking_recipes_API.Models
{
    public class CreateNoteDto
    {
        public string Name { get; set; }
        public List<InputDto> Inputs { get; set; }
    }
}
