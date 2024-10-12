using Susalem.Mes;

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using Volo.Abp.Data;
using Volo.Abp.Domain.Entities.Auditing;

using Susalem.Mam.MamProductStatusEnum;

namespace Susalem.Mam.MamPlanProductDeletes
{

    /// <summary>
    /// 【领域实体】产品清单实时删除记录
    /// </summary>
    public class MamPlanProductDelete : AuditedEntity<Guid>, IHasExtraProperties
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
        ///  载体编码
        /// </summary>
        [StringLength(CommonConsts.StandardFiledMaxLength80)]
        public string CarrierCode { get; set; }

        /// <summary>
        /// 适配板编号
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
        protected MamPlanProductDelete()
        {

        }
    }
}
