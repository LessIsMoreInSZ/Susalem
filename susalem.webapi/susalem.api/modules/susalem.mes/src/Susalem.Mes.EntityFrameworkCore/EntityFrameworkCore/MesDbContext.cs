using Microsoft.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace Susalem.Mes.EntityFrameworkCore;

[ConnectionStringName(MesDbProperties.ConnectionStringName)]
public class MesDbContext : AbpDbContext<MesDbContext>, IMesDbContext
{
    /* Add DbSet for each Aggregate Root here. Example:
     * public DbSet<Question> Questions { get; set; }
     */

    public MesDbContext(DbContextOptions<MesDbContext> options)
        : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ConfigureMes();
    }
}
