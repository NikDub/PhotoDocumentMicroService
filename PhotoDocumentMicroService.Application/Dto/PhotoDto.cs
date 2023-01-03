namespace PhotoDocumentMicroService.Application.DTO
{
    public class PhotoDto
    {
        public string Id { get; set; }
        public string FileName { get; set; }
        public byte[] Value { get; set; }
    }
}
