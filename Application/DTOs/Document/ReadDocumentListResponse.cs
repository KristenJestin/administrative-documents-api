﻿using Application.DTOs.DocumentTag;
using Application.DTOs.DocumentType;
using System;
using System.Collections.Generic;

namespace Application.DTOs.Document
{
    public class ReadDocumentListResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Note { get; set; }
        public ReadDocumentTypeResponse Type { get; set; }
        public ICollection<ReadDocumentTagResponse> Tags { get; set; }


        #region driven by type
        public decimal? Amount { get; set; }
        public DateTime? Date { get; set; }
        public int? Duration { get; set; }
        public DateTime? EndDate { get; set; }
        #endregion

    }
}
