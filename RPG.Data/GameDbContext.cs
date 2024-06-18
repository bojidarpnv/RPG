using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using RPG.Data.Models;

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
            var builder = new ConfigurationBuilder()
                .AddUserSecrets<GameDbContext>();
            IConfiguration configuration = builder.Build();
            if (!optionsBuilder.IsConfigured)
            {
                //optionsBuilder
                //    .UseSqlServer(Configuration.ConnectionString);
                optionsBuilder
                    .UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            }
        }

    }
}
