using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Cheesemaking_recipes_API.Models
{
    public class TemplateDto
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
        [MinLength(1)]
        public IEnumerable<CategoryDto> Categories { get; set; }
    }
}
