using Application.DTOs.DocumentTags;
using Application.DTOs.DocumentType;
using System;
using System.Collections.Generic;

namespace Application.DTOs.Document
{
    public class ReadDocumentResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int FileId { get; set; }
        public string Note { get; set; }
        public ReadDocumentTypeResponse Type { get; set; }
        public ICollection<ReadDocumentTagResponse> Tags { get; set; }


        #region driven by type
        public decimal? Amount { get; set; }
        public DateTime? Date { get; set; }
        public int? Duration { get; set; }
        public DateTime? EndDate { get; set; }
        #endregion

        public ReadDocumentResponse()
        {
            Tags = new List<ReadDocumentTagResponse>();
        }
    }
}
