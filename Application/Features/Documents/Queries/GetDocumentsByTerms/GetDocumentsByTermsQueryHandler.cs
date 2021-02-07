using Application.DTOs.Document;
using Application.Features.Documents.Queries.GetAllDocuments;
using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Application.Parameters;
using Application.Wrappers;
using AutoMapper;
using AutoWrapper.Wrappers;
using Domain.Common;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Documents.Queries.GetDocumentsByTerms
{
    public class GetDocumentsByTermsQueryHandler : IRequestHandler<GetDocumentsByTermsQuery, PagedApiResponse<IEnumerable<ReadDocumentListResponse>>>
    {
        private readonly IAuthenticatedUserService _authenticatedUser;
        private readonly IDocumentRepositoryAsync _repository;
        private readonly IDocumentTypeRepositoryAsync _documentTypeRepository;
        private readonly IDocumentTagRepositoryAsync _documentTagRepository;
        private readonly IMapper _mapper;

        public GetDocumentsByTermsQueryHandler(IAuthenticatedUserService authenticatedUser, IDocumentRepositoryAsync documentRepository, IDocumentTypeRepositoryAsync documentTypeRepository, IMapper mapper, IDocumentTagRepositoryAsync documentTagRepository)
        {
            _authenticatedUser = authenticatedUser;
            _repository = documentRepository;
            _mapper = mapper;
            _documentTypeRepository = documentTypeRepository;
            _documentTagRepository = documentTagRepository;
        }

        public async Task<PagedApiResponse<IEnumerable<ReadDocumentListResponse>>> Handle(GetDocumentsByTermsQuery request, CancellationToken cancellationToken)
        {
            // validate page
            request.ValidateValues();

            // check if type exist
            if (request.Type != null && await _documentTypeRepository.FindByIdAsync(request.Type.Value) == null)
                throw new ApiProblemDetailsException($"Type with id: {request.Type} does not exist.", StatusCodes.Status404NotFound);

            // check if tag exist
            DocumentTag tag = null;
            if (!string.IsNullOrWhiteSpace(request.Tag))
            {
                tag = await _documentTagRepository.FindBySlugAsync(_authenticatedUser.UserId, request.Tag);
                if (tag == null)
                    throw new ApiProblemDetailsException($"Tag with name: {request.Tag} does not exist.", StatusCodes.Status404NotFound);
            }

            // make request
            PaginatedList<Document> paginatedList = await _repository.GetPagedReponseAsync(_authenticatedUser.UserId, request.Page, request.PageSize, request.Term, request.Type, tag?.Id);
            IEnumerable<ReadDocumentListResponse> dto = _mapper.Map<IEnumerable<ReadDocumentListResponse>>(paginatedList.Items);

            ApiResponsePagination pagination = ApiResponsePagination.Build(paginatedList);
            return new PagedApiResponse<IEnumerable<ReadDocumentListResponse>>(dto, pagination);
        }
    }
}
