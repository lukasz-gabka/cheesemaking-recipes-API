using System.ComponentModel.DataAnnotations;

namespace Cheesemaking_recipes_API.Models
{
    public class CreateLabelDto
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
    }
}
