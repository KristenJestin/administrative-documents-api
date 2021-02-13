using Application.Interfaces.Repositories;
using Domain.Entities;
using Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Common.Extensions;
using Domain.Common;

namespace Infrastructure.Persistence.Repositories
{
    public class DocumentRepositoryAsync : GenericRepositoryAsync<Document>, IDocumentRepositoryAsync
    {
        private readonly DbSet<Document> _documents;

        public DocumentRepositoryAsync(ApplicationDbContext dbContext) : base(dbContext)
        {
            _documents = dbContext.Set<Document>();
        }

        public async Task<PaginatedList<Document>> GetPagedReponseAsync(int user, int pageNumber, int pageSize, string search = null, int? type = null, int? tag = null)
        {
            IQueryable<Document> query = _documents.Where(d => d.CreatedBy == user);

            #region parameters
            if (!string.IsNullOrWhiteSpace(search))
                query = query.Where(d => d.Name.Contains(search));

            if (type != null)
                query = query.Where(d => d.TypeId == type);

            if (tag != null)
                query = query
                    .Where(d => d.Tags.Any(t => t.Id == tag.Value));
            #endregion

            // total
            int count = await query.CountAsync();
            // paginations
            query = query
                .Paginate(pageNumber, pageSize)
                .Include(d => d.Type)
                .Include(d => d.Tags)
                .OrderByDescending(d => d.Date)
                .AsNoTracking();

            // list documents
            return new PaginatedList<Document>(await query.ToListAsync(), count, pageNumber, pageSize);
        }

        public async Task<Document> FindByIdWithTypeAndTagsAndFileAsync(int id)
            => await _documents
                .Include(d => d.Type)
                .Include(d => d.Tags)
                .Include(d => d.File)
                .FirstOrDefaultAsync(d => d.Id == id);

        public async Task<Document> FindByIdWithFileAsync(int id)
            => await _documents
                .Include(d => d.File)
                .FirstOrDefaultAsync(d => d.Id == id);

        //public Task<bool> IsUniqueBarcodeAsync(string barcode)
        //{
        //    return _products
        //        .AllAsync(p => p.Barcode != barcode);
        //}
    }
}
