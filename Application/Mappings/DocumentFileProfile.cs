using Application.DTOs.DocumentFile;
using AutoMapper;
using Domain.Entities;

namespace Application.Mappings
{
    public class DocumentFileProfile : Profile
    {
        public DocumentFileProfile()
        {
            CreateMap<DocumentFile, ReadDocumentFileResponse>();
        }
    }
}