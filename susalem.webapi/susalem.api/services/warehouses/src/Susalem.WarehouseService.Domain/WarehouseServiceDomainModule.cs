using SusalemAbp.Shared.DbContext;

using Volo.Abp.Domain;
using Volo.Abp.Modularity;

namespace Susalem.WarehouseService;

[DependsOn(
        typeof(AbpDddDomainModule),
    typeof(SusalemAbpSharedDomainDbContextModule),
    typeof(WarehouseServiceDomainSharedModule)
)]
public class WarehouseServiceDomainModule : AbpModule
{
   
}
