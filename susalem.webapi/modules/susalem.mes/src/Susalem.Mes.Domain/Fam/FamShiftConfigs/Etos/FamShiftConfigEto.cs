using System;

using Volo.Abp.Data;
using Volo.Abp.Domain.Entities;

using Susalem.Fam.FamShiftTagEnum;

namespace Susalem.Fam.FamShiftConfigs.Etos
{
    /// <summary>
    /// 班次管理-主表中转Eto
    /// </summary>
    public class FamShiftConfigEto : Entity<Guid>, IHasExtraProperties
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 序号
        /// </summary>
        public int Index { get; set; }

        /// <summary>
        /// 标识
        /// </summary>
        public ShiftTagEnum Tag { get; set; }

        /// <summary>
        /// 标识名称
        /// </summary>
        public string TagEnum { get; set; }

        /// <summary>
        /// 产线
        /// </summary>
        public string PdLineName { get; set; }

        ///<inheritdoc/>
        public bool IsEnable { get; set; }

        ///<inheritdoc/>
        public string Remark { get; set; }

        ///<inheritdoc/>
        public ExtraPropertyDictionary ExtraProperties { get; set; }

        public FamShiftConfigEto() { }

        public FamShiftConfigEto(Guid id) 
        {
            Id = id;
        }
    }
}
