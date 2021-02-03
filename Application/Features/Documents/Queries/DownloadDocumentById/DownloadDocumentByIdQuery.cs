using Application.DTOs.Document;
using MediatR;

namespace Application.Features.Documents.Queries.DownloadDocumentById
{
    public class DownloadDocumentByIdQuery : IRequest<DownloadDocumentResponse>
    {
        public int Id { get; set; }
    }
}
