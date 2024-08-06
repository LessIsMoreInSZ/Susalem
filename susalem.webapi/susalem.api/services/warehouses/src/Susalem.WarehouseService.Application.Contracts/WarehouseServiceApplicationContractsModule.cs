using Volo.Abp.Application;
using Volo.Abp.Authorization;
using Volo.Abp.Modularity;

namespace Susalem.WarehouseService;

[DependsOn(
    typeof(WarehouseServiceDomainSharedModule),
    typeof(AbpDddApplicationContractsModule),
    typeof(AbpAuthorizationModule)
)]
public class WarehouseServiceApplicationContractsModule : AbpModule
{
  
}
