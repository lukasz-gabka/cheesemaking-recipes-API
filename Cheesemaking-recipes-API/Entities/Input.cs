namespace Cheesemaking_recipes_API.Entities
{
    public class Input
    {
        public int Id { get; set; }
        public string Value { get; set; }
        public int LabelId { get; set; }
        public virtual Label Label { get; set; }
    }
}
