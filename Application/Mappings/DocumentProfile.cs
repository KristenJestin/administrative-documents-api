using Application.DTOs.Document;
using Application.Features.Documents.Commands.CreateDocument;
using Application.Features.Documents.Queries.GetAllDocuments;
using Application.Features.Documents.Queries.GetDocumentsByTerms;
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
            CreateMap<GetDocumentsByTermsQuery, GetDocumentsByTermsParameter>();

            CreateMap<Document, ReadDocumentResponse>();
            CreateMap<Document, ReadDocumentListResponse>();
        }
    }
}