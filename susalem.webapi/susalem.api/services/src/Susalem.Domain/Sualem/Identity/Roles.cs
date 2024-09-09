using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Susalem.Sualem.Identity
{
    /// <summary>
    /// 角色表
    /// </summary>
    public class Role
    {
        /// <summary>
        /// ID
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string RoleName { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 等级
        /// </summary>
        public int? RoleSort { get; set; }

        /// <summary>
        /// 创建日期
        /// </summary>
        public DateTime? CreateTime { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime? UpdateTime { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        public string CreateBy { get; set; }

        //更新人
        public string UpdateBy { get; set; }
        /// <summary>
        /// 是否可用
        /// </summary>
        public int? Enable { get; set; }
    }
}
