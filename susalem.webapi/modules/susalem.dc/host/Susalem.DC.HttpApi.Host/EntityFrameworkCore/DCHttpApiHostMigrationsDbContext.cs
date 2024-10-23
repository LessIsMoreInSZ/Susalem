using Microsoft.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Susalem.DC.EntityFrameworkCore;

public class DCHttpApiHostMigrationsDbContext : AbpDbContext<DCHttpApiHostMigrationsDbContext>
{
    public DCHttpApiHostMigrationsDbContext(DbContextOptions<DCHttpApiHostMigrationsDbContext> options)
        : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ConfigureDC();
    }
}
