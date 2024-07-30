using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using susalem.vue.Data;
using susalem.vue.Models;
using System.Security.Claims;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Cryptography;

namespace susalem.vue.Services
{
    public class UserService
    {
        private readonly UserDbContext _dbContext; 
        private readonly IConfiguration _configuration; 

        public UserService(UserDbContext dbContext, IConfiguration configuration) 
        {
            _dbContext = dbContext; 
            _configuration = configuration; 
        }

        public async Task<User> Register(string username, string name, string email, string password) 
        {
            if (await _dbContext.Users.AnyAsync(u => u.Username == username))
            {
                throw new Exception("用户名已存在");
            }
            var user = new User 
            {
                Username = username,
                Name = name,
                Email = email,
                Created = DateTime.Now
            };

            CreatePasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt); // 生成密码哈希和盐值
            user.PasswordHash = passwordHash; // 将生成的密码哈希赋值给用户对象
            user.PasswordSalt = passwordSalt; // 将生成的密码盐值赋值给用户对象

            _dbContext.Users.Add(user); 
            await _dbContext.SaveChangesAsync(); 
            await AssignDefaultRole(user.Id);
            return user; 
        }

        public async Task<User> Login(string username, string password)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Username == username);

            if (user == null || !VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
            {
                return null;
            }

            user.LastLoginOn = DateTime.Now;
            await _dbContext.SaveChangesAsync();
            return user;
        }

        public string GenerateJwtToken(User user) 
        {
            var claims = new[] 
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Username), 
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()), 
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()) 
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SusalemKey"])); 
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken( 
                issuer: _configuration["Jwt:Issuer"], 
                audience: _configuration["Jwt:Audience"], 
                claims: claims, 
                expires: DateTime.Now.AddMinutes(15), // 令牌过期时间
                signingCredentials: creds); // 签名凭证

            return new JwtSecurityTokenHandler().WriteToken(token); 
        }

        private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt) 
        {
            using (var hmac = new HMACSHA512()) 
            {
                passwordSalt = hmac.Key; 
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password)); 
            }
        }

        private static bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt) 
        {
            using (var hmac = new HMACSHA512(storedSalt)) // 使用存储的盐值初始化HMACSHA512
            {
                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password)); // 对输入的密码进行哈希
                return computedHash.SequenceEqual(storedHash); // 比较哈希值是否相等
            }
        }

        public async Task<UserDto> GetUserById(int id)
        {
            var user = await _dbContext.Users
                .Include(u => u.UserRoles)
                .ThenInclude(ur => ur.Role)
                .FirstOrDefaultAsync(u => u.Id == id);

            if (user == null)
            {
                return null;
            }

            var userDto = new UserDto
            {
                Id = user.Id,
                Username = user.Username,
                Email = user.Email,
                Name = user.Name,
                Created = user.Created,
                LastLoginOn = user.LastLoginOn,
                UserRoles = user.UserRoles.Select(ur => new UserRoleDto
                {
                    RoleId = ur.RoleId,
                    RoleName = ur.Role.Name
                }).ToList()
            };
            return userDto;
        }


        public async Task<Role> CreateRole(string roleName, string description)
        {
            var role = new Role
            {
                Name = roleName,
                Description = description
            };

            _dbContext.Roles.Add(role);
            await _dbContext.SaveChangesAsync();

            return role;
        }

        public async Task AssignRoleToUser(int userId, int roleId)
        {
            var userRole = new UserRole
            {
                UserId = userId,
                RoleId = roleId
            };

            _dbContext.UserRoles.Add(userRole);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<UserRoleDto>> GetUserRoles(int userId)
        {
            var userRoles = await _dbContext.UserRoles
                .Where(ur => ur.UserId == userId)
                .Include(ur => ur.Role)
                .Select(ur => new UserRoleDto
                {
                    RoleId = ur.Role.Id,
                    RoleName = ur.Role.Name
                })
                .ToListAsync();

            return userRoles;
        }

        private async Task AssignDefaultRole(int userId)
        {
            // 查询普通用户角色的ID，这里假设普通用户角色的ID为 2，根据实际情况修改
            int defaultRoleId = 2; // 假设普通用户角色的ID为 2

            // 检查用户是否已经分配了这个角色
            var userRole = await _dbContext.UserRoles
                .FirstOrDefaultAsync(ur => ur.UserId == userId && ur.RoleId == defaultRoleId);

            if (userRole == null)
            {
                // 创建用户角色关联实体并添加到数据库
                userRole = new UserRole
                {
                    UserId = userId,
                    RoleId = defaultRoleId
                };

                _dbContext.UserRoles.Add(userRole);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}