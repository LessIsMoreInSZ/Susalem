using Xunit;

namespace Susalem.WarehouseService.EntityFrameworkCore;

[CollectionDefinition(WarehouseServiceTestConsts.CollectionDefinitionName)]
public class WarehouseServiceEntityFrameworkCoreCollection : ICollectionFixture<WarehouseServiceEntityFrameworkCoreFixture>
{

}
