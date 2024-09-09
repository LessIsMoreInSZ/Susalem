using Microsoft.EntityFrameworkCore;

using Susalem.EntityFrameworkCore.DbContextModelCreatingExtensions;

using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;

namespace Susalem.EntityFrameworkCore;

[ConnectionStringName(SusalemDbProperties.ConnectionStringName)]
public class SusalemDbContext :
    AbpDbContext<SusalemDbContext>,IEfCoreDbContext
{
    /* Add DbSet properties for your Aggregate Roots / Entities here. */

    

    public SusalemDbContext(DbContextOptions<SusalemDbContext> options)
        : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder); 
        builder.ConfigureWarehouseService();

    }
}
