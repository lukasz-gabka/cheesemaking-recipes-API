namespace Cheesemaking_recipes_API.Models
{
    public class RegistrationDto
    {
        public string Email { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
