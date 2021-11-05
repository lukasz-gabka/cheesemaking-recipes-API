using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Cheesemaking_recipes_API.Models
{
    public class CreateNoteDto
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
        [MinLength(1)]
        public List<InputDto> Inputs { get; set; }
    }
}
