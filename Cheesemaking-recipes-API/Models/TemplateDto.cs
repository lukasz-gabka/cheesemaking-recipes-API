using System.Collections.Generic;

namespace Cheesemaking_recipes_API.Models
{
    public class TemplateDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<CategoryDto> Categories { get; set; }
    }
}
