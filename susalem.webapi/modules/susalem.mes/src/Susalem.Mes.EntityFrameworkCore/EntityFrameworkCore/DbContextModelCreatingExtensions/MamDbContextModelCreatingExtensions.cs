using Microsoft.EntityFrameworkCore;

using Susalem.Mam.MamBarcodeBlacks;
using Susalem.Mam.MamDisassembleOrderEnum;
using Susalem.Mam.MamDisassembleOrders;
using Susalem.Mam.MamEmployeeWorkEquipments;
using Susalem.Mam.MamEmployeeWorks;
using Susalem.Mam.MamMasterRepairChecks;
using Susalem.Mam.MamMasterRepairConfigs;
using Susalem.Mam.MamMasterRepairItems;
using Susalem.Mam.MamMasterRepairs;
using Susalem.Mam.MamOrderBoms;
using Susalem.Mam.MamOrders;
using Susalem.Mam.MamPlanFlowHistories;
using Susalem.Mam.MamPlanFlowWips;
using Susalem.Mam.MamPlanProductDeletes;
using Susalem.Mam.MamPlanProductHistories;
using Susalem.Mam.MamPlanProductRecordHistories;
using Susalem.Mam.MamPlanProductRecordWips;
using Susalem.Mam.MamPlanProductWips;
using Susalem.Mam.MamPlans;
using Susalem.Mam.MamProductionPlans;
using Susalem.Mam.MamProductRelations;
using Susalem.Mam.MamProductRepairItems;
using Susalem.Mam.MamProductRepairs;
using Susalem.Mam.MamProductVerAdapts;
using Susalem.Mam.MamRepairPolicies;
using Susalem.Mam.MamSpotCheckRecords;

using Volo.Abp;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace Susalem.Mes.EntityFrameworkCore.DbContextModelCreatingExtensions;


