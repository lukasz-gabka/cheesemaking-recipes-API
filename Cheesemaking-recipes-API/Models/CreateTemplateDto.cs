using System.Collections.Generic;

namespace Cheesemaking_recipes_API.Models
{
    public class CreateTemplateDto
    {
        public string Name { get; set; }
        public List<CreateCategoryDto> Categories { get; set; }
    }
}
