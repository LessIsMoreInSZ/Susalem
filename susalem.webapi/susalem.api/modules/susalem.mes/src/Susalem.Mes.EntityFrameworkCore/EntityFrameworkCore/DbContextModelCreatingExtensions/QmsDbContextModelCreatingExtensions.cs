using Microsoft.EntityFrameworkCore;

using Susalem.Qms.QmsAbnormalRecords;
using Susalem.Qms.QmsBarcodeOperates;
using Susalem.Qms.QmsCallAndonDatas;
using Susalem.Qms.QmsEquipmentStatusHistories;
using Susalem.Qms.QmsEquipmentStatusWips;
using Susalem.Qms.QmsFaultHistories;
using Susalem.Qms.QmsFaultWips;
using Susalem.Qms.QmsMaterialBarcodes;
using Susalem.Qms.QmsProcessDatas;
using Susalem.Qms.QmsStationCycleTimes;

using Volo.Abp;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace Susalem.Mes.EntityFrameworkCore.DbContextModelCreatingExtensions;


public static class QmsDbContextModelCreatingExtensions
{
    public static void ConfigureMesQms(
        this ModelBuilder builder)
    {
        Check.NotNull(builder, nameof(builder));

        //物料批次条码
        builder.Entity<QmsMaterialBarcode>(b =>
        {
            //表名映射
            

            //自动配置实体父类的属性
            b.ConfigureByConvention();

            //Properties
            b.Property(q => q.Qty).HasColumnType("decimal(16,4)").IsRequired();

            //创建索引
            b.HasIndex(q => q.EquipmentId);
            b.HasIndex(q => q.ProductId);
            b.HasIndex(q => q.PlanId);
            b.HasIndex(q => q.ProductTypeId);
            b.HasIndex(q => q.ProcessId);
            b.HasIndex(q => q.MaterialId);
            b.HasIndex(q => q.Status);
            b.HasIndex(q => q.ProductRecordId);
            b.HasIndex(q => q.PlanNo);
            b.HasIndex(q => q.CreationTime);
            b.HasIndex(q => q.IsNew);
            b.HasIndex(q => new { q.CreationTime, q.Status, q.Barcode });
            b.HasIndex(q => new { q.ProductId, q.Id, q.MaterialId, q.ProcessId, q.Status });

        });

        //工艺数据
        builder.Entity<QmsProcessData>(b =>
        {
            //表名映射
            

            //自动配置实体父类的属性
            b.ConfigureByConvention();

            //Properties
            b.Property(q => q.Value).HasColumnType("decimal(16,4)").IsRequired();
            b.Property(q => q.ParaConfigMaxValue).HasColumnType("decimal(16,4)").IsRequired();
            b.Property(q => q.ParaConfigMinValue).HasColumnType("decimal(16,4)").IsRequired();
            b.Property(q => q.ParaConfigStandardValue).HasColumnType("decimal(16,4)").IsRequired();

            //创建索引
            b.HasIndex(q => q.ParaConfigId);
            b.HasIndex(q => q.EquipmentId);
            b.HasIndex(q => q.ProductId);
            b.HasIndex(q => q.PlanId);
            b.HasIndex(q => q.ProductTypeId);
            b.HasIndex(q => q.ProcessId);
            b.HasIndex(q => q.Status);
            b.HasIndex(q => q.ProductRecordId);
            b.HasIndex(q => q.CreationTime);
            b.HasIndex(q => q.IsNew);
        });

        //报警记录实时
        builder.Entity<QmsFaultWip>(b =>
        {
            //表名映射
            

            //自动配置实体父类的属性
            b.ConfigureByConvention();


            //创建索引
            b.HasIndex(q => q.EquipmentId);
            b.HasIndex(q => q.CreationTime);
        });

        //报警记录历史
        builder.Entity<QmsFaultHistory>(b =>
        {
            //表名映射
            

            //自动配置实体父类的属性
            b.ConfigureByConvention();

            //创建索引
            b.HasIndex(q => q.EquipmentId);
            b.HasIndex(q => q.CreationTime);
        });

        //设备状态实时
        builder.Entity<QmsEquipmentStatusWip>(b =>
        {
            //表名映射
            

            //自动配置实体父类的属性
            b.ConfigureByConvention();

            //创建索引
            b.HasIndex(q => q.EquipmentId);
            b.HasIndex(q => q.Status);
            b.HasIndex(q => q.CreationTime);
        });

        //设备状态历史
        builder.Entity<QmsEquipmentStatusHistory>(b =>
        {
            //表名映射
            

            //自动配置实体父类的属性
            b.ConfigureByConvention();

            //Properties
            b.Property(q => q.Duration).HasColumnType("decimal(16,4)").IsRequired();

            //创建索引
            b.HasIndex(q => q.EquipmentId);
            b.HasIndex(q => q.Duration);
            b.HasIndex(q => q.CreationTime);
        });

        //设备状态历史
        builder.Entity<QmsBarcodeOperate>(b =>
        {
            //表名映射
            

            //自动配置实体父类的属性
            b.ConfigureByConvention();

            //创建索引
            b.HasIndex(q => q.CreationTime);
            b.HasIndex(q => q.BarCode);
            b.HasIndex(q => q.SnCode);
            b.HasIndex(q => q.OperateType);
        });

        //工位节拍
        builder.Entity<QmsStationCycleTime>(b =>
        {
            //表名映射
            

            //自动配置实体父类的属性
            b.ConfigureByConvention();

            b.Property(q => q.CycleTime).HasColumnType("decimal(16,4)").IsRequired();
            b.Property(q => q.StationCycleTime).HasColumnType("decimal(16,4)").IsRequired();

            //创建索引
            b.HasIndex(q => q.EquipmentCode);
            b.HasIndex(q => q.PalletNo);
            b.HasIndex(q => q.ProductStatus);
            b.HasIndex(q => q.SnCode);
            b.HasIndex(q => q.CreationTime);
        });

        //andon业务管控
        builder.Entity<QmsCallAndonData>(b =>
        {
            //表名映射
            
            //自动配置实体父类的属性
            b.ConfigureByConvention();

            b.HasIndex(z => z.FactoryId);
            b.HasIndex(z => z.PdLineId);
            b.HasIndex(z => z.WorkShopId);
            b.HasIndex(z => z.Status);
            b.HasIndex(z => z.CallStatus);
            b.HasIndex(q => q.CreationTime);
        });


        //异常点记录
        builder.Entity<QmsAbnormalRecord>(b =>
        {
            //表名映射
            

            //自动配置实体父类的属性
            b.ConfigureByConvention();


            //创建索引
            b.HasIndex(q => q.EquipmentId);
            b.HasIndex(q => q.EmployeeId);

            b.HasIndex(q => q.CreationTime);
        });
    }
}
