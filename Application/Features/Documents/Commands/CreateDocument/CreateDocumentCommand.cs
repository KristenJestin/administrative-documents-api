using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;

namespace Application.Features.Documents.Commands.CreateDocument
{
    public class CreateDocumentCommand : IRequest<int>
    {
        public string Name { get; set; }
        public IFormFile File { get; set; }
        public string Note { get; set; }
        public int? Type { get; set; }

        //TODO: add folder management with folder ref or new folder
        //public Folder Folder { get; set; }


        #region driven by type
        public decimal? Amount { get; set; }
        public DateTime? Date { get; set; }
        public int? Duration { get; set; }
        #endregion

        #region statics
        public static readonly string[] ALLOWED_FILE_TYPES = new[] { "image/jpeg", "image/jpg", "image/png", "application/pdf" };
        #endregion
    }
}
