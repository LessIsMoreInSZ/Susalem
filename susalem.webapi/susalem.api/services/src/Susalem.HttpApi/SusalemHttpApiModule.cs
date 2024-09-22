using Localization.Resources.AbpUi;

using Microsoft.Extensions.DependencyInjection;

using Susalem.Localization;

using Volo.Abp.Account;
using Volo.Abp.AspNetCore.Mvc.Localization;
using Volo.Abp.FeatureManagement;
using Volo.Abp.Identity;
using Volo.Abp.Identity.Localization;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Volo.Abp.OpenIddict.Localization;
using Volo.Abp.PermissionManagement.HttpApi;
using Volo.Abp.SettingManagement;
using Volo.Abp.TenantManagement;

namespace Susalem;

[DependsOn(
   typeof(SusalemApplicationContractsModule),
    typeof(AbpAccountHttpApiModule),
    typeof(AbpPermissionManagementHttpApiModule),
    typeof(AbpTenantManagementHttpApiModule),
    typeof(AbpFeatureManagementHttpApiModule),
    typeof(AbpSettingManagementHttpApiModule)
    )]
public class SusalemHttpApiModule : AbpModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        PreConfigure<AbpMvcDataAnnotationsLocalizationOptions>(options =>
        {
            options.AddAssemblyResource(typeof(AbpOpenIddictResource), typeof(SusalemHttpApiModule).Assembly);
        });

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
