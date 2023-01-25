namespace PhotoDocumentMicroService.Application.Dto;

public class DocumentForCreatedDto
{
    public Guid ResultId { get; set; }
    public string FileName { get; set; }
    public byte[] Value { get; set; }
}