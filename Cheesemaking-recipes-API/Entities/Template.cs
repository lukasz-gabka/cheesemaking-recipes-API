using System.Collections.Generic;

namespace Cheesemaking_recipes_API.Entities
{
    public class Template
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual List<Category> Categories { get; set; }
        public virtual Note Note { get; set; }
    }
}
