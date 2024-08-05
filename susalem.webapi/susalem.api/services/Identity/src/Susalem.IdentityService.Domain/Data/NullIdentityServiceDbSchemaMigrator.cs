using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace Susalem.IdentityService.Data;

/* This is used if database provider does't define
 * IIdentityServiceDbSchemaMigrator implementation.
 */
public class NullIdentityServiceDbSchemaMigrator : IIdentityServiceDbSchemaMigrator, ITransientDependency
{
    public Task MigrateAsync()
    {
        return Task.CompletedTask;
    }
}
