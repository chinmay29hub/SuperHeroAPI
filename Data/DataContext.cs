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

            //base.OnConfiguring(optionsBuilder);
            //string connectionString = Environment.GetEnvironmentVariable("DATABASE_CONNECTION_STRING");

            //if (string.IsNullOrEmpty(connectionString))
            //{
            //    throw new InvalidOperationException("DATABASE_CONNECTION_STRING environment variable is not set.");
            //}

            //optionsBuilder.UseMySql(connectionString, new MySqlServerVersion(new Version()));
            base.OnConfiguring(optionsBuilder);

            string server = Environment.GetEnvironmentVariable("DATABASE_SERVER");
            string port = Environment.GetEnvironmentVariable("DATABASE_PORT");
            string database = Environment.GetEnvironmentVariable("DATABASE_NAME");
            string user = Environment.GetEnvironmentVariable("DATABASE_USER");
            string password = Environment.GetEnvironmentVariable("DATABASE_PASSWORD");

            if (string.IsNullOrEmpty(server) || string.IsNullOrEmpty(port) || string.IsNullOrEmpty(database) || string.IsNullOrEmpty(user) || string.IsNullOrEmpty(password))
            {
                throw new InvalidOperationException("One or more required environment variables are not set.");
            }

            string connectionString = $"server={server};port={port};database={database};user={user};password={password}";

            optionsBuilder.UseMySql(connectionString, new MySqlServerVersion(new Version()));
        
    }

        public DbSet<SuperHero> SuperHeroes { get; set; }

    }
}
