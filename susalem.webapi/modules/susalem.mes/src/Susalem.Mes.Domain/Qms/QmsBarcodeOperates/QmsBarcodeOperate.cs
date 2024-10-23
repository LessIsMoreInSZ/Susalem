using Susalem.Mes;

using System;
using System.ComponentModel.DataAnnotations;

using Volo.Abp.Auditing;
using Volo.Abp.Data;
using Volo.Abp.Domain.Entities.Auditing;

using Susalem.Qms.QmsBarcodeStateEnum;

namespace Susalem.Qms.QmsBarcodeOperates
{
    /// <summary>
    /// 
    /// </summary>
    public class QmsBarcodeOperate : CreationAuditedEntity<Guid>, IHasExtraProperties, IHasCreationTime
    {
        /// <summary>
        /// 产品sncode
        /// </summary>
        [Required, StringLength(CommonConsts.StandardFiledMaxLength200)]
        public string SnCode { get; set; }

        /// <summary>
        /// barcode
        /// </summary>
        [Required, StringLength(CommonConsts.StandardFiledMaxLength200)]
        public string BarCode { get; set; }

        /// <summary>
        /// 操作类型
        /// </summary>
        [Required]
        public BarcodeStateEnum OperateType { get; set; }

        /// <summary>
        /// 解绑类型
        /// </summary>
        [Required]
        public string UnbindType { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [StringLength(CommonConsts.StandardFiledMaxLength2000)]
        public string UnbindRemark { get; set; }

        ///<inheritdoc/>
        public ExtraPropertyDictionary ExtraProperties { get; set; }
    }
}
