﻿using Domain.Common;
using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public class Document : DatedBaseEntity
    {
        public string Name { get; set; }
        public int FileId { get; set; }
        public DocumentFile File { get; set; }
        public string Note { get; set; }
        public int? TypeId { get; set; }
        public DocumentType Type { get; set; }
        public Folder Folder { get; set; }
        public ICollection<DocumentTag> Tags { get; set; }


        #region driven by type
        public decimal? Amount { get; set; }
        public DateTime? Date { get; set; }
        public int? Duration { get; set; }
        public DateTime? EndDate
            => Duration != null ? Date?.AddMonths(Duration.Value) : null;
        #endregion


        public Document()
        {
            Tags = new List<DocumentTag>();
        }
    }
}
