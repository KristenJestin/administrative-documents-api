using Domain.Entities;
using MediatR;

namespace Application.Features.Documents.Queries.GetDocumentById
{
    public class GetDocumentByIdQuery : IRequest<Document>
    {
        public int Id { get; set; }
    }
}
