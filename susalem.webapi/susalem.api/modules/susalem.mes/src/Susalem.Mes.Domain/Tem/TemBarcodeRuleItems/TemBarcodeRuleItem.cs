using Susalem.Mes;

using System;
using System.ComponentModel.DataAnnotations;

using Volo.Abp.Data;
using Volo.Abp.Domain.Entities.Auditing;

namespace Susalem.Tem.TemBarcodeRuleItems
{
    /// <summary>
    /// 【领域实体】产品条码子表
    /// </summary>
    public class TemBarcodeRuleItem : CreationAuditedEntity<Guid>, IHasExtraProperties
    {
        /// <summary>
        /// 【外键】物料Id
        /// </summary>
        [Required]
        public Guid ProductTypeId { get; set; }

        /// <summary>
        /// 【外键】条码规则主表Id
        /// </summary>
        [Required]
        public Guid BarcodeRuleId { get; set; }

        /// <summary>
        ///  记录值 
        /// </summary>
        [StringLength(CommonConsts.StandardFiledMaxLength)]
        public string Value { get; set; }

        /// <summary>
        /// 类型
        /// </summary>
        [Required]
        public BarcodeRuleTypeEnum Type { get; set; }

        /// <summary>
        /// 顺序值
        /// </summary>
        [Required]
        public int Index { get; set; }

        /// <summary>
        /// 拓展字段
        /// </summary>
        public ExtraPropertyDictionary ExtraProperties { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        [Required]
        public bool IsEnable { get; set; } = true;

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 【领域实体】产品条码子表
        /// </summary>
        protected TemBarcodeRuleItem()
        {

        }
        /// <summary>
        /// 【领域实体】产品条码子表
        /// </summary>
        public TemBarcodeRuleItem(
            Guid id,
            Guid productTypeId, 
            Guid barcodeRuleId, 
            string value, 
            BarcodeRuleTypeEnum type, 
            int index, 
            ExtraPropertyDictionary extraProperties, 
            bool isEnable, 
            string remark)
        {
            Id = id;
            ProductTypeId = productTypeId;
            BarcodeRuleId = barcodeRuleId;
            Value = value;
            Type = type;
            Index = index;
            ExtraProperties = extraProperties;
            IsEnable = isEnable;
            Remark = remark;
        }
    }

}
