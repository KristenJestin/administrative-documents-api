using Application.DTOs.DocumentType;
using Application.Wrappers;
using MediatR;

namespace Application.Features.DocumentTypes.Queries.GetDocumentTypeById
{
    public class GetDocumentTypeByIdQuery : IRequest<BasicApiResponse<ReadDocumentTypeResponse>>
    {
        public int Id { get; set; }
    }
}
