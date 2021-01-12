using Domain.Common;

namespace Domain.Entities
{
    public class DocumentType: DatedBaseEntity
    {
        public string Name { get; set; }
        public bool HasDate { get; set; }
        public bool HasAmount { get; set; }
        public bool HasDuration { get; set; }
    }
}
