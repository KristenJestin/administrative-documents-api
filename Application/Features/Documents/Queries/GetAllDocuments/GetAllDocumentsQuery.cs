using Application.DTOs.Document;
using Application.Wrappers;
using MediatR;
using System.Collections.Generic;

namespace Application.Features.Documents.Queries.GetAllDocuments
{
    public class GetAllDocumentsQuery : IRequest<PagedApiResponse<IEnumerable<ReadDocumentListResponse>>>
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
    }
}
