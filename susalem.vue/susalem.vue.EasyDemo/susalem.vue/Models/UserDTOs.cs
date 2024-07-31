using System.ComponentModel.DataAnnotations;

namespace susalem.vue.Models
{
    public class RegisterUserDto // 定义用于接收注册请求的数据传输对象
    {
        [Required]
        [StringLength(50, ErrorMessage = "用户名长度不能超过50个字符")]
        public string Username { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "姓名长度不能超过100个字符")]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(100, ErrorMessage = "电子邮件长度不能超过100个字符")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "密码长度不能超过100个字符")]
        public string Password { get; set; }
    }

    public class LoginUserDto // 定义用于接收登录请求的数据传输对象
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }
    public class CreateRoleDto
    {
        [Required]
        [StringLength(50, ErrorMessage = "角色名长度不能超过50个字符")]
        public string Name { get; set; }

        public string Description { get; set; }
    }

    public class AssignRoleDto
    {
        [Required]
        public int UserId { get; set; }

        [Required]
        public int RoleId { get; set; }
    }

    public class UserDto
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public DateTime Created { get; set; }
        public DateTime? LastLoginOn { get; set; }
        public List<UserRoleDto> UserRoles { get; set; }
    }

    public class UserRoleDto
    {
        public int RoleId { get; set; }
        public string RoleName { get; set; }
    }
}
