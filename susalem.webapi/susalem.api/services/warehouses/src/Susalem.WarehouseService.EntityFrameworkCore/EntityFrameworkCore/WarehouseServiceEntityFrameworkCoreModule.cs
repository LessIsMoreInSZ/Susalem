using Microsoft.Extensions.DependencyInjection;

using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.MySQL;
using Volo.Abp.Modularity;

namespace Susalem.WarehouseService.EntityFrameworkCore;

[DependsOn(
    typeof(WarehouseServiceDomainModule),
    typeof(AbpEntityFrameworkCoreMySQLModule)
    )]
public class WarehouseServiceEntityFrameworkCoreModule : AbpModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        WarehouseServiceEfCoreEntityExtensionMappings.Configure();
    }

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddAbpDbContext<WarehouseServiceDbContext>(options =>
        {
                /* Remove "includeAllEntities: true" to create
                 * default repositories only for aggregate roots */
            options.AddDefaultRepositories(includeAllEntities: true);
        });

        Configure<AbpDbContextOptions>(options =>
        {
                /* The main point to change your DBMS.
                 * See also WarehouseServiceMigrationsDbContextFactory for EF Core tooling. */
            options.UseMySQL();
        });

    }
}
