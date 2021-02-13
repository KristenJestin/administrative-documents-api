using Application.DTOs.Document;
using Application.DTOs.DocumentType;
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

namespace Application.Features.DocumentTypes.Queries.GetDocumentTypeById
{
    public class GetDocumentTypeByIdQueryHandler : IRequestHandler<GetDocumentTypeByIdQuery, BasicApiResponse<ReadDocumentTypeResponse>>
    {
        private readonly IDocumentTypeRepositoryAsync _repository;
        private readonly IMapper _mapper;
        public GetDocumentTypeByIdQueryHandler(IDocumentTypeRepositoryAsync repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<BasicApiResponse<ReadDocumentTypeResponse>> Handle(GetDocumentTypeByIdQuery query, CancellationToken cancellationToken)
        {
            DocumentType type = await _repository.FindByIdAsync(query.Id);

            if (type == null)
                throw new ApiProblemDetailsException($"Record with id: {query.Id} does not exist.", StatusCodes.Status404NotFound);

            ReadDocumentTypeResponse dto = _mapper.Map<ReadDocumentTypeResponse>(type);
            return new BasicApiResponse<ReadDocumentTypeResponse>(dto);
        }
    }
}
