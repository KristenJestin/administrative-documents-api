using Application.Interfaces.Repositories;
using Domain.Entities;
using Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Repositories
{
    public class DocumentTagRepositoryAsync : GenericRepositoryAsync<DocumentTag>, IDocumentTagRepositoryAsync
    {
        private readonly DbSet<DocumentTag> _tags;

        public DocumentTagRepositoryAsync(ApplicationDbContext dbContext) : base(dbContext)
        {
            _tags = dbContext.Set<DocumentTag>();
        }


        public async Task<DocumentTag> FindBySlugAsync(int user, string slug)
            => await _tags
                .Where(t => t.CreatedBy == user && t.Slug == slug)
                .AsNoTracking()
                .FirstOrDefaultAsync();

        public async Task<IEnumerable<DocumentTag>> GetSameUniqueNameAsync(int user, IEnumerable<string> tags)
            => await _tags
                .Where(t => tags.Contains(t.Slug) && t.CreatedBy == user)
                .AsNoTracking()
                .ToListAsync();

        public void AttachRange(IEnumerable<DocumentTag> tags)
            => _tags.AttachRange(tags);
    }
}
