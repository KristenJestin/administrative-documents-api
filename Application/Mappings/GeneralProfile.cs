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
            CreateMap<CreateDocumentCommand, Document>();
        }
    }
}