using Application.DTOs.DocumentTag;
using Application.Wrappers;
using MediatR;

namespace Application.Features.DocumentTags.Queries.GetDocumentTagBySlug
{
    public class GetDocumentTagBySlugQuery : IRequest<BasicApiResponse<ReadDocumentTagResponse>>
    {
        public string Slug { get; set; }
    }
}
