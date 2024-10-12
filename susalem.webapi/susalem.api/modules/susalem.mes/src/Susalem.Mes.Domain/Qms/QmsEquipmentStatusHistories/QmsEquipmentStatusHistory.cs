using JetBrains.Annotations;

using Susalem.Mes;

using System;
using System.ComponentModel.DataAnnotations;

using Volo.Abp.Auditing;
using Volo.Abp.Data;
using Volo.Abp.Domain.Entities;

using Susalem.Qms.QmsEquipmentStatusEnum;

namespace Susalem.Qms.QmsEquipmentStatusHistories
{
    /// <summary>
    /// 【领域实体】设备状态历史
    /// </summary>
    public class QmsEquipmentStatusHistory : Entity<Guid>, IHasCreationTime, IHasExtraProperties
    {
        /// <summary>
        /// 【外键】设备ID（外）
        /// </summary>
        [Required]
        public Guid EquipmentId { get; set; }

        /// <summary>
        /// 【外键】排班子表ID
        /// </summary>
        [Required]
        public Guid DayShiftItemId { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        [Required]
        public EquipmentStatusEnum Status { get; set; }

        /// <summary>
        /// 模式
        /// </summary>
        [StringLength(CommonConsts.StandardFiledMaxLength)]
        public string Model { get; set; }

        /// <summary>
        /// 托盘号
        /// </summary>
        [StringLength(CommonConsts.StandardFiledMaxLength20)]
        public string PalletNo { get; set; }

        /// <summary>
        /// 来料码
        /// </summary>
        [StringLength(CommonConsts.StandardFiledMaxLength100)]
        public string SnCode { get; set; }

        /// <summary>
        /// 订单号
        /// </summary>
        [StringLength(CommonConsts.StandardFiledMaxLength)]
        public string ErpOrderNo { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        [Required]
        public DateTime StartTime { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime? EndTime { get; set; }

        /// <summary>
        /// 时长(s)
        /// </summary>
        [Required]
        public decimal Duration { get; set; }

        ///<inheritdoc/>
        [Required]
        public DateTime CreationTime { get; set; }

        /// <summary>
        /// 拓展字段
        /// </summary>
        public ExtraPropertyDictionary ExtraProperties { get; set; }

        public QmsEquipmentStatusHistory() { }
        //public QmsEquipmentStatusHistory(Guid id)
        //{
        //    Id = id;
        //}

        /// <summary>
        /// 【领域实体】设备状态历史
        /// </summary>
        /// <param name="id">id</param>
        /// <param name="equipmentId">设备ID</param>
        /// <param name="dayShiftItemId">排班子表ID</param>
        /// <param name="status">状态</param>
        /// <param name="startTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <param name="duration">时长(s)</param>
        /// <param name="creationTime">创建时间</param>
        public QmsEquipmentStatusHistory(
         [NotNull] Guid id,
         [NotNull] Guid equipmentId,
         [NotNull] Guid dayShiftItemId,
         [NotNull] EquipmentStatusEnum status,
         [NotNull] DateTime startTime,
         [CanBeNull] DateTime endTime,
         [NotNull] decimal duration,
         [NotNull] DateTime creationTime)
        {
            Id = id;
            EquipmentId = equipmentId;
            DayShiftItemId = dayShiftItemId;
            Status = status;
            StartTime = startTime;
            EndTime = endTime;
            Duration = duration;
            CreationTime = creationTime;
        }
    }
}
