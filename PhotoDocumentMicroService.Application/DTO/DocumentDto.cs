namespace PhotoDocumentMicroService.Application.DTO
{
    public class DocumentDto
    {
        public string Id { get; set; }
        public string PatientId { get; set; }
        public string FileName { get; set; }
        public byte[] Value { get; set; }
    }
}
