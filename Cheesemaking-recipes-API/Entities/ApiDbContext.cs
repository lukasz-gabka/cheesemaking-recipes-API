using Microsoft.EntityFrameworkCore;

namespace Cheesemaking_recipes_API.Entities
{
    public class ApiDbContext : DbContext
    {
        public ApiDbContext()
        {
        }

        public ApiDbContext(DbContextOptions<ApiDbContext> options) : base(options)
        {
        }

        public virtual DbSet<Note> Notes { get; set; }
        public virtual DbSet<Template> Templates { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Label> Labels { get; set; }
        public DbSet<Input> Inputs { get; set; }
        public virtual DbSet<User> Users { get; set; }
    }
}
