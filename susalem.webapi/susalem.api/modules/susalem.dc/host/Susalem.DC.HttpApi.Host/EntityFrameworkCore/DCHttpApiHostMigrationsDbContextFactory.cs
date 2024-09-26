using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Susalem.DC.EntityFrameworkCore;

public class DCHttpApiHostMigrationsDbContextFactory : IDesignTimeDbContextFactory<DCHttpApiHostMigrationsDbContext>
{
    public DCHttpApiHostMigrationsDbContext CreateDbContext(string[] args)
    {
        var configuration = BuildConfiguration();

        var builder = new DbContextOptionsBuilder<DCHttpApiHostMigrationsDbContext>()
            .UseMySql(configuration.GetConnectionString("DC"), MySqlServerVersion.LatestSupportedServerVersion);

        return new DCHttpApiHostMigrationsDbContext(builder.Options);
    }

    private static IConfigurationRoot BuildConfiguration()
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false);

        return builder.Build();
    }
}
