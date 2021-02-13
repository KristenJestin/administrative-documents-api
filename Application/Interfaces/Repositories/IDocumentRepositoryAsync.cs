using Domain.Common;
using Domain.Entities;
using System.Threading.Tasks;

namespace Application.Interfaces.Repositories
{
    public interface IDocumentRepositoryAsync : IGenericRepositoryAsync<Document>
    {
        //Task<bool> IsUniqueNameFolderAsync(string barcode);
        Task<PaginatedList<Document>> GetPagedReponseAsync(int user, int pageNumber, int pageSize, string search = null, int? type = null, int? tag = null);

        Task<Document> FindByIdWithTypeAndTagsAndFileAsync(int id);
        Task<Document> FindByIdWithFileAsync(int id);
    }
}
