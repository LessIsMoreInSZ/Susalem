using System;

using Volo.Abp.Domain.Entities.Auditing;

namespace Susalem.Tem.TemProductFlowChangeRecords
{
    /// <summary>
    /// 【实体】产品工艺变更记录表
    /// </summary>
    public class TemProductFlowChangeRecord : CreationAuditedEntity<Guid>
    {
        /// <summary>
        /// 变更类型
        /// </summary>
        public ChangeTypeEnum ChangeType { get; set; }

        /// <summary>
        /// 产品工艺id
        /// </summary>
        public Guid ProductFlowId { get; set; }

        /// <summary>
        /// 变更详情
        /// </summary>
        public string ChangeInfo { get; set; }

        /// <summary>
        /// 变更所属操作类型
        /// </summary>
        public OperateCategoryEnum OperateCategory { get; set; }

        public TemProductFlowChangeRecord(ChangeTypeEnum changeType, Guid productFlowId, string changeInfo, OperateCategoryEnum operateCategory)
        {
            Id = Guid.NewGuid();
                ChangeType = changeType;
            ProductFlowId = productFlowId;
            ChangeInfo = changeInfo;
            OperateCategory = operateCategory;
        }
    }
}
