using Application.DTOs.Document;
using Application.Wrappers;
using MediatR;

namespace Application.Features.Documents.Queries.GetDocumentById
{
    public class GetDocumentByIdQuery : IRequest<BasicApiResponse<ReadDocumentResponse>>
    {
        public int Id { get; set; }
    }
}
