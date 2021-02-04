﻿using Application.DTOs.Document;
using Application.Features.Documents.Queries.GetAllDocuments;
using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Application.Parameters;
using Application.Wrappers;
using AutoMapper;
using Domain.Common;
using Domain.Entities;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Documents.Queries.GetDocumentsByTerms
{
    public class GetDocumentsByTermsQueryHandler : IRequestHandler<GetDocumentsByTermsQuery, PagedApiResponse<IEnumerable<ReadDocumentListResponse>>>
    {
        private readonly IAuthenticatedUserService _authenticatedUser;
        private readonly IDocumentRepositoryAsync _repository;
        private readonly IMapper _mapper;

        public GetDocumentsByTermsQueryHandler(IAuthenticatedUserService authenticatedUser, IDocumentRepositoryAsync documentRepository, IMapper mapper)
        {
            _authenticatedUser = authenticatedUser;
            _repository = documentRepository;
            _mapper = mapper;
        }

        public async Task<PagedApiResponse<IEnumerable<ReadDocumentListResponse>>> Handle(GetDocumentsByTermsQuery request, CancellationToken cancellationToken)
        {
            request.ValidateValues();

            PaginatedList<Document> paginatedList = await _repository.GetPagedReponseAsync(_authenticatedUser.UserId, request.Page, request.PageSize, request.Term);
            IEnumerable<ReadDocumentListResponse> dto = _mapper.Map<IEnumerable<ReadDocumentListResponse>>(paginatedList.Items);

            ApiResponsePagination pagination = ApiResponsePagination.Build(paginatedList);
            return new PagedApiResponse<IEnumerable<ReadDocumentListResponse>>(dto, pagination);
        }
    }
}
