using Microsoft.Extensions.DependencyInjection;

using SusalemAbp.Shared.Hosting.AspNetCore;

using Volo.Abp.AspNetCore.Authentication.JwtBearer;
using Volo.Abp.Modularity;

namespace SusalemAbp.Shared.Hosting.Microservices
{
    [DependsOn(
    typeof(SusalemAbpSharedHostingAspNetCoreModule),
    //typeof(AbpBackgroundJobsRabbitMqModule),
    typeof(AbpAspNetCoreAuthenticationJwtBearerModule)
    //typeof(AbpEventBusRabbitMqModule),
    //typeof(AbpCachingStackExchangeRedisModule),
    //typeof(AbpDistributedLockingModule)
)]

    public class SusalemAbpSharedHostingMicroservicesModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Microsoft.IdentityModel.Logging.IdentityModelEventSource.ShowPII = true;
            var configuration = context.Services.GetConfiguration();

            //Configure<AbpDistributedCacheOptions>(options =>
            //{
            //    options.KeyPrefix = "SusalemAbp:";
            //});

            //var redis = ConnectionMultiplexer.Connect(configuration["Redis:Configuration"]!);
            //context.Services
            //    .AddDataProtection()
            //    .PersistKeysToStackExchangeRedis(redis, "SusalemAbp-Protection-Keys");

            //context.Services.AddSingleton<IDistributedLockProvider>(sp =>
            //{
            //    var connection = ConnectionMultiplexer.Connect(configuration["Redis:Configuration"]!);
            //    return new RedisDistributedSynchronizationProvider(connection.GetDatabase());
            //});
        }
    }
}
