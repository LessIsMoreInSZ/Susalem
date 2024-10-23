using Serilog;
using Serilog.Events;

namespace SusalemAbp.Shared.Hosting.AspNetCore;

public static class SerilogConfigurationHelper
{
    public static void Configure(string applicationName)
    {
        Log.Logger = new LoggerConfiguration()
#if DEBUG
            .MinimumLevel.Debug()
#else
                .MinimumLevel.Information()
#endif
            .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
            .MinimumLevel.Override("Microsoft.EntityFrameworkCore", LogEventLevel.Warning)
            .Enrich.FromLogContext()
            .Enrich.WithProperty("Application", $"{applicationName}")
            .WriteTo.Logger(log => log.Filter.ByIncludingOnly(evt => evt.Level == LogEventLevel.Information).WriteTo.File($"Logs/{(DateTime.Now.ToString("yyyy-MM-dd", System.Globalization.CultureInfo.CurrentCulture) + "/info.txt")}", fileSizeLimitBytes: 8388608000), LogEventLevel.Information)
            .WriteTo.Logger(log => log.Filter.ByIncludingOnly(evt => evt.Level == LogEventLevel.Warning).WriteTo.File($"Logs/{(DateTime.Now.ToString("yyyy-MM-dd", System.Globalization.CultureInfo.CurrentCulture) + "/warning.txt")}", fileSizeLimitBytes: 83886080), LogEventLevel.Warning)
            .WriteTo.Logger(log => log.Filter.ByIncludingOnly(evt => evt.Level == LogEventLevel.Fatal).WriteTo.File($"Logs/{(DateTime.Now.ToString("yyyy-MM-dd", System.Globalization.CultureInfo.CurrentCulture) + "/fatal.txt")}", fileSizeLimitBytes: 83886080), LogEventLevel.Fatal)
            .WriteTo.Logger(log => log.Filter.ByIncludingOnly(evt => evt.Level == LogEventLevel.Error).WriteTo.File($"Logs/{(DateTime.Now.ToString("yyyy-MM-dd", System.Globalization.CultureInfo.CurrentCulture) + "/error.txt")}", fileSizeLimitBytes: 83886080), LogEventLevel.Error)
            .WriteTo.Async(c => c.Console())
            .CreateLogger();
    }
}