namespace Cheesemaking_recipes_API.Models
{
    public class NoteDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public TemplateDto Template { get; set; }
    }
}
