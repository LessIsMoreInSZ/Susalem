using JetBrains.Annotations;

using Susalem.Mes;

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using Volo.Abp.Data;
using Volo.Abp.Domain.Entities.Auditing;
using Susalem.Mam.MamProductStatusEnum;

namespace Susalem.Mam.MamPlanProductWips
{
    /// <summary>
    /// 【领域实体】产品清单实时
    /// </summary>
    public class MamPlanProductWip : AuditedEntity<Guid>, IHasExtraProperties
    {
        /// <summary>
        /// SN编码
        /// </summary>
        [Required, StringLength(CommonConsts.StandardFiledMaxLength80)]
        public string SnCode { get; set; }

        /// <summary>
        /// 【外键】工单ID(外)
        /// </summary>
        [Required]
        public Guid PlanId { get; set; }

        /// <summary>
        /// 【外键】产品类型ID
        /// </summary>
        [Required]
        public Guid ProductTypeId { get; set; }

        /// <summary>
        ///  工序序号
        /// </summary>
        [Required]
        public int Index { get; set; }

        /// <summary>
        /// 【外键】工序ID
        /// </summary>
        [Required]
        public Guid ProcessId { get; set; }

        /// <summary>
        /// 【外键】设备ID（外）
        /// </summary>
        [Required]
        public Guid EquipmentId { get; set; }

        /// <summary>
        ///  托盘编号
        /// </summary>
        [StringLength(CommonConsts.StandardFiledMaxLength80)]
        public string CarrierCode { get; set; }

        /// <summary>
        ///  适配板编号
        /// </summary>
        [StringLength(CommonConsts.StandardFiledMaxLength80)]
        public string AdapterPlateCode { get; set; }

        /// <summary>
        /// 状态枚举
        /// </summary>
        [Required]
        public ProductStatusEnum Status { get; set; }

        /// <summary>
        /// 上线数量
        /// </summary>
        [Required, Column(TypeName = "decimal(16,4)")]
        public decimal UploadQty { get; set; }

        /// <summary>
        /// 合格数量
        /// </summary>
        [Required, Column(TypeName = "decimal(16,4)")]
        public decimal OkQty { get; set; }

        /// <summary>
        /// 不合格数量
        /// </summary>
        [Required, Column(TypeName = "decimal(16,4)")]
        public decimal NgQty { get; set; }

        /// <summary>
        /// 报废数量
        /// </summary>
        [Required, Column(TypeName = "decimal(16,4)")]
        public decimal ScrapQty { get; set; }

        /// <summary>
        /// 加工人
        /// </summary>
        [Required]
        public Guid WorkEmployeeId { get; set; }

        /// <summary>
        /// 是否返修
        /// </summary>
        [Required]
        public bool IsRework { get; set; }

        /// <summary>
        /// 是否一次加工
        /// </summary>
        [Required]
        public bool IsOneTimeProcess { get; set; }

        /// <summary>
        /// 当前工步号
        /// </summary>
        [Required]
        public int CurStepNo { get; set; }

        /// <summary>
        /// 拓展字段
        /// </summary>
        public ExtraPropertyDictionary ExtraProperties { get; set; }

        /// <summary>
        /// 是否锁件
        /// </summary>
        public bool IsLock { get; set; } 


        /// <summary>
        /// 【领域实体】产品清单实时
        /// </summary>
        protected MamPlanProductWip()
        {

        }

        /// <summary>
        /// 【领域实体】产品清单历史
        /// </summary>
        /// <param name="id">id</param>
        /// <param name="snCode">SN编码</param>
        /// <param name="planId">工单Id</param>
        /// <param name="productTypeId">物料类型Id</param>
        /// <param name="index">序号</param>
        /// <param name="processId">工序id</param>
        /// <param name="equipmentId">设备id</param>
        /// <param name="carrierCode">载体编码</param>
        /// <param name="adapterPlateCode">适配板编号</param>
        /// <param name="status">状态</param>
        /// <param name="uploadQty">上线数</param>
        /// <param name="okQty">合格数</param>
        /// <param name="ngQty">不合格数</param>
        /// <param name="scrapQty">报废数</param>
        /// <param name="workEmployeeId">加工人</param>
        /// <param name="isRework">是否返修</param>
        /// <param name="isOneTimeProcess">是否第一次加工</param>
        /// <param name="curStepNo">当前工步号</param>
        /// <param name="extraProperties">拓展字段</param>
        protected MamPlanProductWip(
           Guid id,
           [NotNull] string snCode,
           [NotNull] Guid planId,
           [NotNull] Guid productTypeId,
           [NotNull] int index,
           [NotNull] Guid processId,
           [NotNull] Guid equipmentId,
           [NotNull] string carrierCode,
           string adapterPlateCode,
           [NotNull] ProductStatusEnum status,
           [NotNull] decimal uploadQty,
           [NotNull] decimal okQty,
           [NotNull] decimal ngQty,
           [NotNull] decimal scrapQty,
           [NotNull] Guid workEmployeeId,
           [NotNull] bool isRework,
           [NotNull] bool isOneTimeProcess,
           [NotNull] int curStepNo,
           [CanBeNull] ExtraPropertyDictionary extraProperties = null)
        {
            Id = id;
            SnCode = snCode;
            PlanId = planId;
            ProductTypeId = productTypeId;
            Index = index;
            ProcessId = processId;
            EquipmentId = equipmentId;
            CarrierCode = carrierCode;
            AdapterPlateCode = adapterPlateCode;
            Status = status;
            UploadQty = uploadQty;
            OkQty = okQty;
            NgQty = ngQty;
            ScrapQty = scrapQty;
            WorkEmployeeId = workEmployeeId;
            IsRework = isRework;
            IsOneTimeProcess = isOneTimeProcess;
            CurStepNo = curStepNo;
            ExtraProperties = extraProperties;
        }
    }
}
