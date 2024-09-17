using Susalem.Abp.Account.Templates;
using Susalem.Abp.Identity;
using Volo.Abp.Modularity;
using Volo.Abp.UI.Navigation.Urls;
using Volo.Abp.VirtualFileSystem;

namespace Susalem.Abp.Account
{
    [DependsOn(
        typeof(Volo.Abp.Account.AbpAccountApplicationModule),
        typeof(AbpAccountApplicationContractsModule),
        typeof(AbpAccountTemplatesModule),
        typeof(AbpIdentityDomainModule)
       )]
    public class AbpAccountApplicationModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpVirtualFileSystemOptions>(options =>
            {
                options.FileSets.AddEmbedded<AbpAccountApplicationModule>();
            });

            Configure<AppUrlOptions>(options =>
            {
                options.Applications["MVC"].Urls[AccountUrlNames.EmailConfirm] = "Account/EmailConfirm";
            });
        }
    }
}
