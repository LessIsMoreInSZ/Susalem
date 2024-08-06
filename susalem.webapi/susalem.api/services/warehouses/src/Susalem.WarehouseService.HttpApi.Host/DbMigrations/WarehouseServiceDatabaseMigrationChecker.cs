using Susalem.WarehouseService.EntityFrameworkCore;

using SusalemAbp.Shared.Hosting.Microservices.DbMigrations.EfCore;

using System;

using Volo.Abp.DistributedLocking;
using Volo.Abp.MultiTenancy;
using Volo.Abp.Uow;

namespace Susalem.WarehouseService.DbMigrations;

public class WarehouseServiceDatabaseMigrationChecker 
    : PendingEfCoreMigrationsChecker<WarehouseServiceDbContext>
{
    public WarehouseServiceDatabaseMigrationChecker(
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
            WarehouseServiceDbProperties.ConnectionStringName)
    {
    }
}