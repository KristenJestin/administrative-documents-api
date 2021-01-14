using Application.Interfaces.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Documents.Commands.CreateDocument
{
    public partial class CreateDocumentCommandHandler : IRequestHandler<CreateDocumentCommand, int>
    {
        private readonly IDocumentRepositoryAsync _documentRepository;
        private readonly IMapper _mapper;
        public CreateDocumentCommandHandler(IDocumentRepositoryAsync documentRepository, IMapper mapper)
        {
            _documentRepository = documentRepository;
            _mapper = mapper;
        }

        public async Task<int> Handle(CreateDocumentCommand request, CancellationToken cancellationToken)
        {
            Document document = _mapper.Map<Document>(request);
            await _documentRepository.AddAsync(document);
            return document.Id;
        }
    }
}
