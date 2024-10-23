using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using Serilog;

using Volo.Abp.Modularity;

namespace SusalemAbp.Shared.Hosting.AspNetCore;

public static class ApplicationBuilderHelper
{
    public static async Task<WebApplication> BuildApplicationAsync<TStartupModule>(string[] args)
        where TStartupModule : IAbpModule
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.Host
            .AddAppSettingsSecretsJson()
            .UseAutofac()
            .UseSerilog();
        var name = typeof(TStartupModule).Name;
        await builder.AddApplicationAsync<TStartupModule>();
        return builder.Build();
    }
}