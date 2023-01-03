using PhotoDocumentMicroService.Application.DTO;

namespace PhotoDocumentMicroService.Application.Service.Abstractions;

public interface IDocumentService
{
    Task<PhotoDto> GetByIdAsync(string id);
    Task<List<DocumentDto>> GetByResultIdAsync(string resultId);
    Task<DocumentDto> CreateDocumentAsync(DocumentForCreatedDto model);
    Task<PhotoDto> CreatePhotoAsync(PhotoForCreatedDto model);
}