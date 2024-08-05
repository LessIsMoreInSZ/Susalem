using Susalem.IdentityService.EntityFrameworkCore;
using Xunit;

namespace Susalem.IdentityService;

[CollectionDefinition(IdentityServiceTestConsts.CollectionDefinitionName)]
public class IdentityServiceApplicationCollection : IdentityServiceEntityFrameworkCoreCollectionFixtureBase
{

}
