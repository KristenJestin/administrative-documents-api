using Application.DTOs.DocumentTag;
using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Application.Wrappers;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.DocumentTags.Queries.GetAllDocumentTags
{
    public class GetAllDocumentTagsQueryHandler : IRequestHandler<GetAllDocumentTagsQuery, BasicApiResponse<IEnumerable<ReadDocumentTagListResponse>>>
    {
        private readonly IDocumentTagRepositoryAsync _repository;
        private readonly IAuthenticatedUserService _authenticatedUser;
        private readonly IMapper _mapper;
        public GetAllDocumentTagsQueryHandler(IDocumentTagRepositoryAsync repository, IAuthenticatedUserService authenticatedUser, IMapper mapper)
        {
            _mapper = mapper;
            _authenticatedUser = authenticatedUser;
            _repository = repository;
        }

        public async Task<BasicApiResponse<IEnumerable<ReadDocumentTagListResponse>>> Handle(GetAllDocumentTagsQuery request, CancellationToken cancellationToken)
        {
            IReadOnlyList<DocumentTag> tags = await _repository.GetAllByUserAsync(_authenticatedUser.UserId);
            IEnumerable<ReadDocumentTagListResponse> dto = _mapper.Map<IEnumerable<ReadDocumentTagListResponse>>(tags);

            return new BasicApiResponse<IEnumerable<ReadDocumentTagListResponse>>(dto);
        }
    }
}
