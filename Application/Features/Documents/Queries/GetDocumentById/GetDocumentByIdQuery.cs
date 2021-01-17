using Application.DTOs.Document;
using MediatR;

namespace Application.Features.Documents.Queries.GetDocumentById
{
    public class GetDocumentByIdQuery : IRequest<ReadDocumentResponse>
    {
        public int Id { get; set; }
    }
}
