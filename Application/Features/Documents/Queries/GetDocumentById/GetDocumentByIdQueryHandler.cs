using Application.Interfaces.Repositories;
using AutoWrapper.Wrappers;
using Domain.Entities;
using MediatR;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Documents.Queries.GetDocumentById
{
    public class GetDocumentByIdQueryHandler : IRequestHandler<GetDocumentByIdQuery, Document>
    {
        private readonly IDocumentRepositoryAsync _documentRepository;
        public GetDocumentByIdQueryHandler(IDocumentRepositoryAsync documentRepository)
        {
            _documentRepository = documentRepository;
        }

        public async Task<Document> Handle(GetDocumentByIdQuery query, CancellationToken cancellationToken)
        {
            Document document = await _documentRepository.FindByIdAsync(query.Id);
            if (document == null)
                throw new ApiProblemDetailsException($"Record with id: {query.Id} does not exist.", (int)HttpStatusCode.NotFound);
            return document;
        }
    }
}
