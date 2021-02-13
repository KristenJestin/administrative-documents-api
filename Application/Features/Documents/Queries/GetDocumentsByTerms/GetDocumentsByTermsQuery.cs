using Application.DTOs.Document;
using Application.Parameters;
using Application.Wrappers;
using MediatR;
using System.Collections.Generic;

namespace Application.Features.Documents.Queries.GetDocumentsByTerms
{
    public class GetDocumentsByTermsQuery : PagedRequestQuery, IRequest<PagedApiResponse<IEnumerable<ReadDocumentListResponse>>>
    {
        public string Term { get; set; }
        public int? Type { get; set; }
        public string Tag { get; set; }
    }
}
