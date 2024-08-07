using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Susalem.WarehouseService.EntityFrameworkCore;

/* This class is needed for EF Core console commands
 * (like Add-Migration and Update-Database commands) */
public class WarehouseServiceDbContextFactory : IDesignTimeDbContextFactory<WarehouseServiceDbContext>
{
    public WarehouseServiceDbContext CreateDbContext(string[] args)
    {
        WarehouseServiceEfCoreEntityExtensionMappings.Configure();

        var configuration = BuildConfiguration();

        var builder = new DbContextOptionsBuilder<WarehouseServiceDbContext>()
            .UseMySql(configuration.GetConnectionString("Default"), MySqlServerVersion.LatestSupportedServerVersion);

        return new WarehouseServiceDbContext(builder.Options);
    }

    private static IConfigurationRoot BuildConfiguration()
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../Susalem.WarehouseService.HttpApi.Host/"))
            .AddJsonFile("appsettings.json", optional: false);

        return builder.Build();
    }
}
