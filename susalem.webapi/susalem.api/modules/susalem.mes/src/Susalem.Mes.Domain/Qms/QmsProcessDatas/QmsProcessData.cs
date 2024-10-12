using JetBrains.Annotations;

using Susalem.Mes;

using System;
using System.ComponentModel.DataAnnotations;

using Volo.Abp.Auditing;
using Volo.Abp.Data;
using Volo.Abp.Domain.Entities;

using Susalem.Mam.MamProductStatusEnum;
using Susalem.Qms.QmsQualityStatusEnum;

namespace Susalem.Qms.QmsProcessDatas
{
    /// <summary>
    /// 【领域实体】工艺数据
    /// </summary>
    public class QmsProcessData : Entity<Guid>, IHasCreationTime, IHasExtraProperties
    {
        /// <summary>
        /// 产品ID
        /// </summary>
        public Guid ProductId { get; set; }

        /// <summary>
        /// 工单id
        /// </summary>
        public Guid PlanId { get; set; }

        /// <summary>
        /// 产品类型id
        /// </summary>
        public Guid ProductTypeId { get; set; }

        /// <summary>
        /// 设备id 【外键】
        /// </summary>
        public Guid EquipmentId { get; set; }

        /// <summary>
        /// 工艺参数id
        /// </summary>
        public Guid ParaConfigId { get; set; }

        /// <summary>
        /// 工序ID
        /// </summary>
        public Guid ProcessId { get; set; }

        /// <summary>
        /// 值
        /// </summary>
        public decimal Value { get; set; }

        /// <summary>
        /// 状态(枚举1)
        /// </summary>
        public QualityStatusEnum Status { get; set; }

        /// <summary>
        /// 创建类型 1自动 2 手动
        /// </summary>
        public MamPlanProductRecordCreateTypeEnum CreateType { get; set; }

        /// <summary>
        /// 是否最新
        /// </summary>
        public bool IsNew { get; set; }

        /// <summary>
        /// 加工记录ID
        /// </summary>
        public Guid ProductRecordId { get; set; }

        /// <summary>
        /// 工艺参数编码
        /// </summary>
        [Required, StringLength(CommonConsts.StandardFiledMaxLength)]
        public string ParaConfigCode { get; set; }

        /// <summary>
        /// 工艺参数名称
        /// </summary>
        [Required, StringLength(CommonConsts.StandardFiledMaxLength)]
        public string ParaConfigName { get; set; }

        /// <summary>
        /// 工艺参数标准值
        /// </summary>
        public decimal ParaConfigStandardValue { get; set; }

        /// <summary>
        /// 工艺参数单位
        /// </summary>
        [StringLength(CommonConsts.StandardFiledMaxLength20)]
        public string ParaConfigUnit { get; set; }

        /// <summary>
        /// 工艺参数最大值
        /// </summary>
        public decimal ParaConfigMaxValue { get; set; }

        /// <summary>
        /// 工艺参数最小值
        /// </summary>

        public decimal ParaConfigMinValue { get; set; }

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
        /// 产品类型编码
        /// </summary>
        [StringLength(CommonConsts.StandardFiledMaxLength)]
        public string ProductTypeCodeWithVer { get; set; }

        /// <summary>
        /// 工序编码
        /// </summary>
        [Required, StringLength(CommonConsts.StandardFiledMaxLength)]
        public string ProcessCode { get; set; }

        /// <summary>
        /// 工序编码
        /// </summary>
        [Required, StringLength(CommonConsts.StandardFiledMaxLength)]
        public string ProcessName { get; set; }

        /// <summary>
        /// 设备编码
        /// </summary>
        [Required, StringLength(CommonConsts.StandardFiledMaxLength)]
        public string EquipmentCode { get; set; }

        /// <summary>
        /// 	编码
        /// </summary>
        [Required, StringLength(CommonConsts.StandardFiledMaxLength)]
        public string CycleCode { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreationTime { get; set; }

        /// <summary>
        /// 拓展字段
        /// </summary>
        public ExtraPropertyDictionary ExtraProperties { get; set; }

        public QmsProcessData()
        {
            //Id = Guid.NewGuid();
        }
        /// <summary>
        /// 【领域实体】工艺数据
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="productId">产品ID</param>
        /// <param name="planId">工单id</param>
        /// <param name="productTypeId">产品类型id</param>
        /// <param name="equipmentId">设备id</param>
        /// <param name="paraConfigId">工艺参数id</param>
        /// <param name="processId">工序ID</param>
        /// <param name="value">值</param>
        /// <param name="status">状态</param>
        /// <param name="isNew">是否最新</param>
        /// <param name="productRecordId">加工记录ID</param>
        /// <param name="productSnCode">产品SN编码</param>
        /// <param name="planNo">工单号</param>
        /// <param name="productTypeName">产品类型名称</param>
        /// <param name="productTypeCode">产品类型编码</param>
        /// <param name="productTypeCodeWithVer">产品类型编码-版本</param>
        /// <param name="processCode">工序编码</param>
        /// <param name="processName">工序编码</param>
        /// <param name="equipmentCode">设备编码</param>
        /// <param name="cycleCode">编码</param>
        /// <param name="creationTime">创建时间</param>
        public QmsProcessData(
           [NotNull] Guid id,
           [NotNull] Guid productId,
           [NotNull] Guid planId,
           [NotNull] Guid productTypeId,
           [NotNull] Guid equipmentId,
           [NotNull] Guid paraConfigId,
           [NotNull] Guid processId,
           [NotNull] int value,
           [NotNull] QualityStatusEnum status,
           [NotNull] bool isNew,
           [CanBeNull] Guid productRecordId,
           [NotNull] string productSnCode,
           [NotNull] string planNo,
           [NotNull] string productTypeName,
           [NotNull] string productTypeCode,
           [NotNull] string productTypeCodeWithVer,
           [NotNull] string processCode,
           [NotNull] string processName,
           [NotNull] string equipmentCode,
           [NotNull] string cycleCode,
           [NotNull] DateTime creationTime)
        {
            ProductId = id;
            ProductId = productId;
            PlanId = planId;
            ProductTypeId = productTypeId;
            ProcessId = processId;
            Value = value;
            Status = status;
            EquipmentId = equipmentId;
            ParaConfigId = paraConfigId;
            IsNew = isNew;
            ProductRecordId = productRecordId;
            ProductSnCode = productSnCode;
            PlanNo = planNo;
            ProductTypeName = productTypeName;
            ProductTypeCode = productTypeCode;
            ProcessCode = processCode;
            ProcessName = processName;
            EquipmentCode = equipmentCode;
            CycleCode = cycleCode;
            CreationTime = creationTime;
            ProductTypeCodeWithVer = productTypeCodeWithVer;
        }
    }
}
