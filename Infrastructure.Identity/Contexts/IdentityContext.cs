using Infrastructure.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Identity.Contexts
{
    public class IdentityContext : IdentityDbContext<ApplicationUser>
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

            builder.Entity<IdentityRole>(entity =>
            {
                entity.Property(m => m.Id).HasMaxLength(85);
                entity.Property(m => m.NormalizedName).HasMaxLength(85);
                entity.ToTable(name: "Role");
            });
            builder.Entity<IdentityUserRole<string>>(entity =>
            {
                entity.Property(m => m.UserId).HasMaxLength(85);
                entity.Property(m => m.RoleId).HasMaxLength(85);
                entity.ToTable("UserRoles");
            });

            builder.Entity<IdentityUserClaim<string>>(entity =>
            {
                entity.Property(m => m.Id).HasMaxLength(85);
                entity.Property(m => m.UserId).HasMaxLength(85);
                entity.ToTable("UserClaims");
            });

            builder.Entity<IdentityUserLogin<string>>(entity =>
            {
                entity.Property(m => m.LoginProvider).HasMaxLength(85);
                entity.Property(m => m.ProviderKey).HasMaxLength(85);
                entity.Property(m => m.UserId).HasMaxLength(85);
                entity.ToTable("UserLogins");
            });

            builder.Entity<IdentityRoleClaim<string>>(entity =>
            {
                entity.Property(m => m.Id).HasMaxLength(85);
                entity.Property(m => m.RoleId).HasMaxLength(85);
                entity.ToTable("RoleClaims");
            });

            builder.Entity<IdentityUserToken<string>>(entity =>
            {
                entity.Property(m => m.UserId).HasMaxLength(85);
                entity.Property(m => m.LoginProvider).HasMaxLength(85);
                entity.Property(m => m.Name).HasMaxLength(85);
                entity.ToTable("UserTokens");
            });
        }
    }
}
