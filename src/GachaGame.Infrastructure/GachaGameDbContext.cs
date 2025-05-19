using GachaGame.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace GachaGame.Infrastructure
{
    public class GachaGameDbContext : DbContext
    {
        public GachaGameDbContext(DbContextOptions<GachaGameDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<GachaRecord> GachaRecords { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // 配置 User 和 GachaRecord 的关系
            modelBuilder.Entity<User>()
                .HasMany(u => u.GachaRecords)
                .WithOne(gr => gr.User)
                .HasForeignKey(gr => gr.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // 其他属性配置可根据需要补充
        }
    }
}
