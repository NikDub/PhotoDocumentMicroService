using PhotoDocumentMicroService.Application.Dto;

namespace PhotoDocumentMicroService.Application.Service.Abstractions;

public interface IDocumentService
{
    Task<PhotoDto> GetByIdAsync(Guid id);
    Task<List<DocumentDto>> GetByResultIdAsync(Guid resultId);
    Task<DocumentDto> CreateDocumentAsync(DocumentForCreatedDto model);
    Task<PhotoDto> CreatePhotoAsync(PhotoForCreatedDto model);
    Task<string> GetUrlByIdAsync(Guid id);
}