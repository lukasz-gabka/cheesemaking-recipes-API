using Microsoft.EntityFrameworkCore;

namespace Cheesemaking_recipes_API.Entities
{
    public class ApiDbContext : DbContext
    {
        public ApiDbContext(DbContextOptions<ApiDbContext> options) : base(options)
        {
        }

        public DbSet<Note> Notes { get; set; }
        public DbSet<Template> Templates { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Label> Labels { get; set; }
        public DbSet<Input> Inputs { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
