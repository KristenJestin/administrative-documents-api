using Application.DTOs.Document;
using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using AutoMapper;
using AutoWrapper.Wrappers;
using Domain.Entities;
using MediatR;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Documents.Queries.GetDocumentById
{
    public class GetDocumentByIdQueryHandler : IRequestHandler<GetDocumentByIdQuery, ReadDocumentResponse>
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

        public async Task<ReadDocumentResponse> Handle(GetDocumentByIdQuery query, CancellationToken cancellationToken)
        {
            Document document = await _documentRepository.FindByIdWithTypeAsync(query.Id);

            if (document == null)
                throw new ApiProblemDetailsException($"Record with id: {query.Id} does not exist.", (int)HttpStatusCode.NotFound);
            if(document.CreatedBy != _authenticatedUser.UserId)
                throw new ApiProblemDetailsException($"You are not authorized to access this resource.", (int)HttpStatusCode.Forbidden);

            return _mapper.Map<ReadDocumentResponse>(document);
        }
    }
}
