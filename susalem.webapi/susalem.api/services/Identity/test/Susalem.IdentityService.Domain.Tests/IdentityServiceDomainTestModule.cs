using Susalem.IdentityService.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace Susalem.IdentityService;

[DependsOn(
    typeof(IdentityServiceEntityFrameworkCoreTestModule)
    )]
public class IdentityServiceDomainTestModule : AbpModule
{

}
