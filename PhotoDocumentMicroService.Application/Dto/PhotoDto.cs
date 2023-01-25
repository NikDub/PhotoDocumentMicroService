namespace PhotoDocumentMicroService.Application.Dto;

public class PhotoDto
{
    public Guid Id { get; set; }
    public string FileName { get; set; }
    public byte[] Value { get; set; }
}