using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Volo.Abp.Validation;
using Volo.Abp.Validation.Localization;
using Volo.Abp.VirtualFileSystem;

namespace SusalemAbp.Localization
{
    [DependsOn(
    typeof(AbpValidationModule)
)]
    public class SusalemAbpSharedLocalizationModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpVirtualFileSystemOptions>(options =>
            {
                options.FileSets.AddEmbedded<SusalemAbpSharedLocalizationModule>();
            });

            Configure<AbpLocalizationOptions>(options =>
            {
                options.Resources
                    .Add<SusalemAbpResource>("zh-Hans")
                    .AddBaseTypes(
                        typeof(AbpValidationResource)
                    ).AddVirtualJson("/Localization/SusalemAbp");

                options.DefaultResourceType = typeof(SusalemAbpResource);
            });
        }
    }
}