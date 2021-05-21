using System.ComponentModel.DataAnnotations;

namespace Cheesemaking_recipes_API.Models
{
    public class LabelDto
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
        public int Order { get; set; }
    }
}
