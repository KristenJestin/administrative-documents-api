using Application.DTOs.Document;
using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using AutoMapper;
using Domain.Entities;
using Domain.Settings;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System;
using System.Drawing;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Documents.Commands.CreateDocument
{
    public class CreateDocumentCommandHandler : IRequestHandler<CreateDocumentCommand, ReadDocumentResponse>
    {
        private readonly IDocumentRepositoryAsync _documentRepository;
        private readonly IDocumentTypeRepositoryAsync _documentTypeRepository;
        private readonly IMapper _mapper;
        private readonly ICryptographyService _cryptographyService;
        private readonly DocumentSettings _settings;
        public CreateDocumentCommandHandler(IDocumentRepositoryAsync documentRepository, IDocumentTypeRepositoryAsync documentTypeRepository, IMapper mapper, ICryptographyService cryptographyService, IOptions<DocumentSettings> documentSettings)
        {
            _documentRepository = documentRepository;
            _documentTypeRepository = documentTypeRepository;
            _mapper = mapper;
            _cryptographyService = cryptographyService;
            _settings = documentSettings.Value;
        }

        public async Task<ReadDocumentResponse> Handle(CreateDocumentCommand request, CancellationToken cancellationToken)
        {
            Document document = _mapper.Map<Document>(request);
            DocumentFile file = await BuildFromFileAsync(request.File);
            document.File = file;
            document.TypeId = request.Type;

            // insert in database
            await _documentRepository.AddAsync(document);

            // fill type
            if (document.TypeId != null)
                document.Type = await _documentTypeRepository.FindByIdAsync(document.TypeId.Value);

            return _mapper.Map<ReadDocumentResponse>(document);
        }

        #region privates
        private async Task<DocumentFile> BuildFromFileAsync(IFormFile file)
        {
            // detect image dimensions
            string dimensions = null;
            try
            {
                using (Stream fileStream = file.OpenReadStream())
                using (Image image = Image.FromStream(fileStream, false, true))
                    dimensions = $"{image.Width},{image.Height}";
            }
            catch { }

            // copy file
            string resourcePath = Path.Combine("Resources", "Uploads");
            string destinationPath = Path.Combine(Directory.GetCurrentDirectory(), resourcePath);
            FileInfo fileInfo = new FileInfo(Path.Combine(destinationPath, GenerateUniqueFileName()));

            // copy in resources folder
            using (var stream = new FileStream(fileInfo.FullName, FileMode.Create))
                await file.CopyToAsync(stream);

            // encrypt file
            string iv = Guid.NewGuid().ToString().AsSpan(0, 16).ToString();
            byte[] cipher = _cryptographyService.Encrypt(await File.ReadAllBytesAsync(fileInfo.FullName), Encoding.UTF8.GetBytes(_settings.EncryptKey), Encoding.UTF8.GetBytes(iv));
            await File.WriteAllBytesAsync(fileInfo.FullName, cipher);

            // create document file
            DocumentFile result = new DocumentFile
            {
                OriginalName = file.FileName,
                Path = fileInfo.Name,
                Encryption = _cryptographyService.Name,
                IV = iv,
                Size = fileInfo.Length,
                Dimensions = dimensions,
                MimeType = file.ContentType,
            };

            return result;
        }

        private string GenerateUniqueFileName()
            => $"{DateTime.Now.Ticks}-{Guid.NewGuid().ToString().AsSpan(0, 8).ToString()}";
        #endregion
    }
}
