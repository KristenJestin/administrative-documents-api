using Application.DTOs.Document;
using Application.DTOs.DocumentType;
using Application.Features.Documents.Commands.CreateDocument;
using Application.Features.Documents.Queries.GetAllDocuments;
using AutoMapper;
using Domain.Entities;

namespace Application.Mappings
{
    public class DocumentProfile : Profile
    {
        public DocumentProfile()
        {
            CreateMap<CreateDocumentCommand, Document>()
                .ForMember(x => x.Tags, y => y.Ignore())
                .ForMember(x => x.File, y => y.Ignore())
                .ForMember(x => x.Type, y => y.Ignore());
            CreateMap<GetAllDocumentsQuery, GetAllDocumentsParameter>();

            CreateMap<Document, ReadDocumentResponse>();
            CreateMap<Document, ReadDocumentListResponse>();
        }
    }
}