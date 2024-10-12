using Microsoft.EntityFrameworkCore;

using Susalem.Ecm.EcmRecordHistories;
using Susalem.Ecm.EcmRecordWips;
using Susalem.Ecm.EcmStructItems;
using Susalem.Ecm.EcmStructs;
using Susalem.EntityFrameworkCore.DbContextModelCreatingExtensions;
using Susalem.Eqm.EqmAccidentEntrys;
using Susalem.Eqm.EqmArchives;
using Susalem.Eqm.EqmEquipments;
using Susalem.Eqm.EqmFaultConfigs;
using Susalem.Eqm.EqmModuleTypes;
using Susalem.Eqm.Susalem.MachLifeMs;
using Susalem.Fam;
using Susalem.Fam.FamDayShiftItems;
using Susalem.Fam.FamDayShifts;
using Susalem.Fam.FamEmployees;
using Susalem.Fam.FamPdLines;
using Susalem.Fam.FamShiftConfigItems;
using Susalem.Fam.FamShiftConfigs;
using Susalem.Fam.FamWorkShops;
using Susalem.Mam.MamBarcodeBlacks;
using Susalem.Mam.MamDisassembleOrders;
using Susalem.Mam.MamEmployeeWorkEquipments;
using Susalem.Mam.MamEmployeeWorks;
using Susalem.Mam.MamMasterRepairChecks;
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
using Susalem.Mes.EntityFrameworkCore;
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
using Susalem.Settings;
using Susalem.Sys;
using Susalem.Sys.Dictionaries;
using Susalem.Sys.SysDictionaryItems;
using Susalem.Sys.SysOperations;
using Susalem.Tem.TemBarcodeRuleItems;
using Susalem.Tem.TemBarcodeRules;
using Susalem.Tem.TemFlowEquipments;
using Susalem.Tem.TemMaterials;
using Susalem.Tem.TemMaterialUsedTimes;
using Susalem.Tem.TemParaTypes;
using Susalem.Tem.TemPfpsParaConfigs;
using Susalem.Tem.TemPfpsProductBoms;
using Susalem.Tem.TemProcesses;
using Susalem.Tem.TemProductFlowChangeRecords;
using Susalem.Tem.TemProductFlowProcesses;
using Susalem.Tem.TemProductFlowProcessSteps;
using Susalem.Tem.TemProductFlows;
using Susalem.Tem.TemProductFlowTimeSpans;
using Susalem.Tem.TemStepTypes;
using Susalem.Tem.TemVirtualMaterialRelations;
using Susalem.Wms.WmsCallMaterialTaskHistories;
using Susalem.Wms.WmsCallMaterialTaskWips;
using Susalem.Wms.WmsLocations;
using Susalem.Wms.WmsMaterialPositionRules;
using Susalem.Wms.WmsShelfs;
using Susalem.Wms.WmsStockInOutItems;
using Susalem.Wms.WmsStockInOuts;
using Susalem.Wms.WmsStockWips;
using Susalem.Wms.WmsWarehouses;

using Volo.Abp.AuditLogging.EntityFrameworkCore;
using Volo.Abp.BackgroundJobs.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.FeatureManagement.EntityFrameworkCore;
using Volo.Abp.Identity;
using Volo.Abp.Identity.EntityFrameworkCore;
using Volo.Abp.OpenIddict.EntityFrameworkCore;
using Volo.Abp.PermissionManagement.EntityFrameworkCore;
using Volo.Abp.SettingManagement.EntityFrameworkCore;
using Volo.Abp.TenantManagement;
using Volo.Abp.TenantManagement.EntityFrameworkCore;

namespace Susalem.EntityFrameworkCore;

