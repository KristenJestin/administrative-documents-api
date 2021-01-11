using Domain.Common;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Contexts
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext()
        {
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            // TODO: add authenticated user
        }

        public DbSet<DocumentType> DocumentTypes { get; set; }
        public DbSet<DocumentTag> DocumentTags { get; set; }
        public DbSet<DocumentFile> DocumentFiles { get; set; }
        public DbSet<Document> Documents { get; set; }
        public DbSet<Folder> Folder { get; set; }


        #region overrides
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (EntityEntry<DatedBaseEntity> entry in ChangeTracker.Entries<DatedBaseEntity>()
               .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified))
            {
                // on create
                if (entry.State.Equals(EntityState.Added))
                {
                    entry.Entity.CreatedAt = DateTime.UtcNow;
                    // TODO: update creater user
                }
                // on update
                else if (entry.State.Equals(EntityState.Modified))
                {
                    entry.Entity.UpdatedAt = DateTime.UtcNow;
                    // TODO : update updater user
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }
        #endregion
    }
}
