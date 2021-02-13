using System.IO;

namespace Application.DTOs.Document
{
    public class DownloadDocumentResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ContentType { get; set; }
        public byte[] FileContent { get; set; }
    }
}
