using PhotoDocumentMicroService.Application.DTO;

namespace PhotoDocumentMicroService.Application.Service.Abstractions;

public interface IDocumentService
{
    Task<DocumentDto> GetByIdAsync(string id);
    Task<List<DocumentDto>> GetByPatientAsyncId(string resultId);
    Task<DocumentDto> CreateAsync(DocumentForCreatedDto model, string docType);

}