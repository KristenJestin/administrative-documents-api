using Domain.Common;
using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class DocumentFile : DatedBaseEntity
    {
        public string OriginalName { get; set; }
        public string Path { get; set; }
        public string Encryption { get; set; }
        [MaxLength(255)]
        public string IV { get; set; }
        public long Size { get; set; }
        public string MimeType { get; set; }
        public string Dimensions { get; set; }
        [JsonIgnore]
        public Document Document { get; set; }
    }
}
