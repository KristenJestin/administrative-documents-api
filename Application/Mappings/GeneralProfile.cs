using Application.DTOs.Document;
using Application.DTOs.DocumentType;
using Application.Features.Documents.Commands.CreateDocument;
using AutoMapper;
using Domain.Entities;

namespace Application.Mappings
{
    public class GeneralProfile : Profile
    {
        public GeneralProfile()
        {
            //CreateMap<Product, GetAllProductsViewModel>().ReverseMap();
            //CreateMap<GetAllProductsQuery, GetAllProductsParameter>();
            CreateMap<CreateDocumentCommand, Document>()
                .ForMember(x => x.File, y => y.Ignore())
                .ForMember(x => x.Type, y => y.Ignore());
            CreateMap<Document, ReadDocumentResponse>();


            CreateMap<DocumentType, ReadDocumentTypeResponse>();
        }
    }
}