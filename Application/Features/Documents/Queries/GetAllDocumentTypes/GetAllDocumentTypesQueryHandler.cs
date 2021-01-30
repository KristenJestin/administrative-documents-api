using Application.DTOs.Document;
using Application.DTOs.DocumentType;
using Application.Interfaces.Repositories;
using Application.Wrappers;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Documents.Queries.GetAllDocumentTypes
{
    public class GetAllDocumentTypesQueryHandler : IRequestHandler<GetAllDocumentTypesQuery, BasicApiResponse<IEnumerable<ReadDocumentTypeListResponse>>>
    {
        private readonly IDocumentTypeRepositoryAsync _repository;
        private readonly IMapper _mapper;
        public GetAllDocumentTypesQueryHandler(IDocumentTypeRepositoryAsync repository, IMapper mapper)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<BasicApiResponse<IEnumerable<ReadDocumentTypeListResponse>>> Handle(GetAllDocumentTypesQuery request, CancellationToken cancellationToken)
        {
            IReadOnlyList<DocumentType> types = await _repository.GetAllAsync();
            IEnumerable<ReadDocumentTypeListResponse> dto = _mapper.Map<IEnumerable<ReadDocumentTypeListResponse>>(types);

            return new BasicApiResponse<IEnumerable<ReadDocumentTypeListResponse>>(dto);
        }
    }
}
