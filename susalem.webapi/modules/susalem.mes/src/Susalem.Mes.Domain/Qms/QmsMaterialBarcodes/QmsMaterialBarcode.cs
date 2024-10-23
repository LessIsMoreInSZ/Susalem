using JetBrains.Annotations;

using Susalem.Mes;

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using Volo.Abp.Auditing;
using Volo.Abp.Data;
using Volo.Abp.Domain.Entities;

using Susalem.Mam.MamProductStatusEnum;
using Susalem.Qms.QmsBarcodeStateEnum;

namespace Susalem.Qms.QmsMaterialBarcodes
{
    /// <summary>
    /// 【领域实体】物料批次条码
    /// </summary>
    public class QmsMaterialBarcode : Entity<Guid>, IHasCreationTime, IHasExtraProperties
    {
        /// <summary>
        /// 【外键】产品Id
        /// </summary>
        [Required]
        public Guid ProductId { get; set; }

        /// <summary>
        /// 【外键】工单ID
        /// </summary>
        [Required]
        public Guid PlanId { get; set; }

        /// <summary>
        /// 【外键】产品类型ID
        /// </summary>
        [Required]
        public Guid ProductTypeId { get; set; }

        /// <summary>
        /// 【外键】工序ID
        /// </summary>
        [Required]
        public Guid ProcessId { get; set; }

        /// <summary>
        /// 【外键】 设备id
        /// </summary>
        public Guid EquipmentId { get; set; }

        /// <summary>
        /// 【外键】物料ID
        /// </summary>
        [Required]
        public Guid MaterialId { get; set; }

        /// <summary>
        /// 产品code-版本
        /// </summary>
        [StringLength(CommonConsts.StandardFiledMaxLength)]
        public string ProductTypeCodeWithVer { get; set; }

        /// <summary>
        /// 物料编码-版本
        /// </summary>
        [StringLength(CommonConsts.StandardFiledMaxLength)]
        public string MaterialCodeWithVer { get; set; }

        /// <summary>
        ///  条码
        /// </summary>
        [Required, StringLength(CommonConsts.StandardFiledMaxLength200)]
        public string Barcode { get; set; }

        /// <summary>
        /// Erp订单号
        /// </summary>
        [Required, StringLength(CommonConsts.StandardFiledMaxLength100)]
        public string ErpOrderNo { get; set; }

        /// <summary>
        /// 数量
        /// </summary>
        [Required, Column(TypeName = "decimal(16,4)")]
        public decimal Qty { get; set; }

        /// <summary>
        /// 状态枚举
        /// </summary>
        [Required]
        public BarcodeStateEnum Status { get; set; }

        /// <summary>
        /// 创建类型 1自动 2 手动
        /// </summary>
        public MamPlanProductRecordCreateTypeEnum CreateType { get; set; }

        /// <summary>
        ///  是否最新
        /// </summary>
        [Required]
        public bool IsNew { get; set; }

        /// <summary>
        /// 【外键】加工记录ID
        /// </summary>
        public Guid ProductRecordId { get; set; }

        /// <summary>
        /// 产品SN编码
        /// </summary>
        [Required, StringLength(CommonConsts.StandardFiledMaxLength100)]
        public string ProductSnCode { get; set; }

        /// <summary>
        /// 工单号
        /// </summary>
        [Required, StringLength(CommonConsts.StandardFiledMaxLength)]
        public string PlanNo { get; set; }

        /// <summary>
        /// 产品类型名称
        /// </summary>
        [Required, StringLength(CommonConsts.StandardFiledMaxLength)]
        public string ProductTypeName { get; set; }

        /// <summary>
        /// 产品类型编码
        /// </summary>
        [Required, StringLength(CommonConsts.StandardFiledMaxLength)]
        public string ProductTypeCode { get; set; }

        /// <summary>
        /// 物料名称
        /// </summary>
        [Required, StringLength(CommonConsts.StandardFiledMaxLength)]
        public string MaterialName { get; set; }

