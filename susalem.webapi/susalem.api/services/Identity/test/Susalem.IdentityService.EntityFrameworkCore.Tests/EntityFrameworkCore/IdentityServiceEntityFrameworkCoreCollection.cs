using Xunit;

namespace Susalem.IdentityService.EntityFrameworkCore;

[CollectionDefinition(IdentityServiceTestConsts.CollectionDefinitionName)]
public class IdentityServiceEntityFrameworkCoreCollection : ICollectionFixture<IdentityServiceEntityFrameworkCoreFixture>
{

}
