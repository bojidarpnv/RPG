using Microsoft.EntityFrameworkCore;
using RPG.Models;

namespace RPG.Data
{
    public class GameDbContext : DbContext
    {
        public GameDbContext()
        {

        }

        public GameDbContext(DbContextOptions options)
            : base(options)
        {

        }
        public DbSet<Hero> Heroes { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder
                    .UseSqlServer(Configuration.ConnectionString);
            }
        }

    }
}
