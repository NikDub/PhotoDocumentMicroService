namespace PhotoDocumentMicroService.Application.Dto;

public class DocumentDto
{
    public string Id { get; set; }
    public string ResultId { get; set; }
    public string FileName { get; set; }
    public byte[] Value { get; set; }
}