[ReplaceDbContext(typeof(IIdentityDbContext))]
[ReplaceDbContext(typeof(ITenantManagementDbContext))]
[ReplaceDbContext(typeof(IMesDbContext))]
[ConnectionStringName("Default")]
public class SusalemDbContext :
    AbpDbContext<SusalemDbContext>,
    IIdentityDbContext,
    ITenantManagementDbContext,
    IMesDbContext
{

    #region Entities from the modules
    //Identity
    public DbSet<IdentityUser> Users { get; set; }
    public DbSet<IdentityRole> Roles { get; set; }
    public DbSet<IdentityClaimType> ClaimTypes { get; set; }
    public DbSet<OrganizationUnit> OrganizationUnits { get; set; }
    public DbSet<IdentitySecurityLog> SecurityLogs { get; set; }
    public DbSet<IdentityLinkUser> LinkUsers { get; set; }

    // Tenant Management
    public DbSet<Tenant> Tenants { get; set; }
    public DbSet<TenantConnectionString> TenantConnectionStrings { get; set; }

    #endregion

    public SusalemDbContext(DbContextOptions<SusalemDbContext> options)
        : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
       
        /* Include modules to your migration db context */
        // 重新配置表前缀
        SusalemSettings.ConfigureDataTableName();

        #region 系统表
        builder.ConfigurePermissionManagement();
        builder.ConfigureSettingManagement();
        builder.ConfigureBackgroundJobs();
        builder.ConfigureAuditLogging();
        builder.ConfigureIdentity();
        builder.ConfigureOpenIddict();
        builder.ConfigureFeatureManagement();
        builder.ConfigureTenantManagement();
        #endregion
        #region MES
        builder.ConfigureService();
        #endregion


    }
    #region 系统表

    public DbSet<MamPlanProductDelete> MamPlanProductDeletes { get; set; }
    public DbSet<QmsStationCycleTime> QmsStationCycleTimes { get; set; }
    public DbSet<QmsBarcodeOperate> QmsBarcodeOperates { get; set; }
    public DbSet<FamFactory> FamFactories { get; set; }
    public DbSet<FamWorkShop> FamWorkShops { get; set; }
    public DbSet<FamPdLine> FamPdLines { get; set; }
    public DbSet<FamShiftConfig> FamShiftConfigs { get; set; }
    public DbSet<FamShiftConfigItem> FamShiftConfigItems { get; set; }
    public DbSet<FamDayShift> FamDayShifts { get; set; }
    public DbSet<FamDayShiftItem> FamDayShiftItems { get; set; }
    public DbSet<FamEmployee> FamEmployees { get; set; }
    public DbSet<MamPlanProductWip> MamPlanProductWips { get; set; }
    public DbSet<MamPlanProductHistory> MamPlanProductHistories { get; set; }
    public DbSet<MamPlanProductRecordWip> MamPlanProductRecordWips { get; set; }
    public DbSet<MamOrder> MamOrders { get; set; }
    public DbSet<MamProductionPlan> MamProductionPlans { get; set; }
    public DbSet<MamPlan> MamPlans { get; set; }
    public DbSet<MamPlanFlowWip> MamPlanFlowWips { get; set; }
    public DbSet<MamPlanFlowHistory> MamPlanFlowHistories { get; set; }
    public DbSet<MamEmployeeWorkEquipment> MamEmployeeWorkEquipments { get; set; }
    public DbSet<MamPlanProductRecordHistory> MamPlanProductRecordHistories { get; set; }
    public DbSet<MamOrderBom> MamOrderBoms { get; set; }
    public DbSet<MamEmployeeWork> MamEmployeeWorks { get; set; }
    public DbSet<MamProductRelation> MamProductRelations { get; set; }
    public DbSet<MamSpotCheckRecord> MamSpotCheckRecords { get; set; }
    public DbSet<TemMaterialUsedTime> TemMaterialUsedTimes { get; set; }
    public DbSet<TemMaterial> TemMaterials { get; set; }
    public DbSet<TemParaType> TemParaTypes { get; set; }
    public DbSet<TemProcess> TemProcesses { get; set; }
    public DbSet<TemStepType> TemStepTypes { get; set; }
    public DbSet<TemProductFlow> TemProductFlows { get; set; }
    public DbSet<TemProductFlowProcess> TemProductFlowProcesses { get; set; }
    public DbSet<TemProductFlowTimeSpan> TemProductFlowTimeSpans { get; set; }
    public DbSet<TemFlowEquipment> TemFlowEquipments { get; set; }
    public DbSet<TemProductFlowProcessStep> TemProductFlowProcessSteps { get; set; }
    public DbSet<TemPfpsParaConfig> TemPfpsParaConfigs { get; set; }
    public DbSet<TemPfpsProductBom> TemPfpsProductBoms { get; set; }
    public DbSet<TemBarcodeRule> TemBarcodeRules { get; set; }
    public DbSet<TemBarcodeRuleItem> TemBarcodeRuleItems { get; set; }
    public DbSet<QmsMaterialBarcode> QmsMaterialBarcodes { get; set; }
    public DbSet<QmsProcessData> QmsProcessDatas { get; set; }
    public DbSet<QmsFaultWip> QmsFaultWips { get; set; }
    public DbSet<QmsAbnormalRecord> QmsAbnormalRecords { get; set; }
    public DbSet<QmsFaultHistory> QmsFaultHistories { get; set; }
    public DbSet<QmsEquipmentStatusWip> QmsEquipmentStatusWips { get; set; }
    public DbSet<QmsEquipmentStatusHistory> QmsEquipmentStatusHistories { get; set; }
    public DbSet<EqmEquipment> EqmEquipments { get; set; }
    public DbSet<EqmModuleType> EqmModuleTypes { get; set; }
    public DbSet<EqmArchive> EqmArchives { get; set; }
    public DbSet<EqmFaultConfig> EqmFaultConfigs { get; set; }
    public DbSet<EqmAccidentEntry> EqmAccidentEntrys { get; set; }
    public DbSet<TemVirtualMaterialRelation> TemVirtualMaterialRelations { get; set; }
    public DbSet<WmsStockWip> WmsStockWips { get; set; }
    public DbSet<WmsStockInOut> WmsStockInOuts { get; set; }
    public DbSet<WmsStockInOutItem> WmsStockInOutItems { get; set; }
    public DbSet<WmsMaterialPositionRule> WmsMaterialPositionRules { get; set; }
    public DbSet<WmsWarehouse> WmsWarehouses { get; set; }
    public DbSet<WmsShelf> WmsShelfs { get; set; }
    public DbSet<WmsLocation> WmsLocations { get; set; }
    public DbSet<WmsCallMaterialTaskWip> WmsCallMaterialTaskWips { get; set; }
    public DbSet<WmsCallMaterialTaskHistory> WmsCallMaterialTaskHistories { get; set; }
    public DbSet<SysCodeRule> SysCodeRules { get; set; }
    public DbSet<SysOperation> SysOperations { get; set; }
    public DbSet<SysDictionary> SysDictionaries { get; set; }
    public DbSet<SysDictionaryItem> SysDictionaryItems { get; set; }
    public DbSet<EcmStruct> EcmStructs { get; set; }
    public DbSet<EcmStructItem> EcmStructItems { get; set; }
    public DbSet<EcmRecordWip> EcmRecordWips { get; set; }
    public DbSet<EcmRecordHistory> EcmRecordHistories { get; set; }
    public DbSet<MamRepairPolicy> MamRepairPolicies { get; set; }
    public DbSet<MamMasterRepair> MamMasterRepairs { get; set; }
    public DbSet<MamProductRepair> MamProductRepairs { get; set; }
    public DbSet<MamProductRepairItem> MamProductRepairItems { get; set; }
    public DbSet<MamMasterRepairItem> MamMasterRepairItems { get; set; }
    public DbSet<MamMasterRepairCheck> MamMasterRepairChecks { get; set; }
    public DbSet<MamBarcodeBlack> MamBarcodeBlacks { get; set; }
    public DbSet<MamDisassembleOrder> MamDisassembleOrders { get; set; }
    public DbSet<QmsCallAndonData> QmsCallAndonDatas { get; set; }
    public DbSet<MamProductVerAdapt> MamProductVerAdapts { get; set; }
    public DbSet<TemProductFlowChangeRecord> TemProductFlowChangeRecords { get; set; }
    public DbSet<EqmMachLifeM> MachLifeMs { get; set; }
    #endregion
}
