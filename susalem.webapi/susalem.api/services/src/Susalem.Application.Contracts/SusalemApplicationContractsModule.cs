using Volo.Abp.Application;
using Volo.Abp.Authorization;
using Volo.Abp.Modularity;

namespace Susalem;

[DependsOn(
    typeof(SusalemDomainSharedModule),
    typeof(AbpDddApplicationContractsModule),
    typeof(AbpAuthorizationModule))]
public class SusalemApplicationContractsModule : AbpModule
{
    
}
