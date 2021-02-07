using Application.DTOs.DocumentTag;
using Application.Wrappers;
using MediatR;

namespace Application.Features.DocumentTags.Queries.GetDocumentTagById
{
    public class GetDocumentTagByIdQuery : IRequest<BasicApiResponse<ReadDocumentTagResponse>>
    {
        public int Id { get; set; }
    }
}
