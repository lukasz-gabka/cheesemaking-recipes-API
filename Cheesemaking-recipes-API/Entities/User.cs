using System.Collections.Generic;

namespace Cheesemaking_recipes_API.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string PasswordHash { get; set; }
        public virtual List<Template> Templates { get; set; }

    }
}
