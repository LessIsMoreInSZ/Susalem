using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace susalem.EasyDemo.Entities
{
    public class HistoryModel
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int Id { get; set; }

        /// <summary>
        /// 柜号
        /// </summary>
        [Column("CabinetId")]
        public string CabinetId { get; set; }

        /// <summary>
        /// 工匠品名称
        /// </summary>
        [Column("Name")]
        public string? Name { get; set; }

        /// <summary>
        /// 工匠品料号
        /// </summary>
        [Column("PNCode")]
        public string? PNCode { get; set; }

        /// <summary>
        /// 工匠品编号
        /// </summary>
        [Column("SerialNum")]
        public string? SerialNum { get; set; }

        /// <summary>
        /// 机台码
        /// </summary>
        [Column("MachineId")]
        public string? MachineId { get; set; }

        /// <summary>
        /// 开柜时间
        /// </summary>
        [Column("OpenCabinetTime")]
        public DateTime OpenCabinetTime { get; set; }

        /// <summary>
        /// 操作员姓名
        /// </summary>
        [Column("Operater")]
        public string? Operater { get; set; }

        /// <summary>
        /// 操作动作
        /// </summary>
        [Column("Message")]
        public string? Message { get; set; }


    }
}
