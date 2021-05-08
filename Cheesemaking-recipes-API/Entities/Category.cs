using System.Collections.Generic;

namespace Cheesemaking_recipes_API.Entities
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int TemplateId { get; set; }
        public virtual Template Template { get; set; }
        public virtual List<Label> Labels { get; set; }
    }
}
