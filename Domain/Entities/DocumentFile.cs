using Domain.Common;

namespace Domain.Entities
{
    public class DocumentFile : DatedBaseEntity
    {
        public string OriginalName { get; set; }
        public string Path { get; set; }
        public string Encryption { get; set; }
        public long Size { get; set; }
        public Document Document { get; set; }
    }
}
