using Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Interfaces.Repositories
{
    public interface IDocumentTagRepositoryAsync : IGenericRepositoryAsync<DocumentTag>
    {
        Task<IReadOnlyList<DocumentTag>> GetAllByUserAsync(int user);
        Task<DocumentTag> FindByIdAsync(int user, int id);
        Task<DocumentTag> FindBySlugAsync(int user, string slug);
        Task<IEnumerable<DocumentTag>> GetSameUniqueNameAsync(int user, IEnumerable<string> tags);
        void AttachRange(IEnumerable<DocumentTag> tags);
    }
}
