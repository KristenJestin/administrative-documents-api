using Application.DTOs.DocumentType;
using AutoMapper;
using Domain.Entities;

namespace Application.Mappings
{
    public class DocumentTypeProfile : Profile
    {
        public DocumentTypeProfile()
        {
            CreateMap<DocumentType, ReadDocumentTypeResponse>();
            CreateMap<DocumentType, ReadDocumentTypeListResponse>();
        }
    }
}