using Application.DTOs.Document;
using Application.DTOs.DocumentTag;
using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Application.Wrappers;
using AutoMapper;
using AutoWrapper.Wrappers;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.DocumentTags.Queries.GetDocumentTagBySlug
{
    public class GetDocumentTagBySlugQueryHandler : IRequestHandler<GetDocumentTagBySlugQuery, BasicApiResponse<ReadDocumentTagResponse>>
    {
        private readonly IDocumentTagRepositoryAsync _repository;
        private readonly IAuthenticatedUserService _authenticatedUser;
        private readonly IMapper _mapper;
        public GetDocumentTagBySlugQueryHandler(IDocumentTagRepositoryAsync repository, IAuthenticatedUserService authenticatedUser, IMapper mapper)
        {
            _repository = repository;
            _authenticatedUser = authenticatedUser;
            _mapper = mapper;
        }

        public async Task<BasicApiResponse<ReadDocumentTagResponse>> Handle(GetDocumentTagBySlugQuery query, CancellationToken cancellationToken)
        {
            DocumentTag tag = await _repository.FindBySlugAsync(_authenticatedUser.UserId, query.Slug);

            if (tag == null)
                throw new ApiProblemDetailsException($"Record with slug: {query.Slug} does not exist.", StatusCodes.Status404NotFound);

            ReadDocumentTagResponse dto = _mapper.Map<ReadDocumentTagResponse>(tag);
            return new BasicApiResponse<ReadDocumentTagResponse>(dto);
        }
    }
}
