using Application.DTOs.Document;
using Application.Interfaces.Repositories;
using Application.Wrappers;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Documents.Queries.GetAllDocuments
{
    public class GetAllDocumentsQueryHandler : IRequestHandler<GetAllDocumentsQuery, PagedApiResponse<IEnumerable<ReadDocumentListResponse>>>
    {
        private readonly IDocumentRepositoryAsync _documentRepository;
        private readonly IMapper _mapper;
        public GetAllDocumentsQueryHandler(IDocumentRepositoryAsync documentRepository, IMapper mapper)
        {
            _documentRepository = documentRepository;
            _mapper = mapper;
        }

        public async Task<PagedApiResponse<IEnumerable<ReadDocumentListResponse>>> Handle(GetAllDocumentsQuery request, CancellationToken cancellationToken)
        {
            GetAllDocumentsParameter validFilter = _mapper.Map<GetAllDocumentsParameter>(request);
            IReadOnlyList<Document> documents = await _documentRepository.GetPagedReponseAsync(validFilter.Page, validFilter.PageSize);
            IEnumerable<ReadDocumentListResponse> dto = _mapper.Map<IEnumerable<ReadDocumentListResponse>>(documents);

            ApiResponsePagination pagination = new ApiResponsePagination
            {
                Page = validFilter.Page,
                PageSize = validFilter.PageSize
            };
            return new PagedApiResponse<IEnumerable<ReadDocumentListResponse>>(dto, pagination);
        }
    }
}
