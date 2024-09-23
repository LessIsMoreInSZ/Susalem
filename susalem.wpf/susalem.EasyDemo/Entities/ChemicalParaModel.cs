using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace susalem.EasyDemo.Entities
{
    [Table("ChemicalParas")]
    public class ChemicalParaModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(name: "id")]
        public int Id { get; set; }

        /// <summary>
        /// 回温后这个标志位会置1
        /// </summary>
        [Column("IsUse")]
        public bool IsUse { get; set; }

        /// <summary>
        /// 柜号
        /// </summary>
        [Column("CabinetId")]
        public string CabinetId { get; set; }

        /// <summary>
        /// 工匠品名称
        /// </summary>
        [Column(name: "Name")]
        public string Name { get; set; }

        /// <summary>
        /// 工匠品料号
        /// </summary>
        [Column(name: "PNCode")]
        public string PNCode {  get; set; }

        /// <summary>
        /// 工匠品编号
        /// </summary>
        [Column(name: "SerialNum")]
        public string SerialNum { get; set; }

        /// <summary>
        /// 机台码
        /// </summary>
        [Column(name: "MachineId")]
        public string MachineId { get; set; }

        /// <summary>
        /// 回温时间(小时)
        /// </summary>
        [Column(name: "ReheatingTime")]
        public double ReheatingTime { get; set; }

        /// <summary>
        /// 保质期（天）
        /// </summary>
        [Column(name: "ExpirationDate")]
        public double ExpirationDate { get; set; }



    }
}
