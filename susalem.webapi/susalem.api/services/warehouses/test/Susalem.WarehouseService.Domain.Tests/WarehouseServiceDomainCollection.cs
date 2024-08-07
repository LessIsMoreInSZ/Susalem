using Susalem.WarehouseService.EntityFrameworkCore;
using Xunit;

namespace Susalem.WarehouseService;

[CollectionDefinition(WarehouseServiceTestConsts.CollectionDefinitionName)]
public class WarehouseServiceDomainCollection : WarehouseServiceEntityFrameworkCoreCollectionFixtureBase
{

}