        /// <summary>
        /// 物料编码
        /// </summary>
        [Required, StringLength(CommonConsts.StandardFiledMaxLength)]
        public string MaterialCode { get; set; }

        /// <summary>
        /// 工序编码
        /// </summary>
        [Required, StringLength(CommonConsts.StandardFiledMaxLength)]
        public string ProcessCode { get; set; }

        /// <summary>
        /// 工序名称
        /// </summary>
        [Required, StringLength(CommonConsts.StandardFiledMaxLength)]
        public string ProcessName { get; set; }

        /// <summary>
        /// 设备code
        /// </summary>
        [Required, StringLength(CommonConsts.StandardFiledMaxLength)]
        public string EquipmentCode { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreationTime { get; set; }

        /// <summary>
        /// 拓展字段
        /// </summary>
        public ExtraPropertyDictionary ExtraProperties { get; set; }

        /// <summary>
        /// 【领域实体】物料批次条码
        /// </summary>
        public QmsMaterialBarcode()
        {
            //Id = Guid.NewGuid();
        }
        /// <summary>
        /// 【领域实体】物料批次条码
        /// </summary>
        /// <param name="id"></param>
        /// <param name="productId">产品Id</param>
        /// <param name="planId">工单ID</param>
        /// <param name="productTypeId">产品类型ID</param>
        /// <param name="processId">工序ID</param>
        /// <param name="equipmentId">设备id</param>
        /// <param name="materialId">物料ID</param>
        /// <param name="barcode">条码</param>
        /// <param name="erpOrderNo">Erp订单号</param>
        /// <param name="qty">数量</param>
        /// <param name="status">状态枚举</param>
        /// <param name="isNew">是否最新</param>
        /// <param name="productRecordId">加工记录ID</param>
        /// <param name="productSnCode">产品SN编码</param>
        /// <param name="planNo">工单号</param>
        /// <param name="productTypeName">产品类型名称</param>
        /// <param name="productTypeCode">产品类型编码</param>
        /// <param name="materialName">物料名称</param>
        /// <param name="materialCode">物料编码</param>
        /// <param name="processCode">工序编码</param>
        /// <param name="processName">工序名称</param>
        /// <param name="equipmentCode">设备编码</param>
        /// <param name="creationTime">创建时间</param>
        public QmsMaterialBarcode(
            Guid id,
            [NotNull] Guid productId,
            [NotNull] Guid planId,
            [NotNull] Guid productTypeId,
            [NotNull] Guid processId,
            [NotNull] Guid equipmentId,
            [NotNull] Guid materialId,
            [NotNull] string barcode,
            [NotNull] string erpOrderNo,
            [NotNull] decimal qty,
            [NotNull] BarcodeStateEnum status,
            [NotNull] bool isNew,
            [CanBeNull] Guid productRecordId,
            [NotNull] string productSnCode,
            [NotNull] string planNo,
            [NotNull] string productTypeName,
            [NotNull] string productTypeCode,
            [NotNull] string materialName,
            [NotNull] string materialCode,
            [NotNull] string processCode,
            [NotNull] string processName,
            [NotNull] string equipmentCode,
            [CanBeNull] DateTime creationTime)
        {
            Id = id;
            ProductId = productId;
            PlanId = planId;
            ProductTypeId = productTypeId;
            ProcessId = processId;
            EquipmentId = equipmentId;
            MaterialId = materialId;
            Barcode = barcode;
            ErpOrderNo = erpOrderNo;
            Qty = qty;
            Status = status;
            IsNew = isNew;
            ProductRecordId = productRecordId;
            ProductSnCode = productSnCode;
            PlanNo = planNo;
            ProductTypeName = productTypeName;
            ProductTypeCode = productTypeCode;
            MaterialName = materialName;
            MaterialCode = materialCode;
            ProcessCode = processCode;
            ProcessName = processName;
            EquipmentCode = equipmentCode;
            CreationTime = creationTime;
        }


    }
}
