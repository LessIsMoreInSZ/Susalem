﻿using Susalem.DC.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace Susalem.DC;

/* Domain tests are configured to use the EF Core provider.
 * You can switch to MongoDB, however your domain tests should be
 * database independent anyway.
 */
[DependsOn(
    typeof(DCEntityFrameworkCoreTestModule)
    )]
public class DCDomainTestModule : AbpModule
{

}
