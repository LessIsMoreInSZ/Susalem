using Volo.Abp.Domain;
using Volo.Abp.Modularity;

namespace Susalem;

[DependsOn(
    typeof(AbpDddDomainModule),
    typeof(SusalemDomainSharedModule)
)]
public class SusalemDomainModule : AbpModule
{
}
