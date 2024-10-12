using JetBrains.Annotations;

using Susalem.Mes;

using System;
using System.ComponentModel.DataAnnotations;

using Volo.Abp.Auditing;
using Volo.Abp.Data;
using Volo.Abp.Domain.Entities;

namespace Susalem.Qms.QmsAbnormalRecords
{
    /// <summary>
    /// 【领域实体】异常点记录
    /// </summary>
    public class QmsAbnormalRecord : Entity<Guid>, IHasCreationTime, IHasExtraProperties
    {
        /// <summary>
        /// 【外键】设备ID（外）
        /// </summary>
        [Required]
        public Guid EquipmentId { get; set; }

        /// <summary>
        /// 【外键】员工ID（外）
        /// </summary>
        [Required]
        public Guid EmployeeId { get; set; }

        /// <summary>
        /// 异常类型
        /// </summary>
        [Required]
        public string AbnormalType { get; set; }

        /// <summary>
        /// 异常描述
        /// </summary>
        [Required, StringLength(CommonConsts.StandardFiledMaxLength500)]
        public string Description { get; set; }

        ///<inheritdoc/>
        [Required]
        public DateTime CreationTime { get; set; }

        /// <summary>
        /// 拓展字段
        /// </summary>
        public ExtraPropertyDictionary ExtraProperties { get; set; }

        public QmsAbnormalRecord()
        {
        }

        /// <summary>
        /// 【领域实体】异常点记录
        /// </summary>
        /// <param name="id">Id</param>
        /// <param name="equipmentId">设备ID</param>
        /// <param name="employeeId">员工Id</param>
        /// <param name="abnormalType">异常类型</param>
        /// <param name="description">异常描述</param>
        /// <param name="creationTime">创建时间</param>
        public QmsAbnormalRecord(
           [NotNull] Guid id,
           [NotNull] Guid equipmentId,
           [NotNull] Guid employeeId,
           [NotNull] string abnormalType,
           [CanBeNull] string description,
           [NotNull] DateTime creationTime)
        {
            Id = id;
            EquipmentId = equipmentId;
            EmployeeId = employeeId;
            AbnormalType = abnormalType;
            Description = description;
            CreationTime = creationTime;
        }
    }
}
