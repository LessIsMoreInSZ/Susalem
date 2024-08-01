using Microsoft.EntityFrameworkCore;
using susalem.vue.Models;

/*
 *      作用：
 *      实体类型映射
 *      数据库连接配置
 *      CRUD
 *      迁移管理
 */
namespace susalem.vue.Data
{
    public class UserDbContext : DbContext
    {
        public UserDbContext(DbContextOptions<UserDbContext> options) : base(options)
        {

        }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure User entity
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(u => u.Id);

                entity.Property(u => u.Username)
                      .IsRequired()
                      .HasMaxLength(50);
                entity.HasIndex(u => u.Username)
                      .IsUnique(); // 配置唯一约束

                entity.Property(u => u.Email)
                      .HasMaxLength(100);

                entity.Property(u => u.Created)
                      .HasDefaultValueSql("GETUTCDATE()");

                entity.HasMany(u => u.UserRoles)
                      .WithOne(ur => ur.User)
                      .HasForeignKey(ur => ur.UserId);
            });

            // Configure Role entity
            modelBuilder.Entity<Role>(entity =>
            {
                entity.HasKey(r => r.Id);

                entity.Property(r => r.Name)
                      .IsRequired()
                      .HasMaxLength(50);

                entity.HasMany(r => r.UserRoles)
                      .WithOne(ur => ur.Role)
                      .HasForeignKey(ur => ur.RoleId);
            });

            // Configure UserRole entity
            modelBuilder.Entity<UserRole>(entity =>
            {
                entity.HasKey(ur => new { ur.UserId, ur.RoleId });

                entity.HasOne(ur => ur.User)
                      .WithMany(u => u.UserRoles)
                      .HasForeignKey(ur => ur.UserId);

                entity.HasOne(ur => ur.Role)
                      .WithMany(r => r.UserRoles)
                      .HasForeignKey(ur => ur.RoleId);
            });
        }
    }
}
