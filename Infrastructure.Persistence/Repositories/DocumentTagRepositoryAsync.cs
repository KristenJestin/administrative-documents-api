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

        public async Task<IEnumerable<DocumentTag>> GetSameUniqueNameAsync(IEnumerable<string> tags, int user)
            => await _tags
                .Where(t => tags.Contains(t.Slug) && t.CreatedBy == user)
                .AsNoTracking()
                .ToListAsync();

        public void AttachRange(IEnumerable<DocumentTag> tags)
            => _tags.AttachRange(tags);
    }
}
