using System.ComponentModel.DataAnnotations;

namespace Cheesemaking_recipes_API.Models
{
    public class InputDto
    {
        public int Id { get; set; }
        [Required]
        public string Value { get; set; }
        public int Order { get; set; }
    }
}
