using Application.DTOs.Document;
using Application.Wrappers;
using MediatR;
using System.Collections.Generic;

namespace Application.Features.Documents.Queries.GetDocumentsByTerms
{
    public class GetDocumentsByTermsQuery : IRequest<PagedApiResponse<IEnumerable<ReadDocumentListResponse>>>
    {
        public int Page { get; set; }
        public int PageSize { get; set; }

        public string Term { get; set; }
        public int? Type { get; set; }
        public string Tag { get; set; }
    }
}
