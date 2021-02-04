using Application.DTOs.Document;
using Application.Interfaces.Repositories;
using Application.Wrappers;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Documents.Queries.GetDocumentsByTerms
{
    public class GetDocumentsByTermsQueryHandler : IRequestHandler<GetDocumentsByTermsQuery, PagedApiResponse<IEnumerable<ReadDocumentListResponse>>>
    {
        private readonly IDocumentRepositoryAsync _repository;
        private readonly IMapper _mapper;
        public GetDocumentsByTermsQueryHandler(IDocumentRepositoryAsync documentRepository, IMapper mapper)
        {
            _repository = documentRepository;
            _mapper = mapper;
        }

        public async Task<PagedApiResponse<IEnumerable<ReadDocumentListResponse>>> Handle(GetDocumentsByTermsQuery request, CancellationToken cancellationToken)
        {
            GetDocumentsByTermsParameter validFilter = _mapper.Map<GetDocumentsByTermsParameter>(request);
            IReadOnlyList<Document> documents = await _repository.GetPagedReponseAsync(validFilter.Page, validFilter.PageSize, validFilter.Term);
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
