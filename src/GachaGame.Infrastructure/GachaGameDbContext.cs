using GachaGame.Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;
using System.Security.Claims;

namespace GachaGame.Infrastructure
{
    public class GachaGameDbContext : DbContext
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public GachaGameDbContext(DbContextOptions<GachaGameDbContext> options, IHttpContextAccessor httpContextAccessor)
            : base(options)
        {
            _httpContextAccessor = httpContextAccessor;

        }

        public DbSet<User> Users { get; set; }
        public DbSet<GachaRecord> GachaRecords { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

        }
        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            Audit();
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }
        private void Audit()
        {
            var identity = _httpContextAccessor.HttpContext?.User.Identity as ClaimsIdentity;
            var userName = identity?.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Name)?.Value ?? "Anonymous";
            var entries = ChangeTracker.Entries().Where(e => e.Entity is Entity && e.State is EntityState.Added or EntityState.Modified);
            foreach (var entry in entries)
            {
                var entity = (Entity)entry.Entity;
                if (entry.State == EntityState.Added)
                {
                    //未设置创建时间或创建人，则自动设置当前时间或当前用户
                    if (entity.CreatedAt == default) entity.CreatedAt = DateTime.UtcNow;
                    if (string.IsNullOrWhiteSpace(entity.CreatedBy)) entity.CreatedBy = userName;
                }
                else
                {
                    entity.UpdatedAt = DateTime.UtcNow;
                    entity.UpdatedBy = userName;
                }
            }
        }
    }
}
