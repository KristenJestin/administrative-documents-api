﻿using Application.DTOs.DocumentTag;
using AutoMapper;
using Domain.Entities;

namespace Application.Mappings
{
    public class DocumentTagProfile : Profile
    {
        public DocumentTagProfile()
        {
            CreateMap<DocumentTag, ReadDocumentTagResponse>();
            CreateMap<DocumentTag, ReadDocumentTagListResponse>();
        }
    }
}