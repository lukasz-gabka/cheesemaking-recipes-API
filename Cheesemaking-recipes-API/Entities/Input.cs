using System.Text.Json.Serialization;

namespace Cheesemaking_recipes_API.Entities
{
    public class Input
    {
        public int Id { get; set; }
        public string Value { get; set; }
        public int LabelId { get; set; }
        [JsonIgnore]
        public virtual Label Label { get; set; }
    }
}
