using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace SusalemAbp.Shared.Domain.DbContext.IDbContext
{
   public interface IWarehouseServiceDbContext: IEfCoreDbContext
    {
        //public DbSet<Test> Users { get; set; }
    }
}
