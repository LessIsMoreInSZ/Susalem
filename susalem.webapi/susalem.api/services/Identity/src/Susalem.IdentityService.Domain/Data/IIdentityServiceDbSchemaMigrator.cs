using System.Threading.Tasks;

namespace Susalem.IdentityService.Data;

public interface IIdentityServiceDbSchemaMigrator
{
    Task MigrateAsync();
}
