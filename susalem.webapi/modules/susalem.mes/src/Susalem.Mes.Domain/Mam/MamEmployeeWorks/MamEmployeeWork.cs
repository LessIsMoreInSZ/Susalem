using JetBrains.Annotations;

using System;
using System.ComponentModel.DataAnnotations;

using Volo.Abp.Data;
using Volo.Abp.Domain.Entities.Auditing;
using Susalem.Mes;
using Susalem.Fam.FamEmployeeWorkTypeEnum;

namespace Susalem.Mam.MamEmployeeWorks
{
    /// <summary>
    /// 【领域实体】员工上岗记录
    /// </summary>
    public class MamEmployeeWork : FullAuditedEntity<Guid>, IHasExtraProperties
    {
        /// <summary>
        /// 员工ID
        /// </summary>
        [Required]
        public Guid EmployeeId { get; set; }

        /// <summary>
        /// 工位ID
        /// </summary>
        [Required]
        public Guid EquipmentId { get; set; }

        /// <summary>
        /// 类型
        /// </summary>
        [Required]
        public EmployeeWorkTypeEnum Type { get; set; }

        /// <summary>
        /// 数据来源
        /// </summary>
        [Required, StringLength(CommonConsts.StandardFiledMaxLength)]
        public string DataSourceType { get; set; }

        /// <summary>
        /// 拓展字段
        /// </summary>
        public ExtraPropertyDictionary ExtraProperties { get; set; }
        public bool IsEnable { get; set; }

        [Required, MaxLength]
        public string Remark { get; set; }
        /// <summary>
        /// 【领域实体】员工上岗记录
        /// </summary>
        public MamEmployeeWork() { }

        /// <summary>
        /// 【领域实体】员工上岗记录
        /// </summary>
        /// <param name="id">主键ID</param>
        /// <param name="employeeId">域账号</param>
        /// <param name="equipmentId">姓名</param>
        /// <param name="type">车间</param>
        /// <param name="dataSourceType">产线</param>
        /// <param name="creationTime">创建时间</param>
        /// <param name="extraProperties">拓展字段</param>
        /// <param name="isEnable">是否启用</param>
        /// <param name="remark">备注</param>
        public MamEmployeeWork(
            [NotNull] Guid id,
            [NotNull] Guid employeeId,
            [NotNull] Guid equipmentId,
            [NotNull] EmployeeWorkTypeEnum type,
            [NotNull] string dataSourceType,
            [NotNull] DateTime creationTime,
            bool isEnable = true,
            [CanBeNull] ExtraPropertyDictionary extraProperties = null,
            [CanBeNull] string remark = null)
        {
            Id = id;
            EmployeeId = employeeId;
            EquipmentId = equipmentId;
            Type = type;
            DataSourceType = dataSourceType;
            CreationTime = creationTime;
            IsEnable = isEnable;
            Remark = remark;
            ExtraProperties = extraProperties;
        }
    }
}
