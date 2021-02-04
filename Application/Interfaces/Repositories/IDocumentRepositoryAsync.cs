using Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Interfaces.Repositories
{
    public interface IDocumentRepositoryAsync : IGenericRepositoryAsync<Document>
    {
        //Task<bool> IsUniqueNameFolderAsync(string barcode);
        Task<IReadOnlyList<Document>> GetPagedReponseAsync(int pageNumber, int pageSize, string search);

        Task<Document> FindByIdWithTypeAndTagsAsync(int id);
        Task<Document> FindByIdWithFileAsync(int id);
    }
}
