using Susalem.Mes;

using System;
using System.ComponentModel.DataAnnotations;

using Volo.Abp.Auditing;
using Volo.Abp.Data;
using Volo.Abp.Domain.Entities;

namespace Susalem.Qms.QmsStationCycleTimes
{
    /// <summary>
    /// 【领域实体】工位节拍
    /// </summary>
    public class QmsStationCycleTime : Entity<Guid>, IHasExtraProperties, IHasCreationTime
    {
        /// <summary>
        /// 工位编码
        /// </summary>
        [Required, StringLength(CommonConsts.StandardFiledMaxLength)]
        public string EquipmentCode { get; set; }

        /// <summary>
        /// 产品类型编码
        /// </summary>
        [StringLength(CommonConsts.StandardFiledMaxLength)]
        public string ProductTypeCode { get; set; }

        /// <summary>
        /// SnCode
        /// </summary>
        [StringLength(CommonConsts.StandardFiledMaxLength200)]
        public string SnCode { get; set; }

        /// <summary>
        /// 托盘号
        /// </summary>
        [StringLength(CommonConsts.StandardFiledMaxLength20)]
        public string PalletNo { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        [Required]
        public DateTime StartTime { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        [Required]
        public DateTime EndTime { get; set; }

        /// <summary>
        /// 加工节拍
        /// </summary>
        [Required]
        public decimal CycleTime { get; set; }

        /// <summary>
        /// 放行时间
        /// </summary>
        [Required]
        public DateTime PassTime { get; set; }

        /// <summary>
        /// 循环节拍
        /// </summary>
        [Required]
        public decimal StationCycleTime { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        [Required, StringLength(CommonConsts.StandardFiledMaxLength)]
        public string ProductStatus { get; set; }

        /// <summary>
        /// 工作员工 默认ye.tian.o   
        /// </summary>
        [StringLength(CommonConsts.StandardFiledMaxLength)]
        public string WorkEmpId { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>

        public DateTime CreationTime { get; set; }

        /// <summary>
        /// 拓展字段
        /// </summary>

        public ExtraPropertyDictionary ExtraProperties { get; set; }
    }
}
