using Application.DTOs.Document;
using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Application.Wrappers;
using AutoMapper;
using AutoWrapper.Wrappers;
using Domain.Entities;
using Domain.Settings;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Documents.Queries.DownloadDocumentById
{
    public class DownloadDocumentByIdQueryHandler : IRequestHandler<DownloadDocumentByIdQuery, DownloadDocumentResponse>
    {
        private readonly IDocumentRepositoryAsync _documentRepository;
        private readonly IAuthenticatedUserService _authenticatedUser;
        private readonly DocumentSettings _settings;
        private readonly ICryptographyService _cryptographyService;

        public DownloadDocumentByIdQueryHandler(IDocumentRepositoryAsync documentRepository, IAuthenticatedUserService authenticatedUser, IOptions<DocumentSettings> settings, ICryptographyService cryptographyService)
        {
            _documentRepository = documentRepository;
            _authenticatedUser = authenticatedUser;
            _settings = settings.Value;
            _cryptographyService = cryptographyService;
        }

        public async Task<DownloadDocumentResponse> Handle(DownloadDocumentByIdQuery query, CancellationToken cancellationToken)
        {
            Document document = await _documentRepository.FindByIdWithFileAsync(query.Id);

            if (document == null)
                throw new ApiProblemDetailsException($"Record with id: {query.Id} does not exist.", StatusCodes.Status404NotFound);
            if (document.CreatedBy != _authenticatedUser.UserId)
                throw new ApiProblemDetailsException($"You are not authorized to access this resource.", StatusCodes.Status403Forbidden);

            // data
            DocumentFile documentFile = document.File;
            FileInfo fileInfo = new FileInfo(_settings.BuildFilePath(documentFile.Path));
            // check if document is still valid
            if (documentFile == null || !fileInfo.Exists)
                throw new ApiProblemDetailsException($"Record with id: {query.Id} does contain file.", StatusCodes.Status404NotFound);

            // decrypt
            string iv = documentFile.IV;
            byte[] decrypted = _cryptographyService.Decrypt(await File.ReadAllBytesAsync(fileInfo.FullName), Encoding.UTF8.GetBytes(_settings.EncryptKey), Encoding.UTF8.GetBytes(iv));

            // response
            return new DownloadDocumentResponse
            {
                Id = query.Id,
                Name = documentFile.OriginalName,
                ContentType = documentFile.MimeType,
                FileContent = decrypted,
            };
        }
    }
}
