using System.Text.Json.Serialization;

namespace Cheesemaking_recipes_API.Entities
{
    public class Input
    {
        public int Id { get; set; }
        public string Value { get; set; }
        public int Order { get; set; }
        public int NoteId { get; set; }
        [JsonIgnore]
        public virtual Note Note { get; set; }
    }
}
