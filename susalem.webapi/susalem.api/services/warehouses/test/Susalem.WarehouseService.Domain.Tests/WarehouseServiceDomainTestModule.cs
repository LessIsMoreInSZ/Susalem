using Susalem.WarehouseService.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace Susalem.WarehouseService;

[DependsOn(
    typeof(WarehouseServiceEntityFrameworkCoreTestModule)
    )]
public class WarehouseServiceDomainTestModule : AbpModule
{

}
