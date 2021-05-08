using Microsoft.EntityFrameworkCore;

namespace Cheesemaking_recipes_API.Entities
{
    public class ApiDbContext :DbContext
    {
        private string _connectionString = "Server=(localdb)\\mssqllocaldb;Database=CheesemakingRecipesApi;Trusted_Connection=True;";

        public DbSet<Note> Notes { get; set; }
        public DbSet<Template> Templates { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Label> Labels { get; set; }
        public DbSet<Input> Inputs { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString);
        }
    }
}
