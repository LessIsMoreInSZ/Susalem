using Microsoft.EntityFrameworkCore;

using Susalem.WarehouseService.EntityFrameworkCore.DbContextModelCreatingExtensions;

using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace Susalem.WarehouseService.EntityFrameworkCore;

[ConnectionStringName(WarehouseServiceDbProperties.ConnectionStringName)]
public class WarehouseServiceDbContext :AbpDbContext<WarehouseServiceDbContext>, IWarehouseServiceDbContext

{
    /* Add DbSet properties for your Aggregate Roots / Entities here. */

    public WarehouseServiceDbContext(DbContextOptions<WarehouseServiceDbContext> options)
        : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ConfigureWarehouseService();
    }
}
