using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Susalem.Sualem.Identity
{
    public class Users
    {
        /// <summary>
        /// 主键
        /// </summary>
        /// 
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// 工号
        /// </summary>
        public string EmployeeNumber { get; set; }

        /// <summary>
        /// 昵称
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }


        /// <summary>
        /// 手机号
        /// </summary>
        public string PhoneNumber { get; set; }

        /// <summary>
        /// 头像路径
        /// </summary>
        public string HeadPortrait { get; set; }

        /// <summary>
        /// 是否启用（0=未启用，1=启用）
        /// </summary>
        public int Enable { get; set; }


        /// <summary>
        /// 创建人Id
        /// </summary>
        public string CreateBy { get; set; }
        /// <summary>
        /// 创建日期
        /// </summary>
        public DateTime? CreateTime { get; set; }
        /// <summary>
        /// 修改人Id
        /// </summary>
        public string UpdateBy { get; set; }
        /// <summary>
        /// 修改日期
        /// </summary>
        public DateTime? UpdateTime { get; set; }
        /// <summary>
        /// 是否删除
        /// </summary>
        public int IsDeleted { get; set; }
        public DateTime? Token { get; set; }
    }
}
