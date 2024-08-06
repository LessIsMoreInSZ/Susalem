using Volo.Abp.Domain;
using Volo.Abp.Modularity;

namespace Susalem.WarehouseService;

[DependsOn(
        typeof(AbpDddDomainModule),
    typeof(WarehouseServiceDomainSharedModule)
)]
public class WarehouseServiceDomainModule : AbpModule
{
   
}
