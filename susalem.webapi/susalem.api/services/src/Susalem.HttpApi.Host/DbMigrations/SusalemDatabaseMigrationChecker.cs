using Susalem.EntityFrameworkCore;

using SusalemAbp.Shared.Hosting.Microservices.DbMigrations.EfCore;

using System;

using Volo.Abp.DistributedLocking;
using Volo.Abp.MultiTenancy;
using Volo.Abp.Uow;

namespace Susalem.DbMigrations
{
    public class SusalemDatabaseMigrationChecker : PendingEfCoreMigrationsChecker<SusalemDbContext>
    {
        public SusalemDatabaseMigrationChecker(
            IUnitOfWorkManager unitOfWorkManager,
            IServiceProvider serviceProvider,
            ICurrentTenant currentTenant,
            //IDistributedEventBus distributedEventBus,
            IAbpDistributedLock abpDistributedLock)
            : base(
                unitOfWorkManager,
                serviceProvider,
                currentTenant,
                //distributedEventBus,
                abpDistributedLock,
                SusalemDbProperties.ConnectionStringName)
        {
        }
    }
}