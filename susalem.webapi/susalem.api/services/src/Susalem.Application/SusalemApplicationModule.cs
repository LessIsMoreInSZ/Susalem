using Volo.Abp.Application;
using Volo.Abp.Authorization;
using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;

namespace Susalem;

[DependsOn(
    typeof(SusalemDomainSharedModule),
   typeof(AbpDddApplicationContractsModule),
        typeof(AbpAuthorizationModule)
    )]
public class SusalemApplicationModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpAutoMapperOptions>(options =>
        {
            options.AddMaps<SusalemApplicationModule>();
        });
    }
}
