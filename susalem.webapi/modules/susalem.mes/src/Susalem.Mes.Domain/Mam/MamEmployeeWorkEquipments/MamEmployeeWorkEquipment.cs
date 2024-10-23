using System;
using System.ComponentModel.DataAnnotations;

using Volo.Abp.Auditing;
using Volo.Abp.Domain.Entities;

namespace Susalem.Mam.MamEmployeeWorkEquipments
{
    /// <summary>
    /// 【领域实体】员工上岗设备关系
    /// </summary>
    public class MamEmployeeWorkEquipment : Entity<Guid>, IHasCreationTime
    {
        /// <summary>
        /// 员工Id
        /// </summary>
        [Required]
        public Guid EmployeeId { get; set; }

        /// <summary>
        /// 设备Id
        /// </summary>
        [Required]
        public Guid EquipmentId { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [Required]
        public DateTime CreationTime { get; set; }

        public MamEmployeeWorkEquipment() { }

        /// <summary>
        /// 【领域实体】员工上岗设备关系
        /// </summary>
        /// <param name="id">主键id</param>
        /// <param name="employeeId">员工Id</param>
        /// <param name="equipmentId">设备Id</param>
        /// <param name="creationTime">创建时间</param>
        public MamEmployeeWorkEquipment(
            Guid id,
            Guid employeeId,
            Guid equipmentId,
            DateTime creationTime)
        {
            Id = id;
            EmployeeId = employeeId;
            EquipmentId = equipmentId;
            CreationTime = creationTime;
        }

    }
}
