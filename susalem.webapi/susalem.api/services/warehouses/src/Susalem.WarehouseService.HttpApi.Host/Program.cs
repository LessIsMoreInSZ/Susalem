using Microsoft.AspNetCore.Builder;

using Serilog;

using SusalemAbp.Shared.Hosting.AspNetCore;

using System;
using System.Threading.Tasks;

namespace Susalem.WarehouseService;

public class Program
{
    public async static Task<int> Main(string[] args)
    {

        var assemblyName = typeof(Program).Assembly.GetName().Name;

        SerilogConfigurationHelper.Configure(assemblyName);

        try
        {
            Log.Information($"Starting {assemblyName}.");
            var app = await ApplicationBuilderHelper
                .BuildApplicationAsync<WarehouseServiceHttpApiHostModule>(args);
            await app.InitializeApplicationAsync();
            await app.RunAsync();
            return 0;
        }
        catch (Exception ex)
        {
            Log.Fatal(ex, "Host terminated unexpectedly!");
            return 1;
        }
        finally
        {
            Log.CloseAndFlush();
        }
    }
}
