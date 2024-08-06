using Volo.Abp.Modularity;

namespace Susalem.WarehouseService;

[DependsOn(
    typeof(WarehouseServiceApplicationModule),
    typeof(WarehouseServiceDomainTestModule)
    )]
public class WarehouseServiceApplicationTestModule : AbpModule
{

}
