using MimeTypes;

namespace Application.DTOs.DocumentFile
{
    public class ReadDocumentFileResponse
    {
        public string OriginalName { get; set; }
        public string MimeType { get; set; }
        public string Extension
            => MimeTypeMap.GetExtension(MimeType);
    }
}
