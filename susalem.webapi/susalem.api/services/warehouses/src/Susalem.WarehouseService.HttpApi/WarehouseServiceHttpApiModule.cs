using Localization.Resources.AbpUi;

using Microsoft.Extensions.DependencyInjection;

using Susalem.WarehouseService.Localization;

using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;

namespace Susalem.WarehouseService;

[DependsOn(
    typeof(WarehouseServiceApplicationContractsModule),
    typeof(AbpAspNetCoreMvcModule)
    )]
public class WarehouseServiceHttpApiModule : AbpModule
{

    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        PreConfigure<IMvcBuilder>(mvcBuilder =>
        {
            mvcBuilder.AddApplicationPartIfNotExists(typeof(WarehouseServiceHttpApiModule).Assembly);
        });
    }
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpLocalizationOptions>(options =>
        {
            options.Resources
                .Get<WarehouseServiceResource>()
                .AddBaseTypes(
                    typeof(AbpUiResource)
                );
        });
    }
}
