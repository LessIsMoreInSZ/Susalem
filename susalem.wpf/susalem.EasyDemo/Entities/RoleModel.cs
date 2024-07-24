using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace susalem.EasyDemo.Entities
{
    [Table("hc_roles")]
    public class RoleModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(name: "role_id")]
        public int RoleId { get; set; }

        [Column(name: "role_name")]
        public string RoleName { get; set; }

        [Column(name: "level")]
        public int Level { get; set; }

        [Column(name: "state")]
        public int State { get; set; }
    }
}
