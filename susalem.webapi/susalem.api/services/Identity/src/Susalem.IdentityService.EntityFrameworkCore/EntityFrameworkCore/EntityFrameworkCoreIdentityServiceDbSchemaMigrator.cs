using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Susalem.IdentityService.Data;
using Volo.Abp.DependencyInjection;

namespace Susalem.IdentityService.EntityFrameworkCore;

public class EntityFrameworkCoreIdentityServiceDbSchemaMigrator
    : IIdentityServiceDbSchemaMigrator, ITransientDependency
{
    private readonly IServiceProvider _serviceProvider;

    public EntityFrameworkCoreIdentityServiceDbSchemaMigrator(
        IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task MigrateAsync()
    {
        /* We intentionally resolving the IdentityServiceDbContext
         * from IServiceProvider (instead of directly injecting it)
         * to properly get the connection string of the current tenant in the
         * current scope.
         */

        await _serviceProvider
            .GetRequiredService<IdentityServiceDbContext>()
            .Database
            .MigrateAsync();
    }
}
