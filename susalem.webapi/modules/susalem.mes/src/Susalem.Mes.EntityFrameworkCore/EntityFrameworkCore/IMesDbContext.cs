using Microsoft.EntityFrameworkCore;
using Susalem.Ecm.EcmRecordHistories;
using Susalem.Ecm.EcmRecordWips;
using Susalem.Ecm.EcmStructItems;
using Susalem.Ecm.EcmStructs;
using Susalem.Eqm.EqmAccidentEntrys;
using Susalem.Eqm.EqmArchives;
using Susalem.Eqm.EqmEquipments;
using Susalem.Eqm.EqmFaultConfigs;
using Susalem.Eqm.EqmModuleTypes;
using Susalem.Eqm.Susalem.MachLifeMs;
using Susalem.Fam.FamDayShiftItems;
using Susalem.Fam.FamDayShifts;
using Susalem.Fam.FamEmployees;
using Susalem.Fam.FamPdLines;
using Susalem.Fam.FamShiftConfigItems;
using Susalem.Fam.FamShiftConfigs;
using Susalem.Fam.FamWorkShops;
using Susalem.Fam;
using Susalem.Mam.MamBarcodeBlacks;
using Susalem.Mam.MamDisassembleOrders;
using Susalem.Mam.MamEmployeeWorkEquipments;
using Susalem.Mam.MamEmployeeWorks;
using Susalem.Mam.MamMasterRepairChecks;
using Susalem.Mam.MamMasterRepairItems;
using Susalem.Mam.MamMasterRepairs;
using Susalem.Mam.MamOrderBoms;
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
using Susalem.Sys.Dictionaries;
using Susalem.Sys.SysDictionaryItems;
using Susalem.Sys.SysOperations;
using Susalem.Sys;
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
using System;

using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;
using Susalem.Mam.MamOrders;

namespace Susalem.Mes.EntityFrameworkCore;

[ConnectionStringName(MesDbProperties.ConnectionStringName)]
public interface IMesDbContext : IEfCoreDbContext
{
    /* Add DbSet for each Aggregate Root here. Example:
     * DbSet<Question> Questions { get; }
     */
   DbSet<MamPlanProductDelete> MamPlanProductDeletes { get; set; }
   DbSet<QmsStationCycleTime> QmsStationCycleTimes { get; set; }
   DbSet<QmsBarcodeOperate> QmsBarcodeOperates { get; set; }
   DbSet<FamFactory> FamFactories { get; set; }
   DbSet<FamWorkShop> FamWorkShops { get; set; }
   DbSet<FamPdLine> FamPdLines { get; set; }
   DbSet<FamShiftConfig> FamShiftConfigs { get; set; }
   DbSet<FamShiftConfigItem> FamShiftConfigItems { get; set; }
   DbSet<FamDayShift> FamDayShifts { get; set; }
   DbSet<FamDayShiftItem> FamDayShiftItems { get; set; }
   DbSet<FamEmployee> FamEmployees { get; set; }
   DbSet<MamPlanProductWip> MamPlanProductWips { get; set; }
   DbSet<MamPlanProductHistory> MamPlanProductHistories { get; set; }
   DbSet<MamPlanProductRecordWip> MamPlanProductRecordWips { get; set; }
   DbSet<MamOrder> MamOrders { get; set; }
   DbSet<MamProductionPlan> MamProductionPlans { get; set; }
   DbSet<MamPlan> MamPlans { get; set; }
   DbSet<MamPlanFlowWip> MamPlanFlowWips { get; set; }
   DbSet<MamPlanFlowHistory> MamPlanFlowHistories { get; set; }
   DbSet<MamEmployeeWorkEquipment> MamEmployeeWorkEquipments { get; set; }
   DbSet<MamPlanProductRecordHistory> MamPlanProductRecordHistories { get; set; }
   DbSet<MamOrderBom> MamOrderBoms { get; set; }
   DbSet<MamEmployeeWork> MamEmployeeWorks { get; set; }
   DbSet<MamProductRelation> MamProductRelations { get; set; }

   DbSet<MamSpotCheckRecord> MamSpotCheckRecords { get; set; }

