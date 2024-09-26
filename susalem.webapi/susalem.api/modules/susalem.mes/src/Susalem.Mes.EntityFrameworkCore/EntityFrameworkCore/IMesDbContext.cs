using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace Susalem.Mes.EntityFrameworkCore;

[ConnectionStringName(MesDbProperties.ConnectionStringName)]
public interface IMesDbContext : IEfCoreDbContext
{
    /* Add DbSet for each Aggregate Root here. Example:
     * DbSet<Question> Questions { get; }
     */
}
