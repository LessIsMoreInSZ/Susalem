using JetBrains.Annotations;

using Susalem.Mes;

using System;
using System.ComponentModel.DataAnnotations;

using Volo.Abp.Auditing;
using Volo.Abp.Data;
using Volo.Abp.Domain.Entities;

namespace Susalem.Qms.QmsFaultHistories
{
    /// <summary>
    /// 【领域实体】报警记录历史
    /// </summary>
    public class QmsFaultHistory : Entity<Guid>, IHasCreationTime, IHasExtraProperties
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
        /// 故障描述
        /// </summary>
        [Required, StringLength(CommonConsts.StandardFiledMaxLength500)]
        public string FaultDescribe { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        [Required]
        public DateTime StartTime { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime? EndTime { get; set; }

        ///<inheritdoc/>
        [Required]
        public DateTime CreationTime { get; set; }

        /// <summary>
        /// 拓展字段
        /// </summary>
        public ExtraPropertyDictionary ExtraProperties { get; set; }

        /// <summary>
        /// 空构造函数
        /// </summary>
        public QmsFaultHistory() { }
        //public QmsFaultHistory(Guid id)
        //{
        //    Id = id;
        //}

        /// <summary>
        /// 【领域实体】报警记录历史
        /// </summary>
        /// <param name="id">Id</param>
        /// <param name="equipmentId">设备ID</param>
        /// <param name="dayShiftItemId">排班子表ID</param>
        /// <param name="faultDescribe">故障描述</param>
        /// <param name="startTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <param name="creationTime">创建时间</param>
        public QmsFaultHistory(
           [NotNull] Guid id,
           [NotNull] Guid equipmentId,
           [NotNull] Guid dayShiftItemId,
           [NotNull] string faultDescribe,
           [NotNull] DateTime startTime,
           [CanBeNull] DateTime endTime,
           [NotNull] DateTime creationTime)
        {
            Id = id;
            EquipmentId = equipmentId;
            DayShiftItemId = dayShiftItemId;
            FaultDescribe = faultDescribe;
            StartTime = startTime;
            EndTime = endTime;
            CreationTime = creationTime;
        }

    }
}
