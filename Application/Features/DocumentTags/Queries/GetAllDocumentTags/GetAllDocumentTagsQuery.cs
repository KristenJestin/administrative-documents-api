using Application.DTOs.DocumentTag;
using Application.Wrappers;
using MediatR;
using System.Collections.Generic;

namespace Application.Features.DocumentTags.Queries.GetAllDocumentTags
{
    public class GetAllDocumentTagsQuery : IRequest<BasicApiResponse<IEnumerable<ReadDocumentTagListResponse>>> { }
}
