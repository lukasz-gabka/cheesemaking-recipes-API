using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Cheesemaking_recipes_API.Models
{
    public class CreateCategoryDto
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
        [Required]
        [MinLength(1)]
        public IEnumerable<CreateLabelDto> Labels { get; set; }
    }
}
