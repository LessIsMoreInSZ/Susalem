using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace susalem.EasyDemo.Models
{
    [Table("hc_users")]
    public class UserModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(name: "user_id")]
        public int UserId { get; set; }

        [Column(name: "role_id")]
        public int RoleId { get; set; }

        [Column(name: "user_name")]
        public string? UserName { get; set; }
        [Column(name: "password")]
        public string? Password { get; set; }
        [Column(name: "user_icon")]
        public string? UserIcon { get; set; }
        [Column("real_name")]
        public string? RealName { get; set; }
        [Column(name: "state")]
        public int State { get; set; }
        [Column("card_id")]
        public string? CardId { get; set; }
        [Column("fingerprint_id")]
        public string? FingerprintId { get; set; }
    }
}
