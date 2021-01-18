using Application.DTOs.Document;
using Application.DTOs.DocumentType;
using Application.Features.Documents.Commands.CreateDocument;
using AutoMapper;
using Domain.Entities;

namespace Application.Mappings
{
    public class DocumentTypeProfile : Profile
    {
        public DocumentTypeProfile()
        {
            CreateMap<DocumentType, ReadDocumentTypeResponse>();
        }
    }
}