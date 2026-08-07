using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace FleetErp.Infrastructure.Persistence;

public class FleetErpDbContextFactory : IDesignTimeDbContextFactory<FleetErpDbContext>
{
    public FleetErpDbContext CreateDbContext(string[] args)
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../FleetErp.Api"))
            .AddJsonFile("appsettings.json", optional: false)
            .AddJsonFile("appsettings.Development.json", optional: true)
            .Build();

        var connectionString = configuration.GetConnectionString("DefaultConnection")
            ?? "Server=localhost;Database=FleetErp;User=root;Password=root;";

        var optionsBuilder = new DbContextOptionsBuilder<FleetErpDbContext>();
        optionsBuilder.UseMySQL(connectionString);

        return new FleetErpDbContext(optionsBuilder.Options);
    }
}