   DbSet<TemMaterialUsedTime> TemMaterialUsedTimes { get; set; }
   DbSet<TemMaterial> TemMaterials { get; set; }
   DbSet<TemParaType> TemParaTypes { get; set; }
   DbSet<TemProcess> TemProcesses { get; set; }
   DbSet<TemStepType> TemStepTypes { get; set; }
   DbSet<TemProductFlow> TemProductFlows { get; set; }
   DbSet<TemProductFlowProcess> TemProductFlowProcesses { get; set; }
   DbSet<TemProductFlowTimeSpan> TemProductFlowTimeSpans { get; set; }
   DbSet<TemFlowEquipment> TemFlowEquipments { get; set; }
   DbSet<TemProductFlowProcessStep> TemProductFlowProcessSteps { get; set; }
   DbSet<TemPfpsParaConfig> TemPfpsParaConfigs { get; set; }
   DbSet<TemPfpsProductBom> TemPfpsProductBoms { get; set; }
   DbSet<TemBarcodeRule> TemBarcodeRules { get; set; }
   DbSet<TemBarcodeRuleItem> TemBarcodeRuleItems { get; set; }
   DbSet<QmsMaterialBarcode> QmsMaterialBarcodes { get; set; }
   DbSet<QmsProcessData> QmsProcessDatas { get; set; }
   DbSet<QmsFaultWip> QmsFaultWips { get; set; }

   DbSet<QmsAbnormalRecord> QmsAbnormalRecords { get; set; }
   DbSet<QmsFaultHistory> QmsFaultHistories { get; set; }
   DbSet<QmsEquipmentStatusWip> QmsEquipmentStatusWips { get; set; }
   DbSet<QmsEquipmentStatusHistory> QmsEquipmentStatusHistories { get; set; }
   DbSet<EqmEquipment> EqmEquipments { get; set; }
   DbSet<EqmModuleType> EqmModuleTypes { get; set; }
   DbSet<EqmArchive> EqmArchives { get; set; }
   DbSet<EqmFaultConfig> EqmFaultConfigs { get; set; }
   DbSet<EqmAccidentEntry> EqmAccidentEntrys { get; set; }
   DbSet<TemVirtualMaterialRelation> TemVirtualMaterialRelations { get; set; }
   DbSet<WmsStockWip> WmsStockWips { get; set; }
   DbSet<WmsStockInOut> WmsStockInOuts { get; set; }
   DbSet<WmsStockInOutItem> WmsStockInOutItems { get; set; }
   DbSet<WmsMaterialPositionRule> WmsMaterialPositionRules { get; set; }
   DbSet<WmsWarehouse> WmsWarehouses { get; set; }
   DbSet<WmsShelf> WmsShelfs { get; set; }
   DbSet<WmsLocation> WmsLocations { get; set; }
   DbSet<WmsCallMaterialTaskWip> WmsCallMaterialTaskWips { get; set; }
   DbSet<WmsCallMaterialTaskHistory> WmsCallMaterialTaskHistories { get; set; }
   DbSet<SysCodeRule> SysCodeRules { get; set; }
   DbSet<SysOperation> SysOperations { get; set; }
   DbSet<SysDictionary> SysDictionaries { get; set; }
   DbSet<SysDictionaryItem> SysDictionaryItems { get; set; }
   DbSet<EcmStruct> EcmStructs { get; set; }
   DbSet<EcmStructItem> EcmStructItems { get; set; }
   DbSet<EcmRecordWip> EcmRecordWips { get; set; }
   DbSet<EcmRecordHistory> EcmRecordHistories { get; set; }
   DbSet<MamRepairPolicy> MamRepairPolicies { get; set; }
   DbSet<MamMasterRepair> MamMasterRepairs { get; set; }
   DbSet<MamProductRepair> MamProductRepairs { get; set; }
   DbSet<MamProductRepairItem> MamProductRepairItems { get; set; }
   DbSet<MamMasterRepairItem> MamMasterRepairItems { get; set; }
   DbSet<MamMasterRepairCheck> MamMasterRepairChecks { get; set; }
   DbSet<MamBarcodeBlack> MamBarcodeBlacks { get; set; }
   DbSet<MamDisassembleOrder> MamDisassembleOrders { get; set; }
   DbSet<QmsCallAndonData> QmsCallAndonDatas { get; set; }
   DbSet<MamProductVerAdapt> MamProductVerAdapts { get; set; }//产品版本适配-表

   DbSet<TemProductFlowChangeRecord> TemProductFlowChangeRecords { get; set; }
   DbSet<EqmMachLifeM> MachLifeMs { get; set; }
}
