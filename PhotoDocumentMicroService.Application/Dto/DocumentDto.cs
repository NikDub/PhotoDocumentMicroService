namespace PhotoDocumentMicroService.Application.Dto;

public class DocumentDto
{
    public Guid Id { get; set; }
    public Guid ResultId { get; set; }
    public string FileName { get; set; }
    public byte[] Value { get; set; }
}