using System.Collections.Generic;

using Susalem.Fam.FamShiftConfigItems;

namespace Susalem.Fam.FamShiftConfigs.Etos
{
    /// <summary>
    /// 班次主子表数据中转Eto
    /// </summary>
    public class FamShiftConfigAndItemEto
    {
        public FamShiftConfig FamShiftConfig { get; set; }

        /// <summary>
        /// 班次子表
        /// </summary>
        public List<FamShiftConfigItem> FamShiftConfigItem { get; set; }


        public FamShiftConfigAndItemEto() { }
    }
}
