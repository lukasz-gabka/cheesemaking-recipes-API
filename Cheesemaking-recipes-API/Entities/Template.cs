using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Cheesemaking_recipes_API.Entities
{
    public class Template
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual List<Category> Categories { get; set; }
        public virtual List<Note> Notes { get; set; }
        public int UserId { get; set; }
        [JsonIgnore]
        public virtual User User { get; set; }
    }
}
