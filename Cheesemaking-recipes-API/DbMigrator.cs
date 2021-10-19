using Cheesemaking_recipes_API.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Cheesemaking_recipes_API
{
    public class DbMigrator
    {
        private readonly ApiDbContext _dbContext;

        public DbMigrator(ApiDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Migrate()
        {
            if (_dbContext.Database.CanConnect())
            {
                var pendingMigrations = _dbContext.Database.GetPendingMigrations();
                if (pendingMigrations != null && pendingMigrations.Any())
                {
                    _dbContext.Database.Migrate();
                }
            }
        }
    }
}