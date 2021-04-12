using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace App.Domain.Helpers
{
    public class DataContext : DbContext
    {
        protected readonly IConfigurationRoot configuration;

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            configuration = new ConfigurationBuilder()
                           .SetBasePath(System.AppDomain.CurrentDomain.BaseDirectory)
                           .AddJsonFile("appsettings.json")
                           .Build();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseNpgsql(configuration.GetConnectionString("AppDatabase"),
                    npgsqlOptionsAction: psqlOptions =>
                    {
                        psqlOptions.EnableRetryOnFailure();
                    }
                );
        }

        public DbSet<User> User { get; set; }

        public DbSet<AuditLog> AuditLog { get; set; }

    }
}
