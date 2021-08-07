using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Cheesemaking_recipes_API.Models
{
    public class UpdateNoteDto
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
        [MinLength(1)]
        public List<UpdateInputDto> Inputs { get; set; }
    }
}
