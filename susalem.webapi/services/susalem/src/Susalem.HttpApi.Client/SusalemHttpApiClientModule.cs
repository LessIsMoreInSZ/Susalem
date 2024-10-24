using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Account;
using Volo.Abp.Modularity;
using Volo.Abp.PermissionManagement;
using Volo.Abp.VirtualFileSystem;

namespace Susalem;

[DependsOn(
    typeof(SusalemApplicationContractsModule),
    typeof(AbpAccountHttpApiClientModule),
    typeof(Susalem.Identity.AbpIdentityHttpApiClientModule),
    typeof(AbpPermissionManagementHttpApiClientModule)
)]
public class SusalemHttpApiClientModule : AbpModule
{
    public const string RemoteServiceName = "Default";

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddHttpClientProxies(
            typeof(SusalemApplicationContractsModule).Assembly,
            RemoteServiceName
        );

        Configure<AbpVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<SusalemHttpApiClientModule>();
        });
    }
}
