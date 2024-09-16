using Volo.Abp.Modularity;

namespace Susalem.Abp.IdentityServer.EntityFrameworkCore
{
    [DependsOn(typeof(Susalem.Abp.IdentityServer.AbpIdentityServerDomainModule))]
    [DependsOn(typeof(Volo.Abp.IdentityServer.EntityFrameworkCore.AbpIdentityServerEntityFrameworkCoreModule))]
    public class AbpIdentityServerEntityFrameworkCoreModule : AbpModule
    {
    }
}
