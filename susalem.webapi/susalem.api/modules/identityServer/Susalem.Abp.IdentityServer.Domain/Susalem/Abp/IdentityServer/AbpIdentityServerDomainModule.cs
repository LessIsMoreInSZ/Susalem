using Volo.Abp.Modularity;

namespace Susalem.Abp.IdentityServer
{
    [DependsOn(typeof(Volo.Abp.IdentityServer.AbpIdentityServerDomainModule))]
    public class AbpIdentityServerDomainModule : AbpModule
    {
    }
}
