using Microsoft.Extensions.DependencyInjection;

using Volo.Abp.Http.Client;
using Volo.Abp.Modularity;
using Volo.Abp.VirtualFileSystem;

namespace Susalem.WarehouseService;

[DependsOn(
    typeof(WarehouseServiceApplicationContractsModule),
        typeof(AbpHttpClientModule))]
public class WarehouseServiceHttpApiClientModule : AbpModule
{
    public const string RemoteServiceName = "Warehouse";

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddHttpClientProxies(
            typeof(WarehouseServiceApplicationContractsModule).Assembly,
            RemoteServiceName
        );

        Configure<AbpVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<WarehouseServiceHttpApiClientModule>();
        });
    }
}
