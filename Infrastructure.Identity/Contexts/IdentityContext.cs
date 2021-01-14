using Infrastructure.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Identity.Contexts
{
    public class IdentityContext : IdentityDbContext<ApplicationUser, ApplicationRole, int>
    {
        public IdentityContext(DbContextOptions<IdentityContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<ApplicationUser>(entity =>
            {
                entity.Property(m => m.Id).HasMaxLength(85);
                entity.Property(m => m.NormalizedEmail).HasMaxLength(85);
                entity.Property(m => m.NormalizedUserName).HasMaxLength(85);
                entity.ToTable(name: "User");
            });

            builder.Entity<ApplicationRole>(entity =>
            {
                entity.Property(m => m.Id).HasMaxLength(85);
                entity.Property(m => m.NormalizedName).HasMaxLength(85);
                entity.ToTable(name: "Role");
            });
            builder.Entity<IdentityUserRole<int>>(entity =>
            {
                entity.Property(m => m.UserId).HasMaxLength(85);
                entity.Property(m => m.RoleId).HasMaxLength(85);
                entity.ToTable("UserRoles");
            });

            builder.Entity<IdentityUserClaim<int>>(entity =>
            {
                entity.Property(m => m.Id).HasMaxLength(85);
                entity.Property(m => m.UserId).HasMaxLength(85);
                entity.ToTable("UserClaims");
            });

            builder.Entity<IdentityUserLogin<int>>(entity =>
            {
                entity.Property(m => m.LoginProvider).HasMaxLength(85);
                entity.Property(m => m.ProviderKey).HasMaxLength(85);
                entity.Property(m => m.UserId).HasMaxLength(85);
                entity.ToTable("UserLogins");
            });

            builder.Entity<IdentityRoleClaim<int>>(entity =>
            {
                entity.Property(m => m.Id).HasMaxLength(85);
                entity.Property(m => m.RoleId).HasMaxLength(85);
                entity.ToTable("RoleClaims");
            });

            builder.Entity<IdentityUserToken<int>>(entity =>
            {
                entity.Property(m => m.UserId).HasMaxLength(85);
                entity.Property(m => m.LoginProvider).HasMaxLength(85);
                entity.Property(m => m.Name).HasMaxLength(85);
                entity.ToTable("UserTokens");
            });
        }
    }
}
