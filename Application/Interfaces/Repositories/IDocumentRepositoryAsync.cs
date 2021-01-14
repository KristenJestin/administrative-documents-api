using Domain.Entities;

namespace Application.Interfaces.Repositories
{
    public interface IDocumentRepositoryAsync : IGenericRepositoryAsync<Document>
    {
        //Task<bool> IsUniqueNameFolderAsync(string barcode);
    }
}
