namespace Application.DTOs.DocumentType
{
    public class ReadDocumentTypeListResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool HasDate { get; set; }
        public bool HasAmount { get; set; }
        public bool HasDuration { get; set; }
    }
}
