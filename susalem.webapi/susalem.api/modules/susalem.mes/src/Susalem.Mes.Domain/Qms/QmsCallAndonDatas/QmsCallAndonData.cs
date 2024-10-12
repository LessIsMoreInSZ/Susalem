using System;

using Volo.Abp.Auditing;
using Volo.Abp.Domain.Entities;

using Susalem.Mam.MamAndonStatusEnum;

namespace Susalem.Qms.QmsCallAndonDatas
{
    public class QmsCallAndonData : Entity<Guid>, IHasCreationTime
    {
        /// <summary>
        /// 工厂id
        /// </summary>
        public Guid FactoryId { get; set; }
        
        /// <summary>
        /// 车间
        /// </summary>
        public Guid WorkShopId { get; set; }
        
        /// <summary>
        /// 产线
        /// </summary>
        public Guid PdLineId { get; set; }

        /// <summary>
        /// 工位号
        /// </summary>
        public string StationNo { get; set; }

        /// <summary>
        /// call状态
        /// </summary>
        public AndonStatusEnum Status { get; set; }

        /// <summary>
        /// 0 触发 、1复位
        /// </summary>
        public int  CallStatus { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreationTime { get; set; }
        public QmsCallAndonData()
        {

        }
    }
}
