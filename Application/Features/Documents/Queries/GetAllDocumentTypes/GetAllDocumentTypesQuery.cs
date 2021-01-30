﻿using Application.DTOs.Document;
using Application.DTOs.DocumentType;
using Application.Wrappers;
using MediatR;
using System.Collections.Generic;

namespace Application.Features.Documents.Queries.GetAllDocumentTypes
{
    public class GetAllDocumentTypesQuery : IRequest<BasicApiResponse<IEnumerable<ReadDocumentTypeListResponse>>> { }
}
