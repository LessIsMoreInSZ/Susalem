using Microsoft.EntityFrameworkCore;

using Susalem.Sys;
using Susalem.Sys.Dictionaries;
using Susalem.Sys.SysDictionaryItems;
using Susalem.Sys.SysOperations;

using Volo.Abp;
using Volo.Abp.EntityFrameworkCore.Modeling;
namespace Susalem.Mes.EntityFrameworkCore.DbContextModelCreatingExtensions;


public static class SysDbContextModelCreatingExtensions
{
    /// <summary>
    /// 【数据库表映射】MES系统管理
    /// </summary>
    /// <param name="builder">模型生成对象</param>
    public static void ConfigureMesSys(
        this ModelBuilder builder)
    {
        Check.NotNull(builder, nameof(builder));

        //编码规则
        builder.Entity<SysCodeRule>(b =>
        {
            //配置表名和表架构名
            b.ConfigureByConvention();

            //索引
            b.HasIndex(q => q.ModuleCode);
        });

        //操作日志
        builder.Entity<SysOperation>(b =>
        {
            //配置表名和表架构名
            b.ConfigureByConvention();


            //索引
            b.HasIndex(q => q.Platform);
            b.HasIndex(q => q.OperateCategoryEnum);
            b.HasIndex(q => q.CreationTime);
        });


        //字典管理
        builder.Entity<SysDictionary>(b =>
        {
            //配置表名和表架构名
            b.ConfigureByConvention();

            //索引
            b.HasIndex(q => q.Code);
        });

        //字典配置子表
        builder.Entity<SysDictionaryItem>(b =>
        {
            //配置表名和表架构名
            b.ConfigureByConvention();

            //索引
            b.HasIndex(q => q.DictionaryId);
            b.HasIndex(q => q.Index);
        });
  
    }
}
