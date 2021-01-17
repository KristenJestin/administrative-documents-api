using Application.Interfaces.Repositories;
using Domain.Entities;
using Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Repositories
{
    public class DocumentTypeRepositoryAsync : GenericRepositoryAsync<DocumentType>, IDocumentTypeRepositoryAsync
    {
        private readonly DbSet<DocumentType> _types;

        public DocumentTypeRepositoryAsync(ApplicationDbContext dbContext) : base(dbContext)
        {
            _types = dbContext.Set<DocumentType>();
        }

        public async Task<bool> ExistAsync(int id)
            => (await FindByIdAsync(id)) != null;
    }
}
