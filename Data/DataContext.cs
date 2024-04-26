global using Microsoft.EntityFrameworkCore;

namespace SuperHeroAPI.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            base.OnConfiguring(optionsBuilder);
            string connectionString = Environment.GetEnvironmentVariable("DATABASE_CONNECTION_STRING");

            if (string.IsNullOrEmpty(connectionString))
            {
                throw new InvalidOperationException("DATABASE_CONNECTION_STRING environment variable is not set.");
            }

            optionsBuilder.UseMySql(connectionString, new MySqlServerVersion(new Version()));
        }

        public DbSet<SuperHero> SuperHeroes { get; set; }

    }
}
