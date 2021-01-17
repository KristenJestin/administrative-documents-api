using Application.Interfaces.Repositories;
using Domain.Entities;
using Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Repositories
{
    public class DocumentRepositoryAsync : GenericRepositoryAsync<Document>, IDocumentRepositoryAsync
    {
        private readonly DbSet<Document> _documents;

        public DocumentRepositoryAsync(ApplicationDbContext dbContext) : base(dbContext)
        {
            _documents = dbContext.Set<Document>();
        }


        public async Task<Document> FindByIdWithTypeAsync(int id)
            => await _documents
                .Include(d => d.Type)
                .FirstOrDefaultAsync(d => d.Id == id);

        //public Task<bool> IsUniqueBarcodeAsync(string barcode)
        //{
        //    return _products
        //        .AllAsync(p => p.Barcode != barcode);
        //}
    }
}
