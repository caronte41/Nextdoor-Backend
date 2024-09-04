using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using NextDoorBackend.SDK.Entities;
using Npgsql;

namespace NextDoorBackend.Data
{
    public class AppDbContext : DbContext
    {
        protected readonly IConfiguration Configuration;

        public AppDbContext(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            var connectionString = Configuration.GetConnectionString("WebApiDatabase");

            options.UseNpgsql(connectionString, o =>
            {
                o.CommandTimeout(40); // Set the command timeout to 180 seconds
            });
        }

        public DbSet<EmployeeEntity> Employees { get; set; }
    }
}
