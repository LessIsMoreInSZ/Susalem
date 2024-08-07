﻿using Microsoft.AspNetCore.Mvc;
using susalem.vue.Models;
using susalem.vue.Services;

namespace susalem.vue.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService; 
        private readonly ILogger<UserController> _logger;

        public UserController(UserService userService, ILogger<UserController> logger) // 构造函数，注入用户服务和日志记录器
        {
            _userService = userService;
            _logger = logger; 
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserDto registerUserDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var registeredUser = await _userService.Register(registerUserDto.Username, registerUserDto.Name, registerUserDto.Email, registerUserDto.Password);
                return CreatedAtAction(nameof(GetById), new { id = registeredUser.Id }, registeredUser);
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("用户名已存在"))
                {
                    return Conflict(new { message = "用户名已存在" });
                }

                _logger.LogError(ex, "注册失败");
                return BadRequest(new { message = "注册失败", error = ex.Message });
            }
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginUserDto loginUserDto) 
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var user = await _userService.Login(loginUserDto.Username, loginUserDto.Password); 
                if (user != null)
                {
                    var token = _userService.GenerateJwtToken(user); // 生成JWT令牌
                    return Ok(new { message = "登录成功", token, userId = user.Id }); 
                }
                else
                {
                    return Unauthorized(new { message = "无效的用户名或密码" }); 
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "登录失败");
                return StatusCode(500, new { message = "登录失败", error = ex.Message });
            }
        }

        [HttpGet("ID")] //获取用户信息
        public async Task<IActionResult> GetById(int id)
        {
            var userDto = await _userService.GetUserById(id);
            if (userDto == null)
            {
                return NotFound(new { message = "用户未找到" });
            }
            return Ok(userDto);
        }

        [HttpGet("ID/roles")] //获取用户角色信息
        public async Task<IActionResult> GetUserRoles(int id)
        {
            var userRoles = await _userService.GetUserRoles(id);
            if (userRoles == null || userRoles.Count == 0)
            {
                return NotFound(new { message = "未找到用户角色信息" }); 
            }
            return Ok(userRoles);
        }

        [HttpPost("CreateRole")] //创建角色
        public async Task<IActionResult> CreateRole([FromBody] CreateRoleDto createRoleDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var role = await _userService.CreateRole(createRoleDto.Name, createRoleDto.Description);
                return CreatedAtAction(nameof(CreateRole), new { id = role.Id }, role);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "创建角色失败");
                return StatusCode(500, new { message = "创建角色失败", error = ex.Message });
            }
        }

        [HttpPost("AssignRoleToUser")] //分配角色
        public async Task<IActionResult> AssignRoleToUser([FromBody] AssignRoleDto assignRoleDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _userService.AssignRoleToUser(assignRoleDto.UserId, assignRoleDto.RoleId);
                return Ok(new { message = "角色分配成功" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "角色分配失败");
                return StatusCode(500, new { message = "角色分配失败", error = ex.Message });
            }
        }

        [HttpPost("ChangeUserRole")] //改变用户角色
        public async Task<IActionResult> ChangeUserRole([FromBody] ChangeUserRoleDto changeUserRoleDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _userService.ChangeUserRole(changeUserRoleDto.UserId, changeUserRoleDto.NewRoleId);
                return Ok(new { message = "用户角色更改成功" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "更改用户角色失败");
                return StatusCode(500, new { message = "更改用户角色失败", error = ex.Message });
            }
        }

        [HttpDelete("{userId:int}/roles/{roleId:int}")] //删除用户角色
        public async Task<IActionResult> RemoveUserRole(int userId, int roleId)
        {
            try
            {
                await _userService.ChangeUserRole(userId, roleId);
                return Ok(new { message = "用户角色删除成功" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "删除用户角色失败");
                return StatusCode(500, new { message = "删除用户角色失败", error = ex.Message });
            }
        }

        [HttpGet("Test")]
        public async Task<IActionResult> Test()
        {
            try
            {
                return Ok(new { message = "666" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "删除用户角色失败");
                return StatusCode(500, new { message = "删除用户角色失败", error = ex.Message });
            }
        }
    }
}
