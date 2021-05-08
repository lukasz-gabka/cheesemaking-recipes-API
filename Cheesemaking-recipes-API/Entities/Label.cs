namespace Cheesemaking_recipes_API.Entities
{
    public class Label
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }
        public virtual Input Input { get; set; }
    }
}
