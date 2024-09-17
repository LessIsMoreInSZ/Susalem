using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Identity;
using Volo.Abp.Modularity;

namespace Susalem.Abp.Identity.HttpApi.Client
{
    [DependsOn(
        typeof(Volo.Abp.Identity.AbpIdentityHttpApiClientModule),
        typeof(AbpIdentityApplicationContractsModule))]
    public class AbpIdentityHttpApiClientModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddHttpClientProxies(
                typeof(AbpIdentityApplicationContractsModule).Assembly,
                IdentityRemoteServiceConsts.RemoteServiceName
            );
        }
    }
}
