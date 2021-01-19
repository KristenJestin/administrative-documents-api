using Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Interfaces.Repositories
{
    public interface IDocumentTagRepositoryAsync : IGenericRepositoryAsync<DocumentTag>
    {
        Task<IEnumerable<DocumentTag>> GetSameUniqueNameAsync(IEnumerable<string> tags, int user);
        void AttachRange(IEnumerable<DocumentTag> tags);
    }
}
