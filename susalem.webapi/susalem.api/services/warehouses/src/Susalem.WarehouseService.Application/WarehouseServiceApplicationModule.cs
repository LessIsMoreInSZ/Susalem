using Volo.Abp.Application;
using Volo.Abp.Authorization;
using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;

namespace Susalem.WarehouseService;

[DependsOn(
    typeof(WarehouseServiceDomainSharedModule),
   typeof(AbpDddApplicationContractsModule),
        typeof(AbpAuthorizationModule)
    )]
public class WarehouseServiceApplicationModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpAutoMapperOptions>(options =>
        {
            options.AddMaps<WarehouseServiceApplicationModule>();
        });
    }
}
