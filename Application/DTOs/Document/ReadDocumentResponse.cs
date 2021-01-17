using Application.DTOs.DocumentType;
using System;

namespace Application.DTOs.Document
{
    public class ReadDocumentResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int FileRef { get; set; }
        public string Note { get; set; }
        public ReadDocumentTypeResponse Type { get; set; }


        #region driven by type
        public decimal? Amount { get; set; }
        public DateTime? Date { get; set; }
        public int? Duration { get; set; }
        public DateTime? EndDate { get; set; }
        #endregion
    }
}
