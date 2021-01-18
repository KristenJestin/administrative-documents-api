using Domain.Common;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class DocumentTag : DatedBaseEntity
    {
        public string Name { get; set; }
        public string UniqueName { get; set; }
        public ICollection<Document> Documents { get; set; }
    }
}
