using System.Text.Json.Serialization;

namespace Cheesemaking_recipes_API.Entities
{
    public class Label
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Order { get; set; }
        public int CategoryId { get; set; }
        [JsonIgnore]
        public virtual Category Category { get; set; }
        public virtual Input Input { get; set; }
    }
}
