namespace PhotoDocumentMicroService.Application.DTO
{
    public class DocumentForCreatedDto
    {
        public string PatientId { get; set; }
        public string FileName { get; set; }
        public byte[] Value { get; set; }
    }
}
