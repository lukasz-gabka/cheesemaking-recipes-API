using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Cheesemaking_recipes_API.Models
{
    public class NoteDto
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
        [Required]
        public TemplateDto Template { get; set; }
        [MinLength(1)]
        public IEnumerable<InputDto> Inputs { get; set; }
    }
}
