using Application.Interfaces.Repositories;
using AutoMapper;
using Domain.Entities;
using Domain.Settings;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Rijndael256;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Documents.Commands.CreateDocument
{
    public class CreateDocumentCommandHandler : IRequestHandler<CreateDocumentCommand, int>
    {
        private readonly IDocumentRepositoryAsync _documentRepository;
        private readonly IMapper _mapper;
        private readonly DocumentSettings _settings;
        public CreateDocumentCommandHandler(IDocumentRepositoryAsync documentRepository, IMapper mapper, IOptions<DocumentSettings> documentSettings)
        {
            _documentRepository = documentRepository;
            _mapper = mapper;
            _settings = documentSettings.Value;
        }

        public async Task<int> Handle(CreateDocumentCommand request, CancellationToken cancellationToken)
        {
            Document document = _mapper.Map<Document>(request);
            DocumentFile file = await BuildFromFileAsync(request.File);
            // TODO: move file to folder, encode, add in database
            await _documentRepository.AddAsync(document); // TODO: remove Save, create unitof work to add document and file in same sql request
            return document.Id;
        }

        #region privates
        private async Task<DocumentFile> BuildFromFileAsync(IFormFile file)
        {
            // copy file
            string resourcePath = Path.Combine("Resources", "Uploads");
            string destinationPath = Path.Combine(Directory.GetCurrentDirectory(), resourcePath);
            FileInfo fileInfo = new FileInfo(Path.Combine(destinationPath, GenerateUniqueFileName()));

            using (var stream = new FileStream(fileInfo.FullName, FileMode.Create))
                await file.CopyToAsync(stream);

            // TODO: encrypt file

            // create document file
            DocumentFile result = new DocumentFile
            {
                OriginalName = file.FileName,
                Path = fileInfo.Name,
                Encryption = "AES",
                Size = fileInfo.Length,
                MimeType = file.ContentType,
            };

            return result;
        }

        private string GenerateUniqueFileName()
            => $"{DateTime.Now.Ticks}-{Guid.NewGuid().ToString().AsSpan(0, 8).ToString()}";
        #endregion
    }
}
