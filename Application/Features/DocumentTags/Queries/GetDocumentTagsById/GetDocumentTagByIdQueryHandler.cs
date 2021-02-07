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

namespace Application.Features.DocumentTags.Queries.GetDocumentTagById
{
    public class GetDocumentTagByIdQueryHandler : IRequestHandler<GetDocumentTagByIdQuery, BasicApiResponse<ReadDocumentTagResponse>>
    {
        private readonly IDocumentTagRepositoryAsync _repository;
        private readonly IAuthenticatedUserService _authenticatedUser;
        private readonly IMapper _mapper;
        public GetDocumentTagByIdQueryHandler(IDocumentTagRepositoryAsync repository, IAuthenticatedUserService authenticatedUser, IMapper mapper)
        {
            _repository = repository;
            _authenticatedUser = authenticatedUser;
            _mapper = mapper;
        }

        public async Task<BasicApiResponse<ReadDocumentTagResponse>> Handle(GetDocumentTagByIdQuery query, CancellationToken cancellationToken)
        {
            DocumentTag tag = await _repository.FindByIdAsync(_authenticatedUser.UserId, query.Id);

            if (tag == null)
                throw new ApiProblemDetailsException($"Record with id: {query.Id} does not exist.", StatusCodes.Status404NotFound);

            ReadDocumentTagResponse dto = _mapper.Map<ReadDocumentTagResponse>(tag);
            return new BasicApiResponse<ReadDocumentTagResponse>(dto);
        }
    }
}
