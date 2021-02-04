﻿using Application.Interfaces.Repositories;
using Domain.Entities;
using Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
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

        public override async Task<IReadOnlyList<Document>> GetPagedReponseAsync(int pageNumber, int pageSize)
            // TODO : add check of the creator
            => await _documents
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .OrderByDescending(d => d.CreatedAt)
                .Include(d => d.Type)
                .Include(d => d.Tags)
                .AsNoTracking()
                .ToListAsync();

        public virtual async Task<IReadOnlyList<Document>> GetPagedReponseAsync(int pageNumber, int pageSize, string search)
            // TODO : add check of the creator
            // TODO : search in note with ranking
            => await _documents
                .Where(d => d.Name.Contains(search))
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .AsNoTracking()
                .ToListAsync();

        public async Task<Document> FindByIdWithTypeAndTagsAsync(int id)
            => await _documents
                .Include(d => d.Type)
                .Include(d => d.Tags)
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
