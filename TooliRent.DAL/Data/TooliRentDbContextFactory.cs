using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace TooliRent.DAL.Data
{
    public class TooliRentDbContextFactory : IDesignTimeDbContextFactory<TooliRentDBContext>
    {
        public TooliRentDBContext CreateDbContext(string[] args)
        {
            var environment = Environment.GetEnvironmentVariable("DefaultConnection") ?? "Development";

            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .AddJsonFile($"appsettings.{environment}.json", optional: true)             
                .Build();

            var connectionString = configuration.GetConnectionString("DefaultConnection");

            if(string.IsNullOrEmpty(connectionString))
            {
                throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            }

            var optionsBuilder = new DbContextOptionsBuilder<TooliRentDBContext>();
            optionsBuilder.UseSqlServer(connectionString);

            return new TooliRentDBContext(optionsBuilder.Options);
        }
    }
}
