using Domain.Entities;
using System.Threading.Tasks;

namespace Application.Interfaces.Repositories
{
    public interface IDocumentRepositoryAsync : IGenericRepositoryAsync<Document>
    {
        //Task<bool> IsUniqueNameFolderAsync(string barcode);
        Task<Document> FindByIdWithTypeAndTagsAsync(int id);
    }
}
