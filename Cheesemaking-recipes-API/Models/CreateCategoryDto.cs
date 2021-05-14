using System.Collections.Generic;

namespace Cheesemaking_recipes_API.Models
{
    public class CreateCategoryDto
    {
        public string Name { get; set; }
        public List<CreateLabelDto> Labels { get; set; }
    }
}
