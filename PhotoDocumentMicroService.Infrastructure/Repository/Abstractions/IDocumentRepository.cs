using PhotoDocumentMicroService.Domain.Entities.Models;

namespace PhotoDocumentMicroService.Infrastructure.Repository.Abstractions;

public interface IDocumentRepository
{
    Task<Document> GetEntityAsync(string type, string id);
    List<Document> GetEntityByResultId(Guid resultId);
    Task<Document> CreateEntityAsync(Document entity);
}