using Domain.Entities;
using System.Threading.Tasks;

namespace Application.Interfaces.Repositories
{
    public interface IDocumentTypeRepositoryAsync : IGenericRepositoryAsync<DocumentType>
    {
        Task<bool> ExistAsync(int id);
    }
}
