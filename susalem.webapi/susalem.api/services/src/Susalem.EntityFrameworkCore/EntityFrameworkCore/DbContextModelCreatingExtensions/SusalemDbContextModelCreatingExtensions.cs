using Microsoft.EntityFrameworkCore;

using Volo.Abp;

namespace Susalem.EntityFrameworkCore.DbContextModelCreatingExtensions
{
    public static class SusalemDbContextModelCreatingExtensions
    {
        public static void ConfigureWarehouseService(
         this ModelBuilder builder)
        {
            Check.NotNull(builder, nameof(builder));

        }
    }
}
