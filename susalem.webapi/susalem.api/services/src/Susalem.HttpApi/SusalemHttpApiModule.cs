using Localization.Resources.AbpUi;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

using Susalem.Localization;

using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;

namespace Susalem;

[DependsOn(
    typeof(SusalemApplicationContractsModule),
        typeof(AbpAspNetCoreMvcModule)
    )]
public class SusalemHttpApiModule : AbpModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        PreConfigure<IMvcBuilder>(mvcBuilder =>
        {
            mvcBuilder.AddApplicationPartIfNotExists(typeof(SusalemHttpApiModule).Assembly);
        });
    }
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpLocalizationOptions>(options =>
        {
            options.Resources
                .Get<SusalemResource>()
                .AddBaseTypes(
                    typeof(AbpUiResource)
                );
        });
    }
}
