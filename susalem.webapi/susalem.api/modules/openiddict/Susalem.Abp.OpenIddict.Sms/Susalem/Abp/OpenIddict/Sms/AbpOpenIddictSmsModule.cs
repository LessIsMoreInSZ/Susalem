using Susalem.Abp.Identity;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Volo.Abp.OpenIddict;
using Volo.Abp.OpenIddict.ExtensionGrantTypes;
using Volo.Abp.OpenIddict.Localization;
using Volo.Abp.Sms;
using Volo.Abp.VirtualFileSystem;

namespace Susalem.Abp.OpenIddict.Sms;

[DependsOn(
    typeof(AbpSmsModule),
    typeof(AbpIdentityDomainModule),
    typeof(AbpOpenIddictAspNetCoreModule))]
public class AbpOpenIddictSmsModule : AbpModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        PreConfigure<OpenIddictServerBuilder>(builder =>
        {
            builder.AllowSmsFlow();
        });
    }

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpOpenIddictExtensionGrantsOptions>(options =>
        {
            options.Grants.TryAdd(
                SmsTokenExtensionGrantConsts.GrantType,
                new SmsTokenController());
        });

        Configure<AbpVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<AbpOpenIddictSmsModule>();
        });

        Configure<AbpLocalizationOptions>(options =>
        {
            options.Resources
                .Get<AbpOpenIddictResource>()
                .AddVirtualJson("/Susalem/Abp/OpenIddict/Sms/Localization/Resources");
        });
    }
}