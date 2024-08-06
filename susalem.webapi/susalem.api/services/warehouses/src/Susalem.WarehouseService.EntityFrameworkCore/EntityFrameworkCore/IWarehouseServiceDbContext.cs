using Microsoft.EntityFrameworkCore;

using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace Susalem.WarehouseService.EntityFrameworkCore;

[ConnectionStringName(WarehouseServiceDbProperties.ConnectionStringName)]
public interface IWarehouseServiceDbContext :IEfCoreDbContext
{
    /* Add DbSet properties for your Aggregate Roots / Entities here. */

    
}
