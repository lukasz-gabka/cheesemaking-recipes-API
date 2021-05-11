using System.Collections.Generic;

namespace Cheesemaking_recipes_API.Models
{
    public class CategoryDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Order { get; set; }
        public List<LabelDto> Labels { get; set; }
    }
}