public static class MamDbContextModelCreatingExtensions
{
    public static void ConfigureMesMam(
        this ModelBuilder builder)
    {
        Check.NotNull(builder, nameof(builder));

        /* Configure all entities here. Example: */

        builder.Entity<Mam_Order>(b =>
        {
            //Configure table & schema name
            

            b.ConfigureByConvention();

            //Properties
            b.Property(q => q.OrderQty).HasColumnType("decimal(16,4)").IsRequired();
            b.Property(q => q.PlanedQty).HasColumnType("decimal(16,4)").IsRequired().HasDefaultValue(0);
            b.Property(q => q.UploadQty).HasColumnType("decimal(16,4)").IsRequired().HasDefaultValue(0);
            b.Property(q => q.OkQty).HasColumnType("decimal(16,4)").IsRequired();
            b.Property(q => q.NgQty).HasColumnType("decimal(16,4)").IsRequired();
            b.Property(q => q.ScrapQty).HasColumnType("decimal(16,4)").IsRequired();

            //Indexes
            b.HasIndex(q => q.SourceType);
            b.HasIndex(q => q.Status);
            b.HasIndex(q => q.ProductFlowId);
            b.HasIndex(q => q.ProductTypeId);
            b.HasIndex(q => q.OrderNo);
            b.HasIndex(q => q.CreationTime);
        });

        //产品清单实时
        builder.Entity<MamPlanProductWip>(b =>
        {
            //表名映射
            

            //自动配置实体父类的属性
            b.ConfigureByConvention();

            //Properties
            b.Property(q => q.OkQty).HasColumnType("decimal(16,4)").IsRequired();
            b.Property(q => q.NgQty).HasColumnType("decimal(16,4)").IsRequired();
            b.Property(q => q.UploadQty).HasColumnType("decimal(16,4)").IsRequired();
            b.Property(q => q.ScrapQty).HasColumnType("decimal(16,4)").IsRequired();
            b.Property(q => q.IsLock).HasDefaultValue(false);

            //创建索引
            b.HasIndex(q => q.SnCode);
            b.HasIndex(q => q.PlanId);
            b.HasIndex(q => q.ProductTypeId);
            b.HasIndex(q => q.IsOneTimeProcess);
            b.HasIndex(q => q.ProcessId);
            b.HasIndex(q => q.EquipmentId);
            b.HasIndex(q => q.CreationTime);
            b.HasIndex(q => q.IsRework);
            b.HasIndex(q => q.Status);
        });

        //产品清单历史
        builder.Entity<MamPlanProductHistory>(b =>
        {
            //表名映射
            

            //自动配置实体父类的属性
            b.ConfigureByConvention();

            //Properties
            b.Property(q => q.OkQty).HasColumnType("decimal(16,4)").IsRequired();
            b.Property(q => q.NgQty).HasColumnType("decimal(16,4)").IsRequired();
            b.Property(q => q.UploadQty).HasColumnType("decimal(16,4)").IsRequired();
            b.Property(q => q.ScrapQty).HasColumnType("decimal(16,4)").IsRequired();
            b.Property(q => q.IsLock).HasDefaultValue(false);

            //创建索引
            b.HasIndex(q => q.SnCode);
            b.HasIndex(q => q.PlanId);
            b.HasIndex(q => q.ProductTypeId);
            b.HasIndex(q => q.IsOneTimeProcess);
            b.HasIndex(q => q.ProcessId);
            b.HasIndex(q => q.EquipmentId);
            b.HasIndex(q => q.CreationTime);
            b.HasIndex(q => q.DayShiftItemId);
            b.HasIndex(q => q.IsRework);
            b.HasIndex(q => q.Status);
        });

        //产品加工记录
        builder.Entity<MamPlanProductRecordWip>(b =>
        {
            //表名映射
            

            //自动配置实体父类的属性
            b.ConfigureByConvention();

            //创建索引
            b.HasIndex(q => q.SnCode);
            b.HasIndex(q => q.ProductId);
            b.HasIndex(q => q.PlanId);
            b.HasIndex(q => q.ProductTypeId);
            b.HasIndex(q => q.ProcessId);
            b.HasIndex(q => q.EquipmentId);
            b.HasIndex(q => q.ProductFlowProcessId);
            b.HasIndex(q => q.CreationTime);
            b.HasIndex(q => q.DayShiftItemId);
            b.HasIndex(q => q.IsRework);
            b.HasIndex(q => q.Status);
            b.HasIndex(q => new { q.ProductId, q.ProcessId, q.Status });
        });

        //产品抽检记录
        builder.Entity<Mam_SpotCheckRecord>(b =>
        {
            //表名映射
            

            //自动配置实体父类的属性
            b.ConfigureByConvention();

            //创建索引
            b.HasIndex(q => q.SnCode);
            b.HasIndex(q => q.ProductId);
            b.HasIndex(q => q.PlanId);
            b.HasIndex(q => q.ProductTypeId);
            b.HasIndex(q => q.ProcessId);
            b.HasIndex(q => q.EquipmentId);
            b.HasIndex(q => q.ProductFlowProcessId);
            b.HasIndex(q => q.CreationTime);
            b.HasIndex(q => q.DayShiftItemId);
            b.HasIndex(q => q.IsRework);
            b.HasIndex(q => q.Status);
            b.HasIndex(q => new { q.ProductId, q.ProcessId, q.Status });
        });

        //实时生产工单
        builder.Entity<Mam_Plan>(b =>
        {
            //表名映射
            

            //自动配置实体父类的属性
            b.ConfigureByConvention();

            //设置属性
            b.Property(z => z.PlanQty).HasColumnType("decimal(16,4)").IsRequired();
            b.Property(z => z.UploadQty).HasColumnType("decimal(16,4)").IsRequired();
            b.Property(q => q.OkQty).HasColumnType("decimal(16,4)").IsRequired();
            b.Property(q => q.NgQty).HasColumnType("decimal(16,4)").IsRequired();
            b.Property(q => q.ScrapQty).HasColumnType("decimal(16,4)").IsRequired();

            //创建索引
            b.HasIndex(z => z.PlanNo);
            b.HasIndex(z => z.UploadProcessId);
            b.HasIndex(z => z.OrderId);
            b.HasIndex(q => q.ProductTypeId);
            b.HasIndex(q => q.Index);
            b.HasIndex(q => q.UploadQty);
            //b.HasIndex(q => q.CreationTime);
            b.HasIndex(q => q.Status);
            b.HasIndex(q => q.PlanNo);
        });

        //工单工艺流程
        builder.Entity<MamPlanFlowWip>(b =>
        {
            //表名映射
            

            //自动配置实体父类的属性
            b.ConfigureByConvention();

            //设置属性
            b.Property(z => z.PlanQty).HasColumnType("decimal(16,4)").IsRequired();
            b.Property(z => z.UploadQty).HasColumnType("decimal(16,4)").IsRequired();
            b.Property(z => z.OkQty).HasColumnType("decimal(16,4)").IsRequired();
            b.Property(z => z.NgQty).HasColumnType("decimal(16,4)").IsRequired();
            b.Property(z => z.ScrapQty).HasColumnType("decimal(16,4)").IsRequired();
            b.Property(z => z.CycleTime).HasColumnType("decimal(16,4)").IsRequired();
            b.Property(z => z.TimePrice).HasColumnType("decimal(16,4)");
            b.Property(z => z.PiecePrice).HasColumnType("decimal(16,4)");

            //创建索引
            b.HasIndex(z => z.PlanId);
            b.HasIndex(z => z.ProductTypeId);
            b.HasIndex(z => z.ProductFlowProcessId);
            b.HasIndex(q => q.ProcessId);
            b.HasIndex(q => q.Status);
            b.HasIndex(q => q.CreationTime);
        });

        //工单工艺流程历史
        builder.Entity<MamPlanFlowHistory>(b =>
        {
            //表名映射
            

            //自动配置实体父类的属性
            b.ConfigureByConvention();

            //设置属性
            b.Property(z => z.PlanQty).HasColumnType("decimal(16,4)").IsRequired();
            b.Property(z => z.UploadQty).HasColumnType("decimal(16,4)").IsRequired();
            b.Property(z => z.OkQty).HasColumnType("decimal(16,4)").IsRequired();
            b.Property(z => z.NgQty).HasColumnType("decimal(16,4)").IsRequired();
            b.Property(z => z.ScrapQty).HasColumnType("decimal(16,4)").IsRequired();
            b.Property(z => z.CycleTime).HasColumnType("decimal(16,4)").IsRequired();
            b.Property(z => z.TimePrice).HasColumnType("decimal(16,4)");
            b.Property(z => z.PiecePrice).HasColumnType("decimal(16,4)");

            //创建索引
            b.HasIndex(z => z.PlanId);
            b.HasIndex(z => z.ProductTypeId);
            b.HasIndex(z => z.ProductFlowProcessId);
            b.HasIndex(q => q.ProcessId);
            b.HasIndex(q => q.Status);
            b.HasIndex(q => q.CreationTime);
        });

        //外部接收订单表
        builder.Entity<Mam_ProductionPlan>(b =>
        {
            //表名映射
            

            //自动配置实体父类的属性
            b.ConfigureByConvention();

            //创建索引
            b.HasIndex(q => q.OrderNo);
            b.HasIndex(q => q.Status);
            b.HasIndex(q => q.CreationTime);
        });

        //产品加工记录历史
        builder.Entity<MamPlanProductRecordHistory>(b =>
        {
            //表名映射
            

            //自动配置实体父类的属性
            b.ConfigureByConvention();

            b.Property(z => z.UploadQty).HasColumnType("decimal(16,4)").IsRequired();
            b.Property(z => z.OkQty).HasColumnType("decimal(16,4)").IsRequired();
            b.Property(z => z.NgQty).HasColumnType("decimal(16,4)").IsRequired();
            b.Property(z => z.ScrapQty).HasColumnType("decimal(16,4)").IsRequired();
            b.Property(z => z.ActualCycleTime).HasColumnType("decimal(16,4)").IsRequired();

            //创建索引
            b.HasIndex(q => q.ProductId);
            b.HasIndex(q => q.PlanId);
            b.HasIndex(q => q.SnCode);
            b.HasIndex(q => q.ProductTypeId);
            b.HasIndex(q => q.ProductTypeId);
            b.HasIndex(q => q.WorkEmployeeId);
            b.HasIndex(q => q.Status);
            b.HasIndex(q => q.CreationTime);
            b.HasIndex(q => q.DayShiftItemId);
            b.HasIndex(q => q.IsRework);
        });


        //产品关联关系
        builder.Entity<Mam_ProductRelation>(b =>
        {
            //表名映射
            

            //自动配置实体父类的属性
            b.ConfigureByConvention();

            b.Property(z => z.IsDownLine).IsRequired().HasDefaultValue(false);

            //创建索引
            b.HasIndex(q => q.ProductId);
            b.HasIndex(q => q.ParentProductId);
            b.HasIndex(q => q.ProductSnCode);
            b.HasIndex(q => q.PlanNo);
            b.HasIndex(q => q.SerialNo);
            b.HasIndex(q => q.Status);
            b.HasIndex(q => q.IsDownLine);
            b.HasIndex(q => q.IsMain);
            b.HasIndex(q => new { q.IsDownLine, q.IsMain, q.Status, q.SimpleSerialNo });
        });

        //拆解订单-Mop
        builder.Entity<MamDisassembleOrder>(b =>
        {
            //表名映射
            

            //自动配置实体父类的属性
            b.ConfigureByConvention();
            b.Property(q => q.DownlineMark).HasDefaultValue(0); // 新增字段给默认值
            //创建索引
            b.HasIndex(q => q.SerialNo);
            b.HasIndex(q => q.TraceCode);
            b.HasIndex(q => q.WorkOrderNo);
            b.HasIndex(q => q.CreationTime);
        });

   
        //员工上岗记录
        builder.Entity<MamEmployeeWork>(b =>
        {
            

            b.ConfigureByConvention();

            //创建索引
            b.HasIndex(z => z.EmployeeId);
            b.HasIndex(z => z.EquipmentId);
            b.HasIndex(z => z.DataSourceType);
            b.HasIndex(z => z.CreationTime);
        });

        //订单BOM
        builder.Entity<Mam_OrderBom>(b =>
        {
            //表名映射
            

            //自动配置实体父类的属性
            b.ConfigureByConvention();

            //设置属性
            b.Property(z => z.Qty).HasColumnType("decimal(16,4)").IsRequired();

            b.HasIndex(z => z.WorkStationId);
            b.HasIndex(z => z.OrderId);
            b.HasIndex(z => z.ProductTypeId);
            b.HasIndex(z => z.MaterialId);
            b.HasIndex(z => z.MaterialTraceType);
            b.HasIndex(z => z.CreationTime);
        });


        //员工上岗设备关系
        builder.Entity<MamEmployeeWorkEquipment>(b =>
        {
            //表名映射
            

            //自动配置实体父类的属性
            b.ConfigureByConvention();

            b.HasIndex(z => z.EmployeeId);
            b.HasIndex(z => z.EquipmentId);
        });

        //返修策略
        builder.Entity<Mam_RepairPolicy>(b =>
        {
            //表名映射
            

            //自动配置实体父类的属性
            b.ConfigureByConvention();

            b.HasIndex(z => z.WorkStationNo);
            b.HasIndex(z => z.ProcessPeople);
            b.HasIndex(z => z.ProductTypeId);
            b.HasIndex(z => z.DisposalSuggestions);
            b.HasIndex(z => z.CreationTime);
        });

        //Master件维护-质量参数表
        builder.Entity<Mam_MasterRepairConfig>(b =>
        {
            //表名映射
            

            //自动配置实体父类的属性
            b.ConfigureByConvention();

            b.Property(z => z.StandardValue).HasColumnType("decimal(16,4)").IsRequired();
            b.Property(z => z.MaxValue).HasColumnType("decimal(16,4)").IsRequired();
            b.Property(z => z.MinValue).HasColumnType("decimal(16,4)").IsRequired();

            b.HasIndex(z => z.MasterId);
            b.HasIndex(z => z.MasterItemId);
            b.HasIndex(z => z.CreationTime);
        });


        //Master件维护-主表
        builder.Entity<Mam_MasterRepair>(b =>
        {
            //表名映射
            

            //自动配置实体父类的属性
            b.ConfigureByConvention();

            b.HasIndex(z => z.ProductTypeId);
            b.HasIndex(z => z.SnCode);
            b.HasIndex(z => z.CreationTime);
        });

        //Master件维护-子表
        builder.Entity<Mam_MasterRepairItem>(b =>
        {
            //表名映射
            

            //自动配置实体父类的属性
            b.ConfigureByConvention();

            b.HasIndex(z => z.MasterId);
            b.HasIndex(z => z.ProcessId);
            b.HasIndex(z => z.EquipmentId);
            b.HasIndex(z => z.CreationTime);
        });

        //Master件维护-检测表
        builder.Entity<Mam_MasterRepairCheck>(b =>
        {
            //表名映射
            

            //自动配置实体父类的属性
            b.ConfigureByConvention();

            b.Property(z => z.StandardValue).HasColumnType("decimal(16,4)").IsRequired();
            b.Property(z => z.MaxValue).HasColumnType("decimal(16,4)").IsRequired();
            b.Property(z => z.MinValue).HasColumnType("decimal(16,4)").IsRequired();
            b.Property(z => z.Value).HasColumnType("decimal(16,4)").IsRequired();

            b.HasIndex(z => z.ProductTypeId);
            b.HasIndex(z => z.SnCode);
            b.HasIndex(z => z.EquipmentId);
            b.HasIndex(z => z.ProcessId);
            b.HasIndex(z => z.CreationTime);
        });

        //返修管理-主表
        builder.Entity<Mam_ProductRepair>(b =>
        {
            //表名映射
            

            //自动配置实体父类的属性
            b.ConfigureByConvention();
            b.Property(z => z.RepairType).HasDefaultValue("正常返修");

            b.HasIndex(z => z.SnCode);
            b.HasIndex(z => z.Status);
            b.HasIndex(z => z.CreationTime);
        });

        //返修管理-子表
        builder.Entity<Mam_ProductRepairItem>(b =>
        {
            //表名映射
            

            //自动配置实体父类的属性
            b.ConfigureByConvention();

            b.HasIndex(z => z.RepairId);
            b.HasIndex(z => z.SnCode);
            b.HasIndex(z => z.ProcessStatus);
            b.HasIndex(z => z.Status);
            b.HasIndex(z => z.CreationTime);
        });

        //拆解订单
        builder.Entity<MamDisassembleOrder>(b =>
        {
            //表名映射
            

            //自动配置实体父类的属性
            b.ConfigureByConvention();
            b.Property(q => q.DownlineMark).HasDefaultValue(0); // 新增字段给默认值
            b.HasIndex(z => z.CreationTime);
            b.Property(z => z.Status).HasDefaultValue(DisassembleOrderStatusEnum.UnUse);

        });


        //黑名单表
        builder.Entity<MamBarcodeBlack>(b =>
        {
            //表名映射
            

            //自动配置实体父类的属性
            b.ConfigureByConvention();

            b.Property(z => z.IsLockMachine).HasDefaultValue(false);
            b.Property(z => z.Qty).HasColumnType("decimal(16,4)").HasDefaultValue(0);
            b.HasIndex(z => z.ReleaseTime);
            b.HasIndex(z => z.MaterialType);
            b.HasIndex(z => z.CreationTime);
            b.HasIndex(z => new { z.IsLockMachine, z.ReleaseState, z.TraceBarcode, z.CreationTime });
        });


   
        //产品类型适配
        builder.Entity<Mam_ProductVerAdapt>(b =>
        {
            //表名映射
            

            //自动配置实体父类的属性
            b.ConfigureByConvention();

            b.HasIndex(z => z.CreationTime);
            b.HasIndex(z => z.MaterialId);
        });

        //产品清单删除记录
        builder.Entity<MamPlanProductDelete>(b =>
        {
            //表名映射
            

            //自动配置实体父类的属性
            b.ConfigureByConvention();

            //Properties
            b.Property(q => q.OkQty).HasColumnType("decimal(16,4)").IsRequired();
            b.Property(q => q.NgQty).HasColumnType("decimal(16,4)").IsRequired();
            b.Property(q => q.UploadQty).HasColumnType("decimal(16,4)").IsRequired();
            b.Property(q => q.ScrapQty).HasColumnType("decimal(16,4)").IsRequired();
            b.Property(q => q.IsLock).HasDefaultValue(false);

            //创建索引
            b.HasIndex(q => q.SnCode);
            b.HasIndex(q => q.PlanId);
            b.HasIndex(q => q.ProductTypeId);
            b.HasIndex(q => q.IsOneTimeProcess);
            b.HasIndex(q => q.ProcessId);
            b.HasIndex(q => q.EquipmentId);
            b.HasIndex(q => q.CreationTime);
            b.HasIndex(q => q.IsRework);
            b.HasIndex(q => q.Status);
        });

    }
}
