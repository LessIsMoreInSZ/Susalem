using Susalem.Mes;

using System;
using System.ComponentModel.DataAnnotations;

using Volo.Abp.Auditing;
using Volo.Abp.Domain.Entities;

namespace Susalem.Mam.MamBarcodeBlacks
{

    /// <summary>
    /// 【领域实体】精追件黑名单
    /// </summary>
    public class MamBarcodeBlack : Entity<Guid>, IHasCreationTime
    {
        /// <summary>
        /// 工厂 
        /// </summary>
        [StringLength(CommonConsts.StandardFiledMaxLength)]
        public string FactoryNo { get; set; }

        /// <summary>
        /// 产线 
        /// </summary>
        [ StringLength(CommonConsts.StandardFiledMaxLength)]
        public string PdLineNo { get; set; }

        /// <summary>
        /// 工位 
        /// </summary>
        [ StringLength(CommonConsts.StandardFiledMaxLength)]
        public string WorkStationNo { get; set; }

        /// <summary>
        /// 拆解工单号
        /// </summary>
        [ StringLength(CommonConsts.StandardFiledMaxLength)]
        public string WorkOrderNo { get; set; }

        /// <summary>
        /// 订单号 
        /// </summary>
        [ StringLength(CommonConsts.StandardFiledMaxLength)]
        public string OrderNo { get; set; }

        /// <summary>
        /// 订单物料号 
        /// </summary>
        [ StringLength(CommonConsts.StandardFiledMaxLength)]
        public string OrderMaterialNo { get; set; }

        /// <summary>
        /// 父物料号 
        /// </summary>
        [StringLength(CommonConsts.StandardFiledMaxLength)]
        public string ParentMaterialNo { get; set; }

        /// <summary>
        /// 物料号
        /// </summary>
        [ StringLength(CommonConsts.StandardFiledMaxLength)]
        public string MaterialNo { get; set; }

        /// <summary>
        /// 数量
        /// </summary>
        public decimal Qty { get; set; } = 0;

        /// <summary>
        /// 序列号
        /// </summary>
        [ StringLength(CommonConsts.StandardFiledMaxLength100)]
        public string SerialNo { get; set; }

        /// <summary>
        /// 追溯码
        /// </summary>
        [ StringLength(CommonConsts.StandardFiledMaxLength100)]
        public string TraceCode { get; set; }

        /// <summary>
        /// 精追件名称
        /// </summary>
        [ StringLength(CommonConsts.StandardFiledMaxLength)]
        public string TraceName { get; set; }

        /// <summary>
        /// 物料类型 0批次  1精追
        /// </summary>b
        public int MaterialType { get; set; }

        /// <summary>
        /// 追溯条码
        /// </summary>
        [ StringLength(CommonConsts.StandardFiledMaxLength128)]
        public string TraceBarcode { get; set; }

        /// <summary>
        /// 解除状态  TRUE解除  FALSE 锁定
        /// </summary>
        public bool ReleaseState { get; set; }

        /// <summary>
        /// 解除时间
        /// </summary>
        public DateTime? ReleaseTime { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreationTime { get; set; }

        /// <summary>
        /// 数据来源
        /// </summary>
        public string DataSource { get; set; }

        /// <summary>
        /// 是否锁机
        /// </summary>
        public bool IsLockMachine { get;set; }
       
    }
}
