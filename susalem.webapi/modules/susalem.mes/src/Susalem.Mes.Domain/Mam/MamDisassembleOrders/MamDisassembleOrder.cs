using JetBrains.Annotations;

using Susalem.Mes;

using System;
using System.ComponentModel.DataAnnotations;

using Volo.Abp.Auditing;
using Volo.Abp.Domain.Entities;

using Susalem.Mam.MamDisassembleOrderEnum;

namespace Susalem.Mam.MamDisassembleOrders
{
    /// <summary>
    /// 【领域实体】拆解订单
    /// </summary>
    public class MamDisassembleOrder : Entity<Guid>, IHasCreationTime
    {
        /// <summary>
        /// 序列号
        /// </summary>
        [Required, StringLength(CommonConsts.StandardFiledMaxLength80)]
        public string SerialNo { get; set; }

        /// <summary>
        /// 追溯码
        /// </summary>
        [ StringLength(CommonConsts.StandardFiledMaxLength80)]
        public string TraceCode { get; set; }

        /// <summary>
        /// 拆解工单号
        /// </summary>
        [Required, StringLength(CommonConsts.StandardFiledMaxLength)]
        public string WorkOrderNo { get; set; }

        /// <summary>
        /// 产品物料号
        /// </summary>
        [Required, StringLength(CommonConsts.StandardFiledMaxLength80)]
        public string ProductTypeNo { get; set; }

        /// <summary>
        /// 工厂
        /// </summary>
        [Required, StringLength(CommonConsts.StandardFiledMaxLength)]
        public string FactoryNo { get; set; }

        /// <summary>
        /// 产品状态  是否下线
        /// </summary>
        public int DownlineMark { get; set; }

        /// <summary>
        /// 拆解工位
        /// </summary>
        [Required, StringLength(CommonConsts.StandardFiledMaxLength)]
        public string UpStationNo { get; set; }

        /// <summary>
        /// 时间戳
        /// </summary>
        [Required, StringLength(CommonConsts.StandardFiledMaxLength)]
        public DateTime TimeStamp { get; set; }

        /// <summary>
        /// 订单来源
        /// </summary>
        [Required, MaxLength(CommonConsts.StandardFiledMaxLength)]
        public string DataSourceType { get; set; }

        public DisassembleOrderStatusEnum Status { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreationTime { get; set; }

        public MamDisassembleOrder() { }

        /// <summary>
        /// 【领域实体构造函数】外部接收拆解订单
        /// </summary>
        /// <param name="serialNo">序列号</param>
        /// <param name="traceCode">追溯码</param>
        /// <param name="workOrderNo">订单号</param>
        /// <param name="productTypeNo">产品物料号</param>
        /// <param name="dataSoureType">数据来源</param>
        /// <param name="factoryNo">工厂</param>EquipmentId
        /// <param name="downlineMark">上线工位编号</param>
        /// <param name="upStationNo">上线工位编号</param>
        /// <param name="timeStamp">时间戳</param>
        ///<param name= "status" > status</param>
        public MamDisassembleOrder(
             [NotNull] string serialNo,
              string traceCode,
             [NotNull] string workOrderNo,
             [NotNull] string productTypeNo,
             [NotNull] string factoryNo,
              [NotNull]  int downlineMark,
               [NotNull] string dataSoureType,
             [NotNull] string upStationNo,
             [NotNull] DateTime timeStamp,
             [NotNull] DisassembleOrderStatusEnum status)
        {
            SerialNo = serialNo;
            TraceCode = traceCode;
            WorkOrderNo = workOrderNo;
            ProductTypeNo = productTypeNo;
            FactoryNo = factoryNo;
            UpStationNo = upStationNo;
            TimeStamp = timeStamp;
            CreationTime = DateTime.Now;
            DataSourceType = dataSoureType;
            Status = status;
            DownlineMark = downlineMark;
        }
    }
}
