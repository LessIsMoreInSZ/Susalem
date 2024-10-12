using JetBrains.Annotations;

using System;
using System.ComponentModel.DataAnnotations;

using Volo.Abp.Data;
using Volo.Abp.Domain.Entities.Auditing;

using Susalem.Mam.OrderEnum;

namespace Susalem.Mam.MamPlanFlowWips
{
    /// <summary>
    /// 【实体】工单工艺流程
    /// </summary>
    public class MamPlanFlowWip : FullAuditedEntity<Guid>, IHasExtraProperties
    {
        /// <summary>
        /// 工单Id（外）
        /// </summary>
        [Required]
        public Guid PlanId { get; set; }

        /// <summary>
        /// 产品类型ID（外）
        /// </summary>
        [Required]
        public Guid ProductTypeId { get; set; }

        /// <summary>
        /// 产品工艺工序Id（外）
        /// </summary>
        [Required]
        public Guid ProductFlowProcessId { get; set; }

        /// <summary>
        /// 工序ID（外）
        /// </summary>
        [Required]
        public Guid ProcessId { get; set; }

        /// <summary>
        /// 序号
        /// </summary>
        [Required]
        public int Index { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        [Required]
        public OrderStatusEnum Status { get; set; }

        /// <summary>
        /// 工单数量
        /// </summary>
        [Required]
        public decimal PlanQty { get; set; }

        /// <summary>
        /// 上线数量
        /// </summary>
        [Required]
        public decimal UploadQty { get; set; }

        /// <summary>
        /// 合格数量
        /// </summary>
        [Required]
        public decimal OkQty { get; set; }

        /// <summary>
        /// 不合格数量
        /// </summary>
        [Required]
        public decimal NgQty { get; set; }

        /// <summary>
        /// 报废数量
        /// </summary>
        [Required]
        public decimal ScrapQty { get; set; }

        /// <summary>
        /// 节拍(秒)
        /// </summary>
        [Required]
        public decimal CycleTime { get; set; }

        /// <summary>
        /// 计时单价
        /// </summary>
        public decimal TimePrice { get; set; }

        /// <summary>
        /// 计件单价
        /// </summary>
        public decimal PiecePrice { get; set; }

        /// <summary>
        /// 是否计件
        /// </summary>
        [Required]
        public bool IsPiecePay { get; set; }

        /// <summary>
        /// 是否可选
        /// </summary>
        [Required]
        public bool IsOptional { get; set; }

        /// <summary>
        /// 是否平行工序
        /// </summary>
        [Required]
        public bool IsParallelProcess { get; set; }

        /// <summary>
        /// 拓展字段
        /// </summary>
        public ExtraPropertyDictionary ExtraProperties { get; set; }

        /// <summary>
        /// 【实体】工单工艺流程
        /// </summary>
        /// <param name="planId">工单id</param>
        /// <param name="productTypeId">产品类型id</param>
        /// <param name="productFlowProcessId">产品工艺工序id</param>
        /// <param name="processId">工序id</param>
        /// <param name="index">序号</param>
        /// <param name="status">状态</param>
        /// <param name="planQty">工单数量</param>
        /// <param name="uploadQty">上线数</param>
        /// <param name="okQty">合格数</param>
        /// <param name="ngQty">不合格数</param>
        /// <param name="scrapQty">报废数</param>
        /// <param name="cycleTime">节拍（秒）</param>
        /// <param name="isPiecePay">是否计件</param>
        /// <param name="isOptional">是否可选</param>
        /// <param name="isParallelProcess">是否平行工序</param>
        /// <param name="timePrice">计时单价</param>
        /// <param name="piecePrice">计件单价</param>
        /// <param name="extraProperties">拓展字段</param>
        public MamPlanFlowWip(
            [NotNull] Guid planId,
            [NotNull] Guid productTypeId,
            [NotNull] Guid productFlowProcessId,
            [NotNull] Guid processId,
            [NotNull] int index,
            [NotNull] OrderStatusEnum status,
            [NotNull] decimal planQty,
            [NotNull] decimal uploadQty,
            [NotNull] decimal okQty,
            [NotNull] decimal ngQty,
            [NotNull] decimal scrapQty,
            [NotNull] decimal cycleTime,
            [NotNull] bool isPiecePay,
            [NotNull] bool isOptional,
            [NotNull] bool isParallelProcess,
            [CanBeNull] decimal timePrice = 0,
            [CanBeNull] decimal piecePrice = 0,
            [CanBeNull] ExtraPropertyDictionary extraProperties = null)
        {
            PlanId = planId;
            ProductTypeId = productTypeId;
            ProductFlowProcessId = productFlowProcessId;
            ProcessId = processId;
            Index = index;
            Status = status;
            PlanQty = planQty;
            UploadQty = uploadQty;
            OkQty = okQty;
            NgQty = ngQty;
            ScrapQty = scrapQty;
            CycleTime = cycleTime;
            TimePrice = timePrice;
            PiecePrice = piecePrice;
            IsPiecePay = isPiecePay;
            IsOptional = isOptional;
            IsParallelProcess = isParallelProcess;
            ExtraProperties = extraProperties;
        }
    }
}