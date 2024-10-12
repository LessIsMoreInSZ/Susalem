using JetBrains.Annotations;

using Newtonsoft.Json;

using Susalem.Mes;

using System;
using System.ComponentModel.DataAnnotations;

using Volo.Abp.Data;
using Volo.Abp.Domain.Entities.Auditing;

namespace Susalem.Tem.TemMaterialUsedTimeRecords
{
    /// <summary>
    /// 【实体】物料次数管控
    /// </summary>
    public class TemMaterialUsedTimeRecord : FullAuditedEntity<Guid>, IHasExtraProperties
    {

        /// <summary>
        /// 产品工艺工序Id
        /// </summary>
        public Guid ProductFlowProcessId { get; set; }
        /// <summary>
        ///  条码
        /// </summary>
        [Required, StringLength(CommonConsts.StandardFiledMaxLength200)]
        public string Barcode { get; set; }

        /// <summary>
        /// SN编码
        /// </summary>
        [Required, StringLength(CommonConsts.StandardFiledMaxLength80)]
        public string SnCode { get; set; }

        /// <summary>
        /// 物料Id
        /// </summary>
        public Guid MaterialId { get; set; }

        /// <summary>
        /// 工位Id
        /// </summary>
        public Guid EquipmentId { get; set; }

        /// <summary>
        /// 使用次数
        /// </summary>
        public int UsedTime { get; set; }

        /// <summary>
        /// 拓展字段
        /// </summary>
        public ExtraPropertyDictionary ExtraProperties { get; set; }
        public bool IsEnable { get; set; }

        [Required, MaxLength]
        public string Remark { get; set; }
        /// <summary>
        /// 【实体】物料管理
        /// </summary>
        protected TemMaterialUsedTimeRecord() { }

        /// <summary>
        /// 【实体】物料管理
        /// </summary>
        /// <param name="id">主键</param>
        /// <param name="barcode">条码</param>
        /// <param name="snCode">snCode</param>
        /// <param name="equipmentId">设备Id</param>
        /// <param name="materialId">物料Id</param>
        /// <param name="usedTime">使用次数</param>
        /// <param name="isEnable">是否启用</param>
        /// <param name="remark">备注</param>
        /// <param name="extraProperties">拓展字段</param>
        public TemMaterialUsedTimeRecord(
            Guid id,
            [NotNull] string barcode,
            [NotNull] string snCode,
            Guid equipmentId,
            Guid materialId,
            int usedTime,
            bool isEnable,
            [CanBeNull] string remark,
            [CanBeNull] ExtraPropertyDictionary extraProperties = null)
        {
            Id = id;
            Barcode = barcode;
            SnCode = snCode;
            EquipmentId = equipmentId;
            MaterialId = materialId;
            UsedTime = usedTime;
            IsEnable = isEnable;
            Remark = remark;
            ExtraProperties = extraProperties;
        }
    }
}
