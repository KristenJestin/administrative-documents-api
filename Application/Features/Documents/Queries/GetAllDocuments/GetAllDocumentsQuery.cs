using Application.DTOs.Document;
using Application.Parameters;
using Application.Wrappers;
using MediatR;
using System.Collections.Generic;

namespace Application.Features.Documents.Queries.GetAllDocuments
{
    public class GetAllDocumentsQuery : PagedRequestQuery, IRequest<PagedApiResponse<IEnumerable<ReadDocumentListResponse>>> { }
}
