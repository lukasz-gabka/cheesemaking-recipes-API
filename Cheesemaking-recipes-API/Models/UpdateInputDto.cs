using System.ComponentModel.DataAnnotations;

namespace Cheesemaking_recipes_API.Models
{
    public class UpdateInputDto
    {
        [Required]
        public string Value { get; set; }
    }
}