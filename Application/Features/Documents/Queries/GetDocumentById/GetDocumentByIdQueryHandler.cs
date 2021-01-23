using Application.DTOs.Document;
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

namespace Application.Features.Documents.Queries.GetDocumentById
{
    public class GetDocumentByIdQueryHandler : IRequestHandler<GetDocumentByIdQuery, BasicApiResponse<ReadDocumentResponse>>
    {
        private readonly IDocumentRepositoryAsync _documentRepository;
        private readonly IAuthenticatedUserService _authenticatedUser;
        private readonly IMapper _mapper;
        public GetDocumentByIdQueryHandler(IDocumentRepositoryAsync documentRepository, IAuthenticatedUserService authenticatedUser, IMapper mapper)
        {
            _documentRepository = documentRepository;
            _authenticatedUser = authenticatedUser;
            _mapper = mapper;
        }

        public async Task<BasicApiResponse<ReadDocumentResponse>> Handle(GetDocumentByIdQuery query, CancellationToken cancellationToken)
        {
            Document document = await _documentRepository.FindByIdWithTypeAndTagsAsync(query.Id);

            if (document == null)
                throw new ApiProblemDetailsException($"Record with id: {query.Id} does not exist.", StatusCodes.Status404NotFound);
            if(document.CreatedBy != _authenticatedUser.UserId)
                throw new ApiProblemDetailsException($"You are not authorized to access this resource.", StatusCodes.Status403Forbidden);

            ReadDocumentResponse dto = _mapper.Map<ReadDocumentResponse>(document);
            return new BasicApiResponse<ReadDocumentResponse>(dto);
        }
    }
}